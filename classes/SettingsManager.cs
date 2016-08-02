using UnityEngine;

namespace VirtusArts
{
    public static class SettingsManager
    {
        private static bool loaded = false;
        private static void Load()
        {
            if (loaded) return;
            _sfxIsEnabled = PlayerPrefs.GetInt(Constant.SFX_KEY, 1).ToBoolean();
            _bgIsEnabled = PlayerPrefs.GetInt(Constant.BG_KEY, 1).ToBoolean();
            _cloudIsEnabled = PlayerPrefs.GetInt(Constant.CLOUD_KEY, 1).ToBoolean();
            loaded = true;
        }

        private static bool _sfxIsEnabled = false;
        public static bool SFXEnabled
        {
            get
            {
                if (!loaded) Load();
                return _sfxIsEnabled;
            }
            set
            {
                SoundManager.SFXEnabled = value;
                _sfxIsEnabled = value;
                PlayerPrefs.SetInt(Constant.SFX_KEY, value.ToInt());
            }
        }

        private static bool _bgIsEnabled = false;
        public static bool MusicEnabled
        {
            get
            {
                if (!loaded) Load();

                return _bgIsEnabled;
            }
            set
            {
                SoundManager.MusicEnabled = value;
                _bgIsEnabled = value;
                PlayerPrefs.SetInt(Constant.BG_KEY, value.ToInt());
            }
        }

        private static bool _cloudIsEnabled = true;
        public static bool CloudEnabled
        {
            get
            {
                if (!loaded) Load();
                return _cloudIsEnabled;
            }
            set
            {
                _cloudIsEnabled = value;
                PlayerPrefs.SetInt(Constant.CLOUD_KEY, value.ToInt());
            }
        }
    }
}
