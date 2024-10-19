using Interface;
using UnityEngine;

namespace Entitys
{
    public class Interactor : MonoBehaviour
    {
        private Rigidbody2D rb;
        public int gridSize;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            InvokeRepeating(nameof(HandleInteract), .5f, 1.5f);
        }

        private void HandleInteract()
        {
            Debug.Log("Chequei interacao! " + gameObject.name );

            Vector2[] directions = {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right
            };

            bool foundInteractable = false;

            foreach (var direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, gridSize, LayerMask.GetMask("Interactable"));

                if (hit.collider != null)
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        foundInteractable = true;
                        Debug.Log("POSSO interacao! " + gameObject.name);
                        interactable.CanInteract(); // Chamado se o objeto está em alcance para interagir

                        // Se o jogador pressionar o botão de interagir (por exemplo, "E"):
                        if (Input.GetButtonDown("Interact"))
                        {
                            Debug.Log("APERTEI interacao! " + gameObject.name);
                            interactable.OnInteract();
                        }
                    }
                }
            }

            // Se nenhum objeto interagível foi encontrado nas direções, chama CantInteract()
            if (!foundInteractable)
            {
                // Caso não tenha encontrado nenhum objeto, notifica que não pode interagir
                RaycastHit2D[] hits = Physics2D.RaycastAll(rb.position, Vector2.zero, gridSize, LayerMask.GetMask("Interactable"));
                foreach (var hit in hits)
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.CantInteract();
                    }
                }
            }
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

                    // Se encontrar um objeto interagível, a cor da linha será verde, senão vermelha
                    Gizmos.color = hit.collider != null ? Color.green : Color.red;
                    Gizmos.DrawLine(rb.position, rb.position + direction * gridSize);
                }
            }
        }
    }
}

    
