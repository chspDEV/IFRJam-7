using System.Collections;
using Manager;
using UnityEngine;

namespace Entitys
{
    public class EntityMainLogic : MonoBehaviour
    {
        [Header("Movement")]
        public float gridSize = 1f;
        public float moveSpeed = 0.2f;
        [SerializeField] private bool invertMovement;
        public LayerMask obstacleLayer;
        public Vector2 offsetRbRaycast;
        
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
            var _enabled = (GameManager.Instance.State == GameState.PLAY);
            if (!_enabled) return;
            
            HandleMovement();
        }

        private void Update()
        {
            var _enabled = (GameManager.Instance.State == GameState.PLAY);
            if (!_enabled) return;
            
            UpdateState();
        }

        private void HandleMovement()
        {
            if (isMoving) return;
            
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
            RaycastHit2D hit = Physics2D.Raycast(rb.position + offsetRbRaycast,
                target - rb.position, gridSize, obstacleLayer);
            return hit.collider;
        }

        private void UpdateState()
        {
            switch (currentState)
            {
                case State.Idle:
                    Debug.Log("Rodando Idle state " + gameObject.name);
                    PlayAnimation("Idle", 0f);
                    break;

                case State.Walking:
                    Debug.Log("Rodando Walking state " + gameObject.name);
                    PlayAnimation("Walk", 0f);
                    break;
            }
        }

        void PlayAnimation(string animationName, float transitionTime)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Rodando anim " + animationName + "  -> " +gameObject.name);
            if (stateInfo.normalizedTime >= 0.05f)
            { 
                animator.CrossFade(animationName, transitionTime); 
            }
            
        }

        public void PlayStepSound()
        {
            SoundManager.Instance.PlaySound("Passo 2", SoundManager.SoundMixer.SFX);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = debugColor;

            if (rb != null)
                Gizmos.DrawLine(rb.position +offsetRbRaycast, targetPosition);
        }
    }
}
