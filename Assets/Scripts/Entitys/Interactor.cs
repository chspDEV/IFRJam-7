using Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entitys
{
    public class Interactor : MonoBehaviour
    {
        [Header("Configuration")]
        private Rigidbody2D rb;
        [FormerlySerializedAs("gridSize")] public float distanceRaycast = 1.33f;
        public IInteractable cInteractable;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && cInteractable != null)
            {
                cInteractable.OnInteract();
                Debug.Log("interagi! " + gameObject.name);
            }
        }

        public void HandleInteract()
        {
            Debug.Log("Chequei interacao! " + gameObject.name);

            Vector2[] directions = {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right
            };

            IInteractable foundInteractable = null;

            foreach (var direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, distanceRaycast, LayerMask.GetMask("Interactable"));

                if (hit.collider != null)
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        foundInteractable = interactable;
                        Debug.Log("Posso interagir! " + gameObject.name);
                        interactable.ControlIcon(true);
                        break;
                    }
                }
            }

            if (foundInteractable == null && cInteractable != null)
            {
                cInteractable.ControlIcon(false);
                cInteractable.OnLeave();
                Debug.Log("LIMPANDO INTERACTABLES");
            }

            cInteractable = foundInteractable;
        }

        private void OnDrawGizmos()
        {
            if (rb != null)
            {
                Vector2[] directions = {
                    Vector2.up,
                    Vector2.down,
                    Vector2.left,
                    Vector2.right
                };

                foreach (var direction in directions)
                {
                    RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, distanceRaycast, LayerMask.GetMask("Interactable"));

                    Gizmos.color = hit.collider != null ? Color.green : Color.red;
                    Gizmos.DrawLine(rb.position, rb.position + direction * distanceRaycast);
                }
            }
        }
    }
}
