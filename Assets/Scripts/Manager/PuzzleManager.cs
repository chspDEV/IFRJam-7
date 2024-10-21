using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance { get; private set; }

        public string currentScene;
        private bool canWin;
        [SerializeField] private int conditionsToWin;
        [SerializeField] private int currentConditions;
        
        public List<GameObject> puzzleList;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this); }
            else { Instance = this; }

            currentScene = SceneManager.GetActiveScene().name;
            canWin = false;
            if(conditionsToWin == 0) conditionsToWin = 1;
        }
        
        public void ControlarPuzzle(int numero, bool state)
        {
            GameManager.Instance.SetState(state ? GameState.PUZZLE : GameState.PLAY);

            puzzleList[numero].SetActive(state);
        }

        public bool CheckVictory()
        {
            if (currentConditions >= conditionsToWin)
            {
                canWin = true;
            }
            
            return canWin;
        }

        public void PuzzleWin()
        {
            GameManager.Instance.levelTimer += 3f;
            currentConditions++;
            CheckVictory();
        }
        
    }
}