using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Models;
using UnityEngine;

namespace _Project.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public List<Sound> sounds;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else 
                Destroy(this);
        }

        private void Start() => Setup();

        private void Setup()
        {
            foreach (Sound sound in Instance.sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();

                sound.Source.clip = sound.Clip;
                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.Loop;
            }
        }
        
        public void PlaySound(string audioName)
        {
            try
            {
                GetAudioSource(audioName).Play();
            }
            catch (System.Exception)
            {
                Debug.LogError(audioName + "Could not play the");
            }
        }

        private AudioSource GetAudioSource(string audioName)
        {
            Sound sound = sounds.FirstOrDefault(x => x.AudioName == audioName);

            if (sound != null) return sound.Source;
            Debug.Log("Could not find the audio in AudioManager list:" + audioName);
            return null;
        }
    }
}