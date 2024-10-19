using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class WirePuzzle : MonoBehaviour
    {
        public int fiosCertos = 2;
        public int fiosCertosCortados = 0;
        
        public Button[] fiosCorretos;
        public Button[] fiosErrados;
        
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

            GameManager.Instance.levelTimer += 2f;
            fiosCertosCortados++;
            if (fiosCertosCortados >= fiosCertos)
            {
                PuzzleComplete();
            }

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

        private void PuzzleComplete()
        {
            PuzzleManager.Instance.PuzzleWin();
            RemoveListeners();
            
        }



    }
}
