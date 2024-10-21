using Manager;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

namespace Entitys
{
    public class Boss : MonoBehaviour
    {
        public Animator animator;
        public float stateChangeInterval = 5f; // Tempo entre trocas de estado aleatório
        public float deathAnimationTime = 2f;  // Tempo que a animação de morte leva para terminar
        private float stateChangeTimer;
        private bool isDead = false;
        private bool hasInit = false;

        public Camera bossCamera;
        public Camera mainCamera;
        public Camera dogCamera;
        public Camera humanCamera;

        private enum States
        {
            Cansado,
            Olhando,
            Rindo,
            Morrendo,
            Ligando
        }

        private States currentState;

        void Start()
        {
            StartCoroutine(initBoss());
            bossCamera.enabled = false;
        }

        private IEnumerator initBoss()
        {
            yield return new WaitForSeconds(3f);
            hasInit = true;
            ChangeState(States.Ligando);
        }

        void Update()
        {
            if (isDead || hasInit == false) return;

            // Timer para alternar estados aleatoriamente
            stateChangeTimer -= Time.deltaTime;
            if (stateChangeTimer <= 0 && currentState == States.Olhando)
            {
                ChangeState(Random.Range(0, 2) == 0 ? States.Rindo : States.Cansado);
                stateChangeTimer = stateChangeInterval;
            }

            if (PuzzleManager.Instance.CheckVictory())
            {
                OnDeath();
            }
        }

        void ChangeState(States newState)
        {
            currentState = newState;
            switch (currentState)
            {
                case States.Ligando:
                    PlayAnimation("Ligando", 0.2f);
                    Invoke("SwitchToOlhando", 2f); // Depois de 2s troca para Olhando
                    break;
                case States.Olhando:
                    PlayAnimation("Olhando", 0.2f);
                    break;
                case States.Rindo:
                    PlayAnimation("Rindo", 0.2f);
                    Invoke("SwitchToOlhando", 3f); // Volta para Olhando após 3s
                    break;
                case States.Cansado:
                    PlayAnimation("Cansado", 0.2f);
                    Invoke("SwitchToOlhando", 3f); // Volta para Olhando após 3s
                    break;
                case States.Morrendo:
                    isDead = true;
                    PlayAnimation("Morrendo", 0.2f);
                    Invoke("Die", deathAnimationTime);
                    break;
            }
        }

        void SwitchToOlhando()
        {
            if (!isDead) ChangeState(States.Olhando);
        }

        void PlayAnimation(string animationName, float transitionTime)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log("Rodando anim " + animationName + "  -> " + gameObject.name);
            if (stateInfo.normalizedTime >= 0.05f)
            {
                animator.CrossFade(animationName, transitionTime);
            }
        }
        
        public void Die()
        {
            GameManager.Instance.NextLevel();
        }
        
        public void OnDeath()
        {
            if (!isDead)
            {
                bossCamera.enabled = true;
                mainCamera.enabled = false;
                dogCamera.enabled = false;
                humanCamera.enabled = false;
                ChangeState(States.Morrendo);
            }
        }
    }
}
