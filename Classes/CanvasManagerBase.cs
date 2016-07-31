﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace VirtusArts.UI
{
    public abstract class CanvasManagerBase<T> : Singleton<T> where T: MonoBehaviour
    {
        [SerializeField]
        private Image Fader;

        protected virtual void Start()
        {
            StartCoroutine(FadeImage(Fader, Constant.FADING_IN_TRANSITION_TIME, FadeType.FadeOut));
        }

        public IEnumerator FadeImage(Image target, float duration, FadeType fadeType)
        {
            if (target == null)
            {
                yield break;
            }
            if (!target.enabled)
            {
                target.enabled = true;
            }

            float alpha = 1;
            if (fadeType == FadeType.FadeIn)
            {
                alpha = 0;
            }
            target.color = new Color(0, 0, 0, alpha);


            var color = fadeType == FadeType.FadeIn ? new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0);

            for (var t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / duration)
            {
                var newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
                target.color = newColor;
                yield return null;
            }

            target.color = color;
            if (fadeType == FadeType.FadeOut)
            {
                Fader.raycastTarget = false;
            }
        }

        public enum FadeType
        {
            FadeIn,
            FadeOut
        }
    } 
}
