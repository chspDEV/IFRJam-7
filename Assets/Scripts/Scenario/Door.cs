using Interface;
using Manager;
using UnityEngine;

namespace Scenario
{
    public class Door: MonoBehaviour, IInteractable
    {
        public GameObject onRangeImage;
        public bool isInteracting;
        [Tooltip("0 = dog // 1 = human")]
        public int entityToDeactiveIndex;

        private void Update()
        {
            onRangeImage.SetActive(isInteracting);
        }

        public void OnLeave()
        {
            //nada e requerido aq
        }

        public void ControlIcon(bool state)
        {
            isInteracting = state;
        }

        public void OnInteract()
        {
            if (PuzzleManager.Instance.CheckVictory()) //CHECANDO SE JA POSSO GANHAR, JAPODEGANHAR???
            {
                GameManager.Instance.EnterDoor(entityToDeactiveIndex);
            }
            else
            {
                //sound error
            }
        }
    }
}