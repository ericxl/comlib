using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace VirtusArts.UI
{
    public abstract class CanvasManagerBase : DerivableSingleton<CanvasManagerBase>
    {
        protected virtual void Start()
        {
            FadeOut(InitializeFader().GetComponent<Image>(), Constant.FADING_OUT_TRANSITION_TIME);
        }

        protected GameObject InitializeFader()
        {
            var faderGo = new GameObject("Fader");
            faderGo.transform.SetParent(transform, false);
            var fader = faderGo.AddComponent<Image>();
            fader.color = new Color(0,0,0,1);
            var rt = faderGo.GetComponent<RectTransform>();
            rt.offsetMin = rt.offsetMax = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;
            rt.anchorMax = new Vector2(1, 1);
            rt.anchorMin = new Vector2(0, 0);
            return faderGo;
        }

        protected void FadeOut<T>(T fader, float duration) where T : Graphic
        {
            StartCoroutine(FadeImage(fader, FadeType.FadeOut, duration));
        }

        private IEnumerator FadeImage<T>(T target, FadeType fadeType, float duration) where T : Graphic
        {
            if (target == null) yield break;

            float startAlpha = fadeType == FadeType.FadeIn ? 0 : 1;
            target.color = new Color(0, 0, 0, startAlpha);
            var finalColor = new Color(0, 0, 0, startAlpha >= 0 ? 0 : 1);

            for (var t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / duration)
            {
                target.color = new Color(finalColor.r, finalColor.g, finalColor.b, Mathf.SmoothStep(startAlpha, finalColor.a, t));
                yield return null;
            }

            target.raycastTarget = fadeType != FadeType.FadeOut;
            Destroy(target.gameObject);
        }
    }

    public enum FadeType
    {
        FadeIn,
        FadeOut
    }
}
