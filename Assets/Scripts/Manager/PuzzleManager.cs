using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance { get; private set; }

        public string currentScene;
        private bool canWin = false;
        [SerializeField] private int conditionsToWin = 2;
        [SerializeField] private  int currentConditions = 0;
        
        public List<GameObject> puzzleList;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this); }
            else { Instance = this; }

            currentScene = SceneManager.GetActiveScene().name;
            canWin = false;
        }
        
        public void ControlarPuzzle(int numero, bool state)
        {
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
            currentConditions++;
            CheckVictory();
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