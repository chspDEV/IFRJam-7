using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class Wire: MonoBehaviour
    {
        public Sprite fioCortado;
        private Image img;

        private void Awake()
        {
            img = GetComponent<Image>();
        }
        
        public void Cortar()
        {
            SoundManager.Instance.PlaySound("Cortando Fios", SoundManager.SoundMixer.SFX);
            img.sprite = fioCortado;
            enabled = false;
        }
    }
}