using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameState state;
        public GameState State { get {return state;} }
        
        [Header("LevelVariables")]
        public float levelMaxTimer = 60f;
        [HideInInspector] public float levelTimer;
        public int winnerEntitys;
        
        [Header("Sprites")]
        public Image levelTimerImage;
        public Sprite doorOpen;

        [Header("Transform References")] 
        public RectTransform mainCanvas;
        public RectTransform cameraMask;
        public Transform mainCameraTransform;
        public Transform scenaryHolder;
        public Transform Dog, Human;
        
        private GameObject rightDoor, leftDoor, victoryHolder, defeatHolder, pauseHolder, puzzles;
        private float mainCameraSize;
        private Camera mainCamera;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this); }
            else { Instance = this; }

            levelTimer = levelMaxTimer;
            mainCamera = mainCameraTransform.gameObject.GetComponent<Camera>();
            mainCameraSize = mainCamera.orthographicSize;
        }

        private void Start()
        {
            //Definindo estado
            state = GameState.PLAY;

            //Achando GO
            puzzles = mainCanvas.Find("Puzzles").gameObject;
            
            Transform Essentials = mainCanvas.Find("Essentials");
            
            victoryHolder = Essentials.Find("VictoryHolder").gameObject;
            defeatHolder = Essentials.Find("DefeatHolder").gameObject;
            pauseHolder = Essentials.Find("PauseHolder").gameObject;
            
            leftDoor = scenaryHolder.Find("Saida_L").gameObject;
            rightDoor = scenaryHolder.Find("Saida_R").gameObject;

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

            StartCoroutine(CameraTransition());
        }

        private void Update()
        {
            HandleTimer();
            HandleStates();
            HandlePause();
            HandleMenuHolders();
            HandleChangeDoors();
        }

        private IEnumerator CameraTransition()
        {
            yield return new WaitForSecondsRealtime(3f);

            while (mainCameraSize > 6f)
            {
                mainCameraSize -= 0.01f;
                mainCamera.orthographicSize = mainCameraSize;
                
                yield return null;
            }

            cameraMask.gameObject.SetActive(true);

            yield return null;
        }

        private void HandleMenuHolders()
        {
            pauseHolder.SetActive(state == GameState.PAUSED);
            victoryHolder.SetActive(state == GameState.WIN);
            defeatHolder.SetActive(state == GameState.DEFEAT);
            puzzles.SetActive(state == GameState.PUZZLE);
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
                levelTimerImage.fillAmount = levelTimer / levelMaxTimer;
            }
        
        }

        void HandlePause()
        {
            bool condition1 = Input.GetKeyDown(KeyCode.Escape);
            bool condition2 = state != GameState.DEFEAT && state != GameState.WIN;

            if (!condition1 || !condition2) return;

            SetState(state == GameState.PAUSED ? GameState.PLAY : GameState.PAUSED);
        }

        /// <summary>
        /// Esta função entra na porta e checa qual entidade vai desativar
        /// </summary>
        /// <param name="entityToDeactiveIndex">0 = Dog // 1 = Human.</param>
        public void EnterDoor(int entityToDeactiveIndex)
        {
            switch (entityToDeactiveIndex)
            {
                case 0:
                    Dog.gameObject.SetActive(false);
                    cameraMask.Find("Dog").gameObject.SetActive(false);
                    break;
                case 1:
                    Human.gameObject.SetActive(false);
                    cameraMask.Find("Human").gameObject.SetActive(false);
                    break;
            }
            
            winnerEntitys++;
            if (winnerEntitys >= 2) { SetState(GameState.WIN); }
        }

        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                case GameState.PUZZLE:
                    SetTimeScale(0.9f);
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
        PLAY,
        PUZZLE
    }
}