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
        public string nextSceneName; 
        public float typingSpeed = 0.05f;
        public GameObject pressSpace;

        private int currentDialogueIndex;
        private bool isTyping;

        void Start()
        {
            StartCoroutine(TypeDialogue());
        }

        void Update()
        {
            pressSpace.SetActive(!isTyping);

            if (Input.GetKeyDown(KeyCode.Space) && !isTyping) 
            {
                AdvanceDialogue();
            }
        }

        IEnumerator TypeDialogue()
        {
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
            dialogueText.text = "Loading...";
            pressSpace.SetActive(false);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}