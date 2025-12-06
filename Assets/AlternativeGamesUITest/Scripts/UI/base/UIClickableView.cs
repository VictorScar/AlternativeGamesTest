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

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;
            //Debug.Log($"Pointer Up");
            pointerDownAnimator?.Cancel(this);
            pointerUpAnimator?.Cancel(this);
            pointerUpAnimator?.Animate(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;
           // Debug.Log($"Pointer Down");
            pointerUpAnimator?.Cancel(this);
            pointerDownAnimator?.Cancel(this);
            pointerDownAnimator?.Animate(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           // Debug.Log($"On Pointer Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           // Debug.Log($"On Pointer Exit");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            Debug.Log($"Pointer Click");
            onClick?.Invoke();
        }
    }
}