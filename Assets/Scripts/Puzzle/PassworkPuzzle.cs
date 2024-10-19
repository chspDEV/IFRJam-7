using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Puzzle
{
    public class PasswordPuzzle : MonoBehaviour
    {
        public string correctPassword = "batatasfritas";
        private string playerInput = "";
        
        public TMP_InputField passwordInputField;
        public Button submitButton;
        public TextMeshProUGUI displayText;

        void Start()
        {
            submitButton.onClick.AddListener(CheckPassword);
        }
        
        void CheckPassword()
        {
            playerInput = passwordInputField.text;
            
            if (playerInput == correctPassword)
            {
                displayText.text = "Sucesso!";
                submitButton.gameObject.SetActive(false);
                PuzzleCompleted();
            }
            else
            {
                displayText.text = "ERROR...";
                ResetInput();
            }
        }
        
        void ResetInput()
        {
            passwordInputField.text = "";
        }
        
        void PuzzleCompleted()
        {
            PuzzleManager.Instance.PuzzleWin();
        }
        
    }

}