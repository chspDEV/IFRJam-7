using Manager;
using TMPro;
using UnityEngine;
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

        private bool isCompleted;

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
                SoundManager.Instance.PlaySound("Mensagem Error", SoundManager.SoundMixer.SFX);
                ResetInput();
            }
        }
        
        void ResetInput()
        {
            passwordInputField.text = "";
        }
        
        void OnDisable()
        {
            QuitPuzzle();
        }
        
        public void QuitPuzzle()
        {
            SoundManager.Instance.PlaySound("Abrindo Fechando Puzzle", SoundManager.SoundMixer.SFX);
            if(GameManager.Instance.State == GameState.PUZZLE)
                GameManager.Instance.SetState(GameState.PLAY);
        }
        
        void PuzzleCompleted()
        {
            if (!isCompleted)
            {
                SoundManager.Instance.PlaySound("Completando Puzzle", SoundManager.SoundMixer.SFX);
                PuzzleManager.Instance.PuzzleWin();
                isCompleted = true;
            }
        }
        
    }

}