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
        private bool isCompleted;
        void Start()
        {
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(() => ButtonPressed(button));
            }
            
        }

        public void QuitPuzzle()
        {
            if(GameManager.Instance.State == GameState.PUZZLE)
                GameManager.Instance.SetState(GameState.PLAY);
        }

        void OnDisable()
        {
            QuitPuzzle();
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
            if (!isCompleted)
            {
                PuzzleManager.Instance.PuzzleWin();
                isCompleted = true;
            }
        }
    }

}