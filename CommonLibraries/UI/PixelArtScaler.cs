using System;
using UnityEngine;
using UnityEngine.UI;

namespace VirtusArts.UI
{

    [ExecuteInEditMode]
    public class PixelArtScaler : MonoBehaviour
    {
        private CanvasScaler scaler;

        [SerializeField]
        private JoyStick js;

        [SerializeField]
        private int RefenrenceHeight = 320;

        [SerializeField]
        private int RefenrenceWidth = 240;

        [SerializeField]
        private ScaleMode mode = ScaleMode.BaseOnHeight;

        private void Awake()
        {
            scaler = GetComponent<CanvasScaler>();
            UpdateScale();
        }

#if UNITY_EDITOR
        private void Update()
        {
            UpdateScale();
        }
#endif

        private void UpdateScale()
        {
            var sca = mode == ScaleMode.BaseOnHeight ? Mathf.Floor((float) Screen.height/(float) RefenrenceHeight) : Mathf.Floor((float)Screen.width / (float)RefenrenceWidth);
            sca = sca < 1 ? 1 : sca;

            if (scaler)
            {
                scaler.scaleFactor = (int)sca;
            }
            if (js)
            {
                js.PixelRatio = 1.0f / sca;
            }

        }
    }

    public enum ScaleMode
    {
        BaseOnHeight,
        BaseOnWidth
    }
}