using Interface;
using UnityEngine;

namespace Entitys
{
    public class Interactor : MonoBehaviour
    {
        private Rigidbody2D rb;
        public int gridSize = 1;
        public IInteractable cInteractable;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            InvokeRepeating(nameof(HandleInteract), 0.5f, 1.5f);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && cInteractable != null )
            {
                cInteractable.OnInteract();
                Debug.Log("interagi! " + gameObject.name);
            }
        }

        private void HandleInteract()
        {
            Debug.Log("Chequei interacao! " + gameObject.name);

            Vector2[] directions = {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right
            };
            
            foreach (var direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, gridSize, LayerMask.GetMask("Interactable"));

                if (hit.collider != null)
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {

                        cInteractable = interactable;
                        Debug.Log("POSSO interagir! " + gameObject.name);
                        interactable.ControlIcon(true);
                    }
                    else
                    {
                        cInteractable.ControlIcon(false);
                        Debug.Log("Saindo de perto de: " + gameObject.name);
                        cInteractable = null;
                    }
                }
            }

            cInteractable = null;
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
                    RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, gridSize, LayerMask.GetMask("Interactable"));

                    Gizmos.color = hit.collider != null ? Color.green : Color.red;
                    Gizmos.DrawLine(rb.position, rb.position + direction * gridSize);
                }
            }
        }
    }
}
