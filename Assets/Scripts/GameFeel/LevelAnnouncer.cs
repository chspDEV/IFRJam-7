using System.Collections;
using Manager;

namespace GameFeel
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class LevelAnnouncer : MonoBehaviour
    {
        [Header("References")]
        public TextMeshProUGUI levelText;  
        public CanvasGroup canvasGroup;
        
        [Header("Configs")]
        public float displayDuration = 2f;  
        public Vector2 offset = new Vector2(20, -20);  
        public float fadeSpeed = 1f;  

        void Start()
        {
            levelText.rectTransform.anchoredPosition = offset; 
            levelText.text = SceneManager.GetActiveScene().name;  
            StartCoroutine(ShowLevelText());

            //Tocando OSTs
            switch (SceneManager.GetActiveScene().name)
            {
                case "Menu Principal":
                    SoundManager.Instance.PlaySound("Menu Song", SoundManager.SoundMixer.MUSIC);
                    break;
                case "Roubatorio de Bytes":
                    SoundManager.Instance.PlaySound("Ambiente", SoundManager.SoundMixer.MUSIC);
                    break;
                case "Corredor da [Deserialização]":
                    SoundManager.Instance.PlaySound("Ambiente", SoundManager.SoundMixer.MUSIC);
                    break;
                case "private SalaDoChefe":
                    SoundManager.Instance.PlaySound("BossIFRJAM", SoundManager.SoundMixer.MUSIC);
                    break;
            }
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
            
            canvasGroup.gameObject.SetActive(false);
        }
    }

}