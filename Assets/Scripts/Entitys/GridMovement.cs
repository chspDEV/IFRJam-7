using System.Collections;
using Manager;
using UnityEngine;

namespace Entitys
{
    public class GridMovement : MonoBehaviour
    {
        public float gridSize = 1f;
        public float moveSpeed = 0.2f;
        public bool invertMovement = false;
        public LayerMask obstacleLayer;

        private Vector2 targetPosition;
        private bool isMoving = false;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Color debugColor = Color.white;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            targetPosition = rb.position;
        }

        void FixedUpdate()
        {
            HandleMovement();
        }

        private void Update()
        {
            enabled = (GameManager.Instance.State != GameState.DEFEAT || GameManager.Instance.State != GameState.WIN);
        }

        private void HandleMovement()
        {
            if (!isMoving)
            {
                Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                if (Mathf.Abs(input.x) > 0 && Mathf.Abs(input.y) > 0)
                {
                    input = Vector2.zero;
                }

                if (input != Vector2.zero)
                {
                    Vector2 nextPosition = invertMovement 
                        ? targetPosition - new Vector2(input.x, input.y) * gridSize 
                        : targetPosition + new Vector2(input.x, input.y) * gridSize;

                    if (!IsBlocked(nextPosition) && !isMoving)
                    {
                        spriteRenderer.flipX = invertMovement ? (input.x < 0) : (input.x > 0); 
                        
                        debugColor = Color.green;
                        StartCoroutine(MoveToPosition(nextPosition));
                    }
                    else
                    {
                        debugColor = Color.red;
                    }
                }
            }
        }

        IEnumerator MoveToPosition(Vector2 destination)
        {
            isMoving = true;

            while (rb.position != destination)
            {
                Vector2 newPosition = Vector2.MoveTowards(rb.position, destination, moveSpeed * Time.deltaTime);
                rb.MovePosition(newPosition);
                yield return null;
            }

            var i = GetComponent<Interactor>();
            i.HandleInteract();
            targetPosition = destination;
            isMoving = false;
        }

        bool IsBlocked(Vector2 target)
        { 
            RaycastHit2D hit = Physics2D.Raycast(rb.position, target - rb.position, gridSize, obstacleLayer);
            return hit.collider;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = debugColor;

            if (rb != null)
                Gizmos.DrawLine(rb.position, targetPosition);
        }
    }
}
