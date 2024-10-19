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
        private int fiosCorretosCortados = 0;
        private readonly int fiosCorretosFase1 = 2;
        private readonly string senhaCorretaFase1 = "batatasfritas";
        private readonly string ordemCorretaFase2 = "134972";

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

            if(fiosCorretosCortados >= fiosCorretosFase1) canWin = true;
        }

        public void CortarFioErrado()
        {
            GameManager.Instance.levelTimer -= 5f;
        }

        public void ChecarOrdem(string resposta)
        {
            if (resposta == ordemCorretaFase2) { canWin = true; }
        }

        public void ChecarSenha(string resposta)
        {
            if (resposta == senhaCorretaFase1) { canWin = true; }
        }

        public void AtivarPuzzle(int numero)
        {
            puzzleList[numero].SetActive(true);
        }

        public void Update()
        {
 
        }
    }
}