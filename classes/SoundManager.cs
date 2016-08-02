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

        private static bool sfxIsEnabled;
        private static bool bgIsEnabled;
        private static bool _in_fading = false;

        private AudioSource musicSource;

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

        public static bool SFXEnabled
        {
            get
            {
                return sfxIsEnabled;
            }
            set
            {
                sfxIsEnabled = value;

                Instance.mixer.SetFloat("sfxVol", sfxIsEnabled ? 0.0f : -80.0f);
            }
        }

        public static bool MusicEnabled
        {
            get
            {
                return bgIsEnabled;
            }
            set
            {
                bgIsEnabled = value;
                Instance.mixer.SetFloat("musicVol", bgIsEnabled ? 0.0f : -80.0f);
            }
        }

        public static void PlaySoundtrack(AudioClip clip)
        {
            if (Instance.musicSource.clip == clip || clip == null)
            {
                return;
            }

            if (Instance.musicSource.clip == null)
            {
                Instance.musicSource.clip = clip;
                Instance.musicSource.Play();
                return;
            }

            Instance.Crossfade(clip, Constant.BACKGROUND_MUSIC_TRANSITION_TIME);
        }

        public static void PlaySFX(string sfxName)
        {

            var clip = Resources.Load<AudioClip>(sfxName);
            Instance.PlayClip(clip);
        }

        public static void PlaySFX(AudioClip clip)
        {
            Instance.PlayClip(clip);
        }

        public static void PlaySFX(IList<AudioClip> clips)
        {
            if (clips == null || !clips.Any()) return;
            PlaySFX(clips[new System.Random().Next(clips.Count)]);
        }

        public static void TransitionToPaused()
        {
            Instance.mixer.TransitionToSnapshots(new[] { Instance.paused }, new[] { 1.0f }, 0);

        }

        public static void TransitionToUnpaused()
        {
            Instance.mixer.TransitionToSnapshots(new[] { Instance.unpaused }, new[] { 1.0f }, 0);
        }
    }

}
