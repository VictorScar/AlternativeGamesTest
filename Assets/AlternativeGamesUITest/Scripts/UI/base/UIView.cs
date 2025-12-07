using System.Collections.Generic;
using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] protected RectTransform rect;
        [SerializeField] protected CanvasGroup cg;

        [SerializeReference] protected UIAnimator showAnimator;
        [SerializeReference] protected UIAnimator hideAnimator;


        public RectTransform Rect => rect;
        public CanvasGroup CG => cg;

        public void Init(UIAnimationsRunner runner)
        {
            showAnimator?.Init(this, runner);
            hideAnimator?.Init(this, runner);
            OnInit(runner);
        }

 
        public void Show(bool immediately = false)
        {
            gameObject.SetActive(true);
            hideAnimator?.Cancel();

            if (!immediately)
            {
                showAnimator?.Animate(this, OnShow);
            }
            else
            {
                showAnimator?.AnimateImmediately(this, OnShow);
            }
        }

  
        public void Hide(bool immediately = false)
        {
            showAnimator?.Cancel();

            if (!immediately)
            {
                hideAnimator?.Animate(this, OnHide);
            }
            else
            {
                hideAnimator?.AnimateImmediately(this, OnHide);
            }
        }

        protected virtual void OnInit(UIAnimationsRunner runner)
        {
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
            gameObject.SetActive(false);
        }

        [Button("Show")]
        protected void TestShow()
        {
            Show();
        }

        [Button("Hide")]
        protected void TestHide()
        {
            Hide();
        }
    }
}