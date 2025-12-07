using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AlternativeGamesTest.UI.Base
{
    public class UIClickableView : UIView, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private bool isInteractable = true;
        [SerializeReference] private UIAnimator pointerDownAnimator;
        [SerializeReference] private UIAnimator pointerUpAnimator;

        public event Action onClick;

        protected override void OnInit(UIAnimationsRunner runner)
        {
            base.OnInit(runner);
            pointerDownAnimator?.Init(this, runner);
            pointerUpAnimator?.Init(this, runner);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;

            pointerUpAnimator?.Cancel();
            pointerUpAnimator?.Animate(this);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;

            pointerUpAnimator?.Cancel();
            pointerDownAnimator?.Cancel();
            pointerDownAnimator?.Animate(this);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            // Debug.Log($"Pointer Click");
            onClick?.Invoke();
        }
    }
}