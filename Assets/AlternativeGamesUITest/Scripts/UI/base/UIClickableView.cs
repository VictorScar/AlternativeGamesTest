using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickableView : UIView, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler,
    IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private bool isInteractable = true;

    public event Action OnClick;

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"Pointer Up");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Pointer Down");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"On Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"On Pointer Exit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Pointer Click");
        OnClick?.Invoke();
    }
}