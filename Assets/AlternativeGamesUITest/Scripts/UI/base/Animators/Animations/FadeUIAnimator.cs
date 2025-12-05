using System;
using System.Collections;
using UnityEngine;

namespace AlternativeGamesTest.UI
{
    [Serializable]
    public class FadeUIAnimator : UIAnimator
    {
        [SerializeField, Range(0f, 1f)] private float targetAlpha = 1f;
   
        protected override IEnumerator OnAnimation(UIView view)
        {
            float startAlpha = view.CG.alpha;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float easedT = GetEasedTime(t);
                view.CG.alpha = Mathf.LerpUnclamped(startAlpha, targetAlpha, easedT);
                yield return null;
            }

            
        }

        protected override void OnAnimationEnded(UIView view)
        {
            base.OnAnimationEnded(view);
            view.CG.alpha = targetAlpha;
        }
    }
}