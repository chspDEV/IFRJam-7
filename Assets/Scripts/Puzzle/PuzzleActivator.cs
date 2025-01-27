﻿using Interface;
using Manager;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleActivator : MonoBehaviour, IInteractable
    {

        public int myNumberInPuzzleManager;
        public GameObject onRangeImage;
        public bool isInteracting;

        private void Update()
        {
            onRangeImage.SetActive(isInteracting);
        }

        public void OnLeave()
        {
            PuzzleManager.Instance.ControlarPuzzle(myNumberInPuzzleManager, false);
        }

        public void ControlIcon(bool state)
        {
            isInteracting = state;
        }

        public void OnInteract()
        {
            SoundManager.Instance.PlaySound("Abrindo Fechando Puzzle", SoundManager.SoundMixer.SFX);
            PuzzleManager.Instance.ControlarPuzzle(myNumberInPuzzleManager, true);
        }


    }
}