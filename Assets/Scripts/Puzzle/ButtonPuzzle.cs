using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class ButtonPuzzle : MonoBehaviour
    {
        private string playerSequence = "";
        private string correctSequence;
        public TextMeshProUGUI display;
        
        public Button[] buttons;

        void Start()
        {
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(() => ButtonPressed(button));
            }

            correctSequence = PuzzleManager.Instance.ObterOrdem();
        }
        
        void ButtonPressed(Button button)
        {
            playerSequence += button.name;
            display.text = playerSequence;
            
            if (playerSequence == correctSequence)
            {
                Debug.Log("Você venceu!");
                PuzzleCompleted();
            }
            else if (!correctSequence.StartsWith(playerSequence))
            {
                Debug.Log("Sequência errada! Tente novamente.");
                playerSequence = "";
                display.text = playerSequence;
            }
        }
        
        void PuzzleCompleted()
        {
            PuzzleManager.Instance.ChecarOrdem(playerSequence);
        }
    }

}