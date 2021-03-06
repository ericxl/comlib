﻿#if !GAME_SERVER
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace VirtusArts.UI
{
    public class ButtonAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField]
        private Vector2 pressedScale = new Vector2(0.9f, 0.9f);

        [SerializeField]
        private Vector2 releasedScale = new Vector2(1.05f, 1.05f);

        [SerializeField]
        private float releaseTimer = 0.08f;

        [SerializeField]
        private AudioClip ClickSound;

        private Vector3 _originalScale;

        private void Start()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (pressedScale.x == 1 && pressedScale.y == 1)
            {
                return;
            }
            transform.localScale = new Vector3(pressedScale.x * _originalScale.x, pressedScale.y * _originalScale.y, _originalScale.z);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = _originalScale;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ClickSound)
            {
                SoundManager.PlaySFX(ClickSound);
            }
            if (releasedScale.x == 1 && releasedScale.y == 1)
            {
                transform.localScale = _originalScale;
                return;
            }
            StartCoroutine(ButtonReleaseAnimation());
        }

        private IEnumerator ButtonReleaseAnimation()
        {
            transform.localScale = new Vector3(releasedScale.x * _originalScale.x, releasedScale.y * _originalScale.y, _originalScale.z);
            yield return new WaitForSecondsRealtime(releaseTimer);
            transform.localScale = _originalScale;
        }
    }
}
#endif
