using System.Collections;

namespace GameFeel
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class LevelAnnouncer : MonoBehaviour
    {
        [Header("Configuration")]
        public TextMeshProUGUI levelText;  
        public float displayDuration = 2f;  
        public Vector2 offset = new Vector2(20, -20);  
        public float fadeSpeed = 1f;  

        private CanvasGroup canvasGroup;

        void Start()
        {
            canvasGroup = levelText.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = levelText.gameObject.AddComponent<CanvasGroup>();
            }

            levelText.rectTransform.anchoredPosition = offset; 
            levelText.text = SceneManager.GetActiveScene().name;  
            StartCoroutine(ShowLevelText());
        }

        private IEnumerator ShowLevelText()
        {
            canvasGroup.alpha = 1; 

            
            yield return new WaitForSeconds(displayDuration);

            
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
                yield return null;
            }
            
            levelText.gameObject.SetActive(false);
        }
    }

}