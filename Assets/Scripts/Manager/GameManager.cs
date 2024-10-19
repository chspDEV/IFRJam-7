using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
    
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameState state;
        public GameState State { get; private set; }

        public float levelTimer = 60f;
        public Slider levelTimerSlider;
        public int winnerEntitys = 0;
        public Sprite doorOpen;
        
        private GameObject rightDoor;
        private GameObject leftDoor;
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
            leftDoor = GameObject.Find("Saida_L");
            rightDoor = GameObject.Find("Saida_R");

            //Tratamento
            if (puzzles == null) { Debug.LogError($"Objeto nao encontrado: puzzles"); }
            
            if (victoryHolder != null) { victoryHolder.SetActive(false); } 
            else { Debug.LogError($"Objeto nao encontrado: victoryHolder"); }
            
            if (defeatHolder != null) { defeatHolder.SetActive(false); } 
            else { Debug.LogError($"Objeto nao encontrado: defeatHolder"); }
            
            if (pauseHolder != null) { pauseHolder.SetActive(false); } else 
            { Debug.LogError($"Objeto nao encontrado: pause"); }
            
            if (leftDoor == null) { Debug.LogError($"Objeto nao encontrado: porta_L"); }
            if (rightDoor == null) { Debug.LogError($"Objeto nao encontrado: porta_R"); }
        }

        private void Update()
        {
            HandleTimer();
            HandleStates();
            HandlePause();
            HandleMenuHolders();
            HandleChangeDoors();
        }

        private void HandleMenuHolders()
        {
            pauseHolder.SetActive(state == GameState.PAUSED);
            victoryHolder.SetActive(state == GameState.WIN);
            defeatHolder.SetActive(state == GameState.DEFEAT);
            puzzles.SetActive(state == GameState.PLAY);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void HandleChangeDoors()
        {
            if (!PuzzleManager.Instance.CheckVictory()) return;
            
            leftDoor.GetComponent<SpriteRenderer>().sprite = doorOpen;
            rightDoor.GetComponent<SpriteRenderer>().sprite = doorOpen;
        }

        public void SetState(GameState _state)
        {
            this.state = _state;
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
                levelTimerSlider.value = levelTimer;
            }
        
        }

        void HandlePause()
        {
            bool condition1 = Input.GetKeyDown(KeyCode.Escape);
            bool condition2 = state != GameState.DEFEAT && state != GameState.WIN;

            if (!condition1 || !condition2) return;

            SetState(state == GameState.PAUSED ? GameState.PLAY : GameState.PAUSED);
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