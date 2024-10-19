using System.Collections;
using Manager;
using UnityEngine;

namespace Entitys
{
    public class EntityMainLogic : MonoBehaviour
    {
        public float gridSize = 1f;
        public float moveSpeed = 0.2f;
        [SerializeField] private bool invertMovement;
        public LayerMask obstacleLayer;

        private Vector2 targetPosition;
        private bool isMoving;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Animator animator; 
        private Color debugColor = Color.white;

        private enum State { Idle, Walking }
        private State currentState = State.Idle;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>(); 
            targetPosition = rb.position;
        }

        void FixedUpdate()
        {
            HandleMovement();
        }

        private void Update()
        {
            enabled = (GameManager.Instance.State != GameState.DEFEAT || GameManager.Instance.State != GameState.WIN);
            UpdateState();
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
                else
                {
                    // Sem input, o state muda para Idle
                    currentState = State.Idle;
                }
            }
        }

        IEnumerator MoveToPosition(Vector2 destination)
        {
            isMoving = true;
            currentState = State.Walking;

            Vector2 startPosition = rb.position;
            float timeElapsed = 0f;
            float totalDuration = 0.5f;

            while (timeElapsed < totalDuration)
            {
                Vector2 newPosition = Vector2.Lerp(startPosition, destination, timeElapsed / totalDuration);
                rb.MovePosition(newPosition);

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            rb.MovePosition(destination); 

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

        private void UpdateState()
        {
            switch (currentState)
            {
                case State.Idle:
                    animator.Play("Idle"); 
                    break;

                case State.Walking:
                    animator.Play("Walk"); 
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = debugColor;

            if (rb != null)
                Gizmos.DrawLine(rb.position, targetPosition);
        }
    }
}
