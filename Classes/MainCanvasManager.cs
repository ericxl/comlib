using UnityEngine;
using UnityEngine.UI;

namespace VirtusArts.UI
{
    public class MainCanvasManager : CanvasManagerBase
    {
        [SerializeField]
        private RectTransform OptionPanel;

        [SerializeField]
        private Toggle MusicToggle;
        [SerializeField]
        private Toggle SFXToggle;
        [SerializeField]
        private Toggle CloudToggle;

        public new static MainCanvasManager GetInstance()
        {
            return BaseInstance as MainCanvasManager;
        }

        protected override void Start()
        {
            base.Start();
            SFXToggle.isOn = PlayerPrefs.GetInt(Constant.SFX_KEY, 1).ToBoolean();
            MusicToggle.isOn = PlayerPrefs.GetInt(Constant.BG_KEY, 1).ToBoolean();
            CloudToggle.isOn = PlayerPrefs.GetInt(Constant.CLOUD_KEY, 1).ToBoolean();
        }


        public void StartButtonHandler()
        {
            GamePlayManager.GotoLevel("Level01");
        }

        private bool showOptionPanel;
        public void OptionButtonHandler()
        {
            SoundManager.PlaySFX("pagescroll");
            showOptionPanel = !showOptionPanel;
            //OptionPanel.GetComponent<EasyTween>().
            OptionPanel.GetComponent<Animator>().SetBool("Show", showOptionPanel);
        }

        public void SFXToggleHandler(Toggle toggle)
        {
            SettingsManager.SFXEnabled = toggle.isOn;
        }

        public void MusicToggleHandler(Toggle toggle)
        {
            SettingsManager.MusicEnabled = toggle.isOn;
        }

        public void CloudToggleHandler(Toggle toggle)
        {
            SettingsManager.CloudEnabled = toggle.isOn;
        }
    } 
}
