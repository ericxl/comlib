using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace VirtusArts.UI
{
    public abstract class CanvasManagerBase : DerivableSingleton<CanvasManagerBase>
    {
        protected virtual void Start()
        {
            var faderGo = new GameObject("Fader");
            faderGo.transform.SetParent(transform, false);
            var fader = faderGo.AddComponent<Image>();
            var rt = faderGo.GetComponent<RectTransform>();
            rt.offsetMin = rt.offsetMax = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;
            rt.anchorMax = new Vector2(1, 1);
            rt.anchorMin = new Vector2(0, 0);
            Transition(fader, FadeType.FadeOut);
        }

        private void Transition<T>(T fader, FadeType fadeType) where T : Graphic
        {
            StartCoroutine(FadeImage(fader, fadeType == FadeType.FadeIn ? Constant.FADING_IN_TRANSITION_TIME : Constant.FADING_OUT_TRANSITION_TIME, fadeType));
        }

        private static IEnumerator FadeImage<T>(T target, float duration, FadeType fadeType) where T : Graphic
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
