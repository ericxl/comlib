using UnityEngine;
using UnityEngine.UI;

namespace VirtusArts
{

    [ExecuteInEditMode]
    public class PixelArtScaler : MonoBehaviour
    {
        private CanvasScaler scaler;

        [SerializeField] private JoyStick js;

        [SerializeField] private int RefenrenceHeight = 320;

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
            var sca = Mathf.Floor((float)Screen.height / (float)RefenrenceHeight);
            if (sca < 1)
            {
                sca = 1;
            }
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

}