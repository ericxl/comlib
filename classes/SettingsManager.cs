#if !GAME_SERVER
using UnityEngine;

namespace VirtusArts
{
    public static class SettingsManager
    {
        static SettingsManager()
        {
            _sfxIsEnabled = PlayerPrefs.GetInt(Constant.SFX_KEY, 1).ToBoolean();
            _bgIsEnabled = PlayerPrefs.GetInt(Constant.BG_KEY, 1).ToBoolean();
            _cloudIsEnabled = PlayerPrefs.GetInt(Constant.CLOUD_KEY, 1).ToBoolean();
        }

        private static bool _sfxIsEnabled = false;
        public static bool SFXEnabled
        {
            get
            {
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
#endif
