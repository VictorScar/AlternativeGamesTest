using System;
using System.Collections;
using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    [Serializable]
    public class ScaleUIAnimator : UIAnimator
    {
        [SerializeField] private Vector2 targetScale = Vector2.one;
        private Vector2 _defaultScale;

        protected override void OnInit(UIView view)
        {
            base.OnInit(view);
            _defaultScale = view.Rect.sizeDelta;
        }

        protected override IEnumerator OnAnimation(UIView view)
        {
            Vector2 startScale = view.Rect.localScale;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;


                float t = Mathf.Clamp01(elapsed / duration);
                float easedT = GetEasedTime(t);
                view.Rect.localScale = Vector2.LerpUnclamped(startScale, targetScale, easedT);

                yield return null;
            }

            view.Rect.localScale = targetScale;
        }
    }
}