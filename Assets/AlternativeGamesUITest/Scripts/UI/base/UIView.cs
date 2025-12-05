using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] protected RectTransform rect;
    [SerializeField] protected CanvasGroup cg;

    [SerializeField] protected UIAnimator showAnimator;
    [SerializeField] protected UIAnimator hideAnimator;
    
    public RectTransform Rect => rect;

    public void Init()
    {
        showAnimator?.Init(this);
        hideAnimator?.Init(this);
    }
    
    [Button("Show")]
    public void Show()
    {
        gameObject.SetActive(true);

        if (hideAnimator) hideAnimator.Cancel();
        
        if (showAnimator)
        {
            showAnimator.Animate(this, OnShow);
        }
    }


    [Button("Hide")]
    public void Hide()
    {
        if (showAnimator) showAnimator.Cancel();
        
        if (hideAnimator)
        {
            hideAnimator.Animate(this, OnHide);
        }
    }
    
    protected virtual void OnShow()
    {
        
    }
    
    protected virtual void OnHide()
    {
       gameObject.SetActive(false);
    }
}
