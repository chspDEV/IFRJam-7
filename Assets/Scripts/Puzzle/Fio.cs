using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class Fio : MonoBehaviour
    {
        public Sprite fioCortado;
        private Image spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<Image>();
        }

        public void CortarFio()
        {
            spriteRenderer.sprite = fioCortado;
        }



    }
}
