using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GoToScene : MonoBehaviour
    {
        public string sceneName;

        public void GoScene()
        { 
            SceneManager.LoadScene(sceneName);
        }
    }
}
