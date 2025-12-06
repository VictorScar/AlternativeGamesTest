using System;
using System.Collections;
using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    [Serializable]
    public abstract class UIAnimator
    {
        [SerializeField] protected float duration = 0.3f;

        [Header("Easing Settings")] [SerializeField]
        protected EaseType easeType = EaseType.OutQuad;

        protected Coroutine _animation;

        public void Init(UIView view)
        {
            OnInit(view);
        }

        protected virtual void OnInit(UIView view)
        {
        }

        public Coroutine Animate(UIView view, Action onComplete = null)
        {
            Cancel(view);
            if(view.gameObject.activeInHierarchy) _animation = view.StartCoroutine(AnimateInternal(view, onComplete));
            return _animation;
        }

        public void AnimateImmediately(UIView view, Action onComplete = null)
        {
            OnAnimationEnded(view);
            onComplete?.Invoke();
        }

        public void Cancel(UIView view)
        {
            if (_animation != null)
            {
                view.StopCoroutine(_animation);
                _animation = null;
            }
        }

        protected abstract IEnumerator OnAnimation(UIView view);

        protected virtual void OnAnimationEnded(UIView view)
        {
            _animation = null;
        }

        protected float GetEasedTime(float t)
        {
            return EaseFunctions.Evaluate(easeType, t);
        }

        private IEnumerator AnimateInternal(UIView view, Action onComplete)
        {
            yield return OnAnimation(view);
            OnAnimationEnded(view);
            onComplete?.Invoke();
        }
    }
}