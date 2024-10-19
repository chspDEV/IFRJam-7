using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class ButtonPuzzle : MonoBehaviour
    {
        public string correctSequence = "134972";
        private string playerSequence = "";

        public TextMeshProUGUI display;
        public Button[] buttons;

        void Start()
        {
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(() => ButtonPressed(button));
            }
            
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

        private void PuzzleCompleted()
        {
            PuzzleManager.Instance.PuzzleWin();
        }
    }

}