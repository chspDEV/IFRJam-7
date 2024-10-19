using Interface;
using Manager;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleActivator : MonoBehaviour, IInteractable
    {

        public int myNumberInPuzzleManager;
        public GameObject onRangeImage;


        public void CanInteract()
        {
            onRangeImage.SetActive(true);
        }

        public void CantInteract()
        {
            onRangeImage.SetActive(false);
        }

        public void OnInteract()
        {
            PuzzleManager.Instance.AtivarPuzzle(myNumberInPuzzleManager);
        }


    }
}