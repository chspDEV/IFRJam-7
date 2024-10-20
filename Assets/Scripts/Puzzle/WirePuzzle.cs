using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class WirePuzzle : MonoBehaviour
    {
        public int fiosCertos = 2;
        public int fiosCertosCortados;
        
        public Button[] fiosCorretos;
        public Button[] fiosErrados;
        private bool isCompleted;
        private void Awake()
        {
            
            foreach (Button button in fiosCorretos)
            {
                button.onClick.AddListener(() => CortarFioCerto(button));
            }
            
            foreach (Button button in fiosErrados)
            {
                button.onClick.AddListener(() => CortarFioErrado(button));
            }
        }
        
        public void CortarFioCerto(Button btn)
        {
            if (btn != null) {btn.GetComponent<Wire>()?.Cortar();}
            else
            {
                Debug.Log("Nao foi possivel achar componente Wire " + btn.name);
            }

            GameManager.Instance.levelTimer += 5f;
            fiosCertosCortados++;
            if (fiosCertosCortados >= fiosCertos)
            {
                PuzzleCompleted();
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

        public void CortarFioErrado(Button btn)
        {
            if (btn != null) {btn.GetComponent<Wire>()?.Cortar();}
            else
            {
                Debug.Log("Nao foi possivel achar componente Wire " + btn.name);
            }
            
            GameManager.Instance.levelTimer -= 5f;
        }

        private void RemoveListeners()
        {
            foreach (Button button in fiosCorretos)
            {
                button.onClick.RemoveAllListeners();
            }
            
            foreach (Button button in fiosErrados)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        void PuzzleCompleted()
        {
            if (!isCompleted)
            {
                PuzzleManager.Instance.PuzzleWin();
                RemoveListeners();
                isCompleted = true;
            }
        }



    }
}
