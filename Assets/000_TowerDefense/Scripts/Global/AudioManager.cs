using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RushHour
{

    public enum SoundEffect
    {
        InsufficientFunds = 0, InvalidPlacement = 1, Chaching = 2
    }

    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {

        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
                }
                return _instance;
            }
        }

        [Header("Cached Sound Effects")]
        [SerializeField] private AudioClip _insufficientFundsSFX;
        [SerializeField] private AudioClip _invalidPlacementSFX;
        [SerializeField] private AudioClip _chachingSFX;

        private AudioSource _audioSource;
        private float _audioVolume = 1;

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(this);
            }
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Plays a specified audio clip at the specified volume.
        /// Can be overlayed with other sounds.
        /// </summary>
        public void PlayOneShot(AudioClip sfx, float volume = 1)
        {
            _audioSource.PlayOneShot(sfx, volume * _audioVolume);
        }

        /// <summary>
        /// Some audio is cached so we don't have to keep assigning
        /// the same audio clips. Use the `AudioType` enum instead to
        /// do this.
        /// </summary>
        public void PlayOneShot(SoundEffect sfx, float volumeScale = 1)
        {
            switch (sfx)
            {
                case SoundEffect.InsufficientFunds:
                    _audioSource.PlayOneShot(_insufficientFundsSFX, 0.6f * volumeScale);
                    break;
                case SoundEffect.InvalidPlacement: 
                    _audioSource.PlayOneShot(_invalidPlacementSFX, 0.6f * volumeScale);
                    break;
                case SoundEffect.Chaching:
                    _audioSource.PlayOneShot(_chachingSFX, 0.4f * volumeScale);
                    break;
            }
        }

        /// <summary>
        /// Sets the global volume of this player's audio source.
        /// </summary>
        public void SetVolume(float volume)
        {
            _audioVolume = volume;
        }

    }
}
