using Manager;
using UnityEngine;

namespace Puzzle
{
    public class Note:MonoBehaviour
    {
        void OnDisable()
        {
            QuitPuzzle();
        }
        
        public void QuitPuzzle()
        {
            if(GameManager.Instance.State == GameState.PUZZLE)
            GameManager.Instance.SetState(GameState.PLAY);
        }
    }
}