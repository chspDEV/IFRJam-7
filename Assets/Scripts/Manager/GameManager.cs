using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
    
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameState state;
        public GameState State { get; private set; }

        public float levelTimer = 60f;

        public int winnerEntitys = 0;

        private GameObject victoryHolder;
        private GameObject defeatHolder;
        private GameObject pauseHolder;
        private GameObject puzzles;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this); }
            else { Instance = this; }
        }

        private void Start()
        {
            //Definindo estado
            state = GameState.PLAY;

            //Achando GO
            puzzles = GameObject.Find("Puzzles");
            victoryHolder = GameObject.Find("VictoryHolder");
            defeatHolder = GameObject.Find("DefeatHolder");
            pauseHolder = GameObject.Find("PauseHolder");

            //Desativando por padr�o
            if (puzzles != null) { puzzles.SetActive(true); } else { Debug.LogError($"Objeto nao encontrado: {puzzles.name}"); }
            if (victoryHolder != null) { victoryHolder.SetActive(false); } else { Debug.LogError($"Objeto nao encontrado: {victoryHolder.name}"); }
            if (defeatHolder != null) { defeatHolder.SetActive(false); } else { Debug.LogError($"Objeto nao encontrado: {defeatHolder.name}"); }
            if (pauseHolder != null) { pauseHolder.SetActive(false); } else { Debug.LogError($"Objeto nao encontrado: {pauseHolder.name}"); }
        
        }

        private void Update()
        {
            HandleTimer();
            HandleStates();
            HandlePause();
            HandleMenuHolders();
        }

        private void HandleMenuHolders()
        {
            pauseHolder.SetActive(state == GameState.PAUSED);
            victoryHolder.SetActive(state == GameState.WIN);
            defeatHolder.SetActive(state == GameState.DEFEAT);
            puzzles.SetActive(state == GameState.PLAY);
        }

        public void SetState(GameState state)
        {
            this.state = state;
        }

        void SetTimeScale(float value)
        {
            Time.timeScale = value;
        }

        void HandleTimer()
        {
            if (levelTimer <= 0f)
            {
                SetState(GameState.DEFEAT);
            }
            else
            {
                levelTimer -= Time.deltaTime;
            }
        
        }

        void HandlePause()
        {
            bool condition_1 = Input.GetKeyDown(KeyCode.Escape);
            bool condition_2 = state != GameState.DEFEAT && state != GameState.WIN;

            if (condition_1 && condition_2)
            {
                if (state == GameState.PAUSED)
                {
                    SetState(GameState.PLAY);
                    return;
                }
                else
                {
                    SetState(GameState.PAUSED);
                    return;
                }
            }

        }

        public void NextLevel()
        {
            winnerEntitys++;

            if (winnerEntitys >= 2) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void HandleStates()
        {
            switch (state)
            {
                case GameState.PAUSED:
                    SetTimeScale(0f);
                    break;
                case GameState.DEFEAT:
                    SetTimeScale(.3f);
                    break;
                case GameState.WIN:
                    SetTimeScale(.25f);
                    break;
                case GameState.PLAY:
                    SetTimeScale(1f);
                    break;
            }
        }
    }

    [Serializable]
    public enum GameState
    {
        PAUSED,
        DEFEAT,
        WIN,
        PLAY
    }
}