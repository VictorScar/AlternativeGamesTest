using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] protected RectTransform rect;
    [SerializeField] protected CanvasGroup cg;

    [SerializeReference] protected UIAnimator showAnimator;
    [SerializeReference] protected UIAnimator hideAnimator;

    public RectTransform Rect => rect;
    public CanvasGroup CG => cg;

    public void Init()
    {
        showAnimator?.Init(this);
        hideAnimator?.Init(this);
        OnInit();
    }

    [Button("Show")]
    public void Show()
    {
        gameObject.SetActive(true);
        hideAnimator?.Cancel(this);
        showAnimator?.Animate(this, OnShow);
    }


    [Button("Hide")]
    public void Hide()
    {
        showAnimator?.Cancel(this);
        hideAnimator?.Cancel(this);
        hideAnimator?.Animate(this, OnHide);
    }

    protected virtual void OnInit()
    {
        
    }

    protected virtual void OnShow()
    {
    }

    protected virtual void OnHide()
    {
        gameObject.SetActive(false);
    }
}