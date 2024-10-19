using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class Wire: MonoBehaviour
    {
        public Sprite fioCortado;
        public Image img;

        public void Cortar()
        {
            img.sprite = fioCortado;
        }
    }
}