using System;
using UnityEngine;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        public enum SoundMixer
        {
            MASTER,
            MUSIC,
            SFX
        }
        public static SoundManager Instance { get; private set; }

        public AudioSource MASTER_GROUP;
        public AudioSource MUSIC_GROUP;
        public AudioSource SFX_GROUP;
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
            
        }

        public void PlaySound(int clipIndex, SoundMixer audioSource)
        {
            
            Debug.Log($"Toquei som {clipIndex}");
            if (clipIndex >= 0 && clipIndex < soundClips.Length)
            {
                GetMixerSource(audioSource).clip = soundClips[clipIndex];
                GetMixerSource(audioSource).Play();
            }
            else
            {
                Debug.LogWarning("Índice de áudio fora do intervalo!");
            }
        }

        private AudioSource GetMixerSource(SoundMixer audioSource)
        {
            switch (audioSource)
            {
                case SoundMixer.MASTER:
                    return MASTER_GROUP;
                    break;
                case SoundMixer.MUSIC:
                    return MUSIC_GROUP;
                    break;
                case SoundMixer.SFX:
                    return SFX_GROUP;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(audioSource), audioSource, null);
            }
        }

        public void PlaySound(string clipName, SoundMixer audioSource)
        {
            Debug.Log($"Toquei som {clipName}");
            
            AudioClip clip = System.Array.Find(soundClips, sound => sound.name == clipName);
            if (clip != null)
            {
                GetMixerSource(audioSource).clip = clip;
                GetMixerSource(audioSource).Play();
            }
            else
            {
                Debug.LogWarning("Áudio não encontrado: " + clipName);
            }
        }
    }
}
