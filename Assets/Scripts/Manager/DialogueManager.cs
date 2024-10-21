using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText; 
        public string[] dialogues;
        public GameObject[] cutsceneImages;
        public string nextSceneName; 
        public float typingSpeed = 0.05f;
        public GameObject pressE;

        private int currentDialogueIndex;
        private bool isTyping;

        void Start()
        {
            StartCoroutine(TypeDialogue());
        }

        void Update()
        {
            pressE.SetActive(!isTyping);

            if (Input.GetKeyDown(KeyCode.E) && !isTyping) 
            {
                AdvanceDialogue();
            }
        }

        IEnumerator TypeDialogue()
        {
            cutsceneImages[currentDialogueIndex].SetActive(true);
            isTyping = true;
            dialogueText.text = "";

            foreach (char letter in dialogues[currentDialogueIndex].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;
        }

        void AdvanceDialogue()
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Length)
            {
                StartCoroutine(TypeDialogue());
            }
            else
            {
                LoadNextScene();
            }
        }

        void LoadNextScene()
        {
            dialogueText.text = "...";
            pressE.SetActive(false);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}