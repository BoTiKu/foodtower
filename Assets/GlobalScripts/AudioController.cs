using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TowerDefense
{
    public class AudioController : MonoSingleton<AudioController>
    {
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField, Range(.0f, 1.0f)]
        private float _voloume;
        [SerializeField]
        private AudioSource _soundPrefab;
        [SerializeField]
        private AudioClip _onClikSound;
        [SerializeField]
        private AudioClip _successSound;
        [SerializeField]
        private AudioClip _failedSound;

        private List<AudioSource> _sounds = new();

        private void Start()
        {
            _audioSource.volume = _voloume;
        }

        public void SetVolume(float volume)
        {
            volume = volume > 1 ? 1 : volume;
            volume = volume < 0 ? 0 : volume;
            _voloume = volume;

            _sounds.ForEach(sound => sound.volume = _voloume);
            _audioSource.volume = _voloume;
        }

        public void SetMusic(AudioClip clip, bool loop = true)
        {
            _audioSource.clip = clip;
            _audioSource.loop = loop;
            _audioSource.Play();
        }

        public void PlayClick() =>  PlaySound(_onClikSound);

        public void PlaySuccess() => PlaySound(_successSound);

        public void PlayFailed() => PlaySound(_failedSound);

        public void PlaySound(AudioClip sound)
        {
            var source = Instantiate(_soundPrefab);
            _sounds.Add(source);
            source.volume = _voloume;
            source.clip = sound;
            source.loop = false;
            source.Play();
        }

        private void Update()
        {
            RemoveSources();
        }

        private void RemoveSources()
        {
            var length = _sounds.Count;
            _sounds.RemoveAll(sound => sound == null);
            var needDestroy = _sounds.Where(sound => !sound.isPlaying).Select(obj => obj.gameObject).ToList();
            for (int i = 0; i < needDestroy.Count; i++)
            {
                    Destroy(needDestroy[i]);
            }
        }
    }
}