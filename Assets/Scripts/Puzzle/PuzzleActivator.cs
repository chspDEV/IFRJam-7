using System;
using Interface;
using Manager;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleActivator : MonoBehaviour, IInteractable
    {

        public int myNumberInPuzzleManager;
        public GameObject onRangeImage;
        public bool isInteracting = false;

        private void Update()
        {
            onRangeImage.SetActive(isInteracting);
        }

        public void ControlIcon(bool state)
        {
            isInteracting = state;
        }

        public void OnInteract()
        {
            PuzzleManager.Instance.AtivarPuzzle(myNumberInPuzzleManager);
        }


    }
}