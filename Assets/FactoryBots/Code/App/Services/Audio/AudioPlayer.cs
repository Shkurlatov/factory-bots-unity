using UnityEngine;

namespace FactoryBots.App.Services.Audio
{
    public class AudioPlayer : MonoBehaviour, IAppAudio
    {
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _effectSource;

        [SerializeField] private AudioClip _exampleSoundClip;
        [SerializeField] private AudioClip _exampleEffectClip;

        private void Awake() => 
            DontDestroyOnLoad(gameObject);

        public void PlayExampleSound() => 
            _soundSource.PlayOneShot(_exampleSoundClip);

        public void PlayExampleEffect() => 
            _effectSource.PlayOneShot(_exampleEffectClip);

        public void Dispose() { }
    }
}
