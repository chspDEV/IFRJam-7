using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        private AudioSource audioSource;
        public AudioClip[] soundClips;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(int clipIndex)
        {
            if (clipIndex >= 0 && clipIndex < soundClips.Length)
            {
                audioSource.clip = soundClips[clipIndex];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Índice de áudio fora do intervalo!");
            }
        }

        public void PlaySound(string clipName)
        {
            AudioClip clip = System.Array.Find(soundClips, sound => sound.name == clipName);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Áudio não encontrado: " + clipName);
            }
        }
    }
}
