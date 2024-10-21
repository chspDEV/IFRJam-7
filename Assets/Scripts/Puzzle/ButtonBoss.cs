using Interface;
using Manager;
using UnityEngine;
using System;


namespace Puzzle
{
    public class ButtonBoss : MonoBehaviour, IInteractable
    {
        private bool hasInteracted = false;
        public Sprite pressedSprite;
        
        public void OnInteract()
        {
            if(hasInteracted) return;
            
            SoundManager.Instance.PlaySound("Completando Puzzle", SoundManager.SoundMixer.SFX);
            PuzzleManager.Instance.PuzzleWin();
            hasInteracted = true;
            var sprR = GetComponent<SpriteRenderer>().sprite = pressedSprite;
        }

        public void OnLeave()
        {
            //nada
        }

        public void ControlIcon(bool state)
        {
            //nada
        }
    }
}