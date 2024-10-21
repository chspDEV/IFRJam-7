using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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

        private void Start()
        {
            //Tocando OSTs
            switch (SceneManager.GetActiveScene().name)
            {
                case "Menu Principal":
                    SoundManager.Instance.PlaySound("Menu Song", SoundManager.SoundMixer.MUSIC);
                    break;
                case "Roubatorio de Bytes":
                    SoundManager.Instance.PlaySound("Ambiente", SoundManager.SoundMixer.MUSIC);
                    break;
                case "Corredor if else":
                    SoundManager.Instance.PlaySound("Ambiente", SoundManager.SoundMixer.MUSIC);
                    break;
                case "private SalaDoChefe":
                    SoundManager.Instance.PlaySound("BossIFRJAM", SoundManager.SoundMixer.MUSIC);
                    break;
            }
        }


        public void StopAllSounds()
        {
            MASTER_GROUP.Stop();
            MUSIC_GROUP.Stop();
            SFX_GROUP.Stop();
        }

        private AudioSource GetMixerSource(SoundMixer desiredGroup)
        {
            return desiredGroup switch
            {
                SoundMixer.MASTER => MASTER_GROUP,
                SoundMixer.MUSIC => MUSIC_GROUP,
                SoundMixer.SFX => SFX_GROUP,
                _ => throw new ArgumentOutOfRangeException(nameof(desiredGroup), desiredGroup, null)
            };
        }

        //POR STRING
        public void PlaySound(string clipName, SoundMixer _group)
        {
            Debug.Log($"Toquei som {clipName}");
            
            AudioClip clip = System.Array.Find(soundClips, sound => sound.name == clipName);
            if (clip != null)
            {
                GetMixerSource(_group).clip = clip;
                GetMixerSource(_group).pitch = Random.Range(0.9f, 1.2f);
                GetMixerSource(_group).Play();
            }
            else
            {
                Debug.LogWarning("Áudio não encontrado: " + clipName);
            }
        }
        
        //POR INDEX
        public void PlaySound(int clipIndex, SoundMixer _group)
        {
            
            Debug.Log($"Toquei som {clipIndex}");
            if (clipIndex >= 0 && clipIndex < soundClips.Length)
            {
                GetMixerSource(_group).clip = soundClips[clipIndex];
                GetMixerSource(_group).pitch = Random.Range(0.95f, 1.05f);
                GetMixerSource(_group).Play();
            }
            else
            {
                Debug.LogWarning("Índice de áudio fora do intervalo!");
            }
        }
        
    }
}
