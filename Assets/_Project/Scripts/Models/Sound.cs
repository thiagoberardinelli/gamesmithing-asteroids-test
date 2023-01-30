using UnityEngine;

namespace _Project.Scripts.Models
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField] private string audioName;
        [SerializeField] private AudioClip clip;
        
        [Range(0F, 1F)]
        [SerializeField] private float volume;
        
        [Range(-3F, 3F)]
        [SerializeField] private float pitch;

        [SerializeField] private bool loop;

        public SoundType soundType;

        public AudioSource Source { get; set; }

        public string AudioName
        {
            get => audioName;
            set => audioName = value;
        }
        
        public AudioClip Clip
        {
            get => clip;
            set => clip = value;
        }

        public float Volume
        {
            get => volume;
            set => volume = value;
        }

        public float Pitch
        {
            get => pitch;
            set => pitch = value;
        }

        public bool Loop
        {
            get => loop;
            set => loop = value;
        }
    }
}
