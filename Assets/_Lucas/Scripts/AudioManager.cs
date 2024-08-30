using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lucas
{
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
        /// Sets the global volume of this player's audio source.
        /// </summary>
        public void SetVolume(float volume)
        {
            _audioVolume = volume;
        }

    }
}
