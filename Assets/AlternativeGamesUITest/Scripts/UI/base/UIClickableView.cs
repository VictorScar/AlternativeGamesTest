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

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;

            pointerUpAnimator?.Cancel(this);
            pointerUpAnimator?.Animate(this);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;

            pointerUpAnimator?.Cancel(this);
            pointerDownAnimator?.Cancel(this);
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