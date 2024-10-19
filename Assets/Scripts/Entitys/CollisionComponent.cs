using UnityEngine;
using System;
using System.Collections.Generic;
using Interface;

namespace Entitys
{
    public class CollisionComponent : MonoBehaviour
    {
        [Serializable]
        public class CollisionCheck
        {
            public string name;
            public float radius = 0.5f;
            public Vector3 offset = Vector3.zero;
            public LayerMask layers;
            public Color collisionColor = Color.green;
            public Color noCollisionColor = Color.red;
            public CollisionType collisionType = CollisionType.Sphere;
            public float raycastDistance = 1.0f;
            [HideInInspector] public bool isColliding;
            [HideInInspector] public ICollisionResult collisionResult;

            public Vector3 GetCheckPosition(Transform transform, Vector3 facingDirection)
            {
                return transform.position + facingDirection * offset.x + Vector3.up * offset.y + Vector3.forward * offset.z;
            }
        }

        public enum CollisionType
        {
            Sphere,
            Raycast,
            Box
        }

        [SerializeField] private List<CollisionCheck> collisionChecks = new List<CollisionCheck>();
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Dictionary<CollisionType, ICollisionDetector> collisionDetectors;
        private Vector3 FacingDirection => spriteRenderer != null && spriteRenderer.flipX ? Vector3.left : Vector3.right;

        private void Awake()
        {
            InitializeCollisionDetectors();
        }

        private void InitializeCollisionDetectors()
        {
            collisionDetectors = new Dictionary<CollisionType, ICollisionDetector>
            {
                { CollisionType.Sphere, new SphereCollisionDetector() },
                { CollisionType.Raycast, new RaycastCollisionDetector() },
                { CollisionType.Box, new BoxCollisionDetector() }
            };
        }

        private void FixedUpdate()
        {
            UpdateCollisionStates();
        }

        private void UpdateCollisionStates()
        {
            foreach (var check in collisionChecks)
            {
                if (collisionDetectors.TryGetValue(check.collisionType, out var detector))
                {
                    Vector3 checkPosition = check.GetCheckPosition(transform, FacingDirection);
                    check.collisionResult = detector.Detect(checkPosition, check);
                    check.isColliding = check.collisionResult != null;
                }
            }
        }

        public bool IsColliding<T>(string checkName, out T result) where T : class, ICollisionResult
        {
            result = null;
            var check = collisionChecks.Find(c => c.name == checkName);
            if (check != null && check.collisionResult is T typedResult)
            {
                result = typedResult;
                return check.isColliding;
            }
            return false;
        }

        public float CalculateFallDistance()
        {
            var groundCheck = collisionChecks.Find(c => c.name == "GroundDistance");
            if (groundCheck == null) return 0f;

            Vector3 checkPosition = groundCheck.GetCheckPosition(transform, FacingDirection);
            if (Physics.Raycast(checkPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundCheck.layers))
            {
                return hit.distance;
            }
            return Mathf.Infinity;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            foreach (var check in collisionChecks)
            {
                if (collisionDetectors.TryGetValue(check.collisionType, out var detector))
                {
                    detector.DrawGizmos(check, check.GetCheckPosition(transform, FacingDirection));
                }
            }
        }
    }

    public interface ICollisionResult { }

    public class EntityCollisionResult : ICollisionResult
    {
        public IInteractable Entity { get; }
        public EntityCollisionResult(IInteractable entity) => Entity = entity;
    }

    public class RaycastResult : ICollisionResult
    {
        public RaycastHit Hit { get; }
        public float distance => Hit.distance;
        public RaycastResult(RaycastHit hit) => Hit = hit;
    }

    public interface ICollisionDetector
    {
        ICollisionResult Detect(Vector3 position, CollisionComponent.CollisionCheck check);
        void DrawGizmos(CollisionComponent.CollisionCheck check, Vector3 position);
    }

    public class SphereCollisionDetector : ICollisionDetector
    {
        public ICollisionResult Detect(Vector3 position, CollisionComponent.CollisionCheck check)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, check.radius, check.layers);
            if (hitColliders.Length > 0)
            {
                var entity = hitColliders[0].GetComponent<IInteractable>();
                return entity != null ? new EntityCollisionResult(entity) : new RaycastResult(new RaycastHit());
            }
            return null;
        }

        public void DrawGizmos(CollisionComponent.CollisionCheck check, Vector3 position)
        {
            Gizmos.color = check.isColliding ? check.collisionColor : check.noCollisionColor;
            Gizmos.DrawWireSphere(position, check.radius);
        }
    }

    public class RaycastCollisionDetector : ICollisionDetector
    {
        public ICollisionResult Detect(Vector3 position, CollisionComponent.CollisionCheck check)
        {
            if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, check.raycastDistance, check.layers))
            {
                var entity = hit.collider.GetComponent<IInteractable>();
                return entity != null ? new EntityCollisionResult(entity) : new RaycastResult(hit);
            }
            return null;
        }

        public void DrawGizmos(CollisionComponent.CollisionCheck check, Vector3 position)
        {
            Gizmos.color = check.isColliding ? check.collisionColor : check.noCollisionColor;
            Gizmos.DrawLine(position, position + Vector3.down * check.raycastDistance);
        }
    }

    public class BoxCollisionDetector : ICollisionDetector
    {
        public ICollisionResult Detect(Vector3 position, CollisionComponent.CollisionCheck check)
        {
            Collider[] hitColliders = Physics.OverlapBox(position, Vector3.one * check.radius, Quaternion.identity, check.layers);
            if (hitColliders.Length > 0)
            {
                var entity = hitColliders[0].GetComponent<IInteractable>();
                return entity != null ? new EntityCollisionResult(entity) : new RaycastResult(new RaycastHit());
            }
            return null;
        }

        public void DrawGizmos(CollisionComponent.CollisionCheck check, Vector3 position)
        {
            Gizmos.color = check.isColliding ? check.collisionColor : check.noCollisionColor;
            Gizmos.DrawWireCube(position, Vector3.one * check.radius * 2);
        }
    }
}