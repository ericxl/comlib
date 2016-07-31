using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Audio;

namespace VirtusArts
{
    public class SoundManager : PersistentSingleton<SoundManager>
    {
        [SerializeField]
        private AudioMixer mixer;

        private AudioMixerSnapshot paused;
        private AudioMixerSnapshot unpaused;
        private AudioMixerGroup sfxGroup;
        private AudioMixerGroup musicGroup;

        private bool sfxIsEnabled;
        private bool bgIsEnabled;

        private AudioSource musicSource;

        private bool _in_fading = false;

        protected override void Awake()
        {
            base.Awake();

            sfxGroup = mixer.FindMatchingGroups("SFX")[0];
            musicGroup = mixer.FindMatchingGroups("Background")[0];
            unpaused = mixer.FindSnapshot("Unpaused");
            paused = mixer.FindSnapshot("Paused");

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.loop = true;
            musicSource.outputAudioMixerGroup = musicGroup;
        }

        protected void Start()
        {
            SFXEnabled = SettingsManager.SFXEnabled;
            MusicEnabled = SettingsManager.MusicEnabled;
        }

        private void Crossfade(AudioClip clip, float fadeTime)
        {
            if (_in_fading) return;
            Instance.StartCoroutine(Instance.CrossfadeCo(clip, fadeTime));
            _in_fading = true;
        }

        private IEnumerator CrossfadeCo(AudioClip newClip, float fadeTime)
        {
            var t = 0.0f;

            var initialVolume = musicSource.volume;

            while (t < fadeTime)
            {
                musicSource.volume = Mathf.Lerp(initialVolume, 0.0f, t / fadeTime);


                t += Time.unscaledDeltaTime;
                yield return null;
            }

            musicSource.clip = newClip;
            musicSource.volume = 1;
            musicSource.Play();
            _in_fading = false;
        }

        private void PlayClip(AudioClip clip)
        {
            if (!sfxIsEnabled || clip == null) return;

            var tempGo = new GameObject("One Shot Audio");
            var asource = tempGo.AddComponent<AudioSource>();
            asource.outputAudioMixerGroup = sfxGroup;

            asource.spatialBlend = 0.0f;

            DontDestroyOnLoad(tempGo);
            asource.clip = clip;
            asource.Play();
            Destroy(tempGo, clip.length);
        }

        public bool SFXEnabled
        {
            get
            {
                return sfxIsEnabled;
            }
            set
            {
                sfxIsEnabled = value;
                mixer.SetFloat("sfxVol", sfxIsEnabled ? 0.0f : -80.0f);
            }
        }

        public bool MusicEnabled
        {
            get
            {
                return bgIsEnabled;
            }
            set
            {
                bgIsEnabled = value;
                mixer.SetFloat("musicVol", bgIsEnabled ? 0.0f : -80.0f);
            }
        }

        public void PlaySoundtrack(AudioClip clip)
        {
            if (musicSource.clip == clip || clip == null)
            {
                return;
            }

            if (musicSource.clip == null)
            {
                musicSource.clip = clip;
                musicSource.Play();
                return;
            }

            Crossfade(clip, Constant.BACKGROUND_MUSIC_TRANSITION_TIME);
        }

        public void PlaySFX(string sfxName)
        {

            var clip = Resources.Load<AudioClip>(sfxName);
            PlayClip(clip);
        }

        public void PlaySFX(AudioClip clip)
        {
            PlayClip(clip);
        }

        public void PlaySFX(IList<AudioClip> clips)
        {
            if (clips == null || !clips.Any()) return;
            PlaySFX(clips[new System.Random().Next(clips.Count)]);
        }

        public void TransitionToPaused()
        {
            mixer.TransitionToSnapshots(new[] { paused }, new[] { 1.0f }, 0);

        }

        public void TransitionToUnpaused()
        {
            mixer.TransitionToSnapshots(new[] { unpaused }, new[] { 1.0f }, 0);
        }
    }

}
