using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance { get; private set; }

        public string currentScene;
        public bool canWin = false;
        public int conditionsToWin = 2;
        public int currentConditions = 0;
        
        
        //Fase 1
        private int fiosCorretosCortados = 0;
        private readonly int fiosCorretosFase1 = 2;
        private readonly string senhaCorretaFase1 = "batatasfritas";
        
        //Fase 2
        private readonly string ordemCorretaFase2 = "134972";
        
        //Fase 3

        public List<GameObject> puzzleList;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this); }
            else { Instance = this; }

            currentScene = SceneManager.GetActiveScene().name;
            canWin = false;
        }

        public void CortarFioCerto()
        { 
            fiosCorretosCortados    ++;
            GameManager.Instance.levelTimer += 2f;

            if (fiosCorretosCortados >= fiosCorretosFase1)
            {
                conditionsToWin++;
                CheckVictory();
            }
        }

        public void CortarFioErrado()
        {
            GameManager.Instance.levelTimer -= 5f;
        }

        public void ChecarOrdem(string resposta)
        {
            if (resposta == ordemCorretaFase2)
            {
                GameManager.Instance.levelTimer += 2f;
                conditionsToWin++;
                CheckVictory();
                return;
            }
            
            GameManager.Instance.levelTimer -= 5f;
        }

        public void ChecarSenha(string resposta)
        {
            if (resposta == senhaCorretaFase1)
            {
                GameManager.Instance.levelTimer += 2f;
                conditionsToWin++;
                CheckVictory();
                return;
            }
            
            GameManager.Instance.levelTimer -= 5f;
            
        }

        public void AtivarPuzzle(int numero)
        {
            puzzleList[numero].SetActive(true);
        }

        public bool CheckVictory()
        {
            if (currentConditions >= conditionsToWin)
            {
                canWin = true;
            }
            
            return canWin;
        }

        public string ObterOrdem()
        {
            return ordemCorretaFase2;
        }

        public void Update()
        {
            switch (currentScene)
            {
                default:
                    conditionsToWin = 2;
                    break;
            }
        }
    }
}