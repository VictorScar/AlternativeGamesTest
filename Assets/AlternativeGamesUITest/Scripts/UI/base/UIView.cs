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

 
    public void Show(bool immediately = false)
    {
        gameObject.SetActive(true);
        hideAnimator?.Cancel(this);

        if (!immediately)
        {
            showAnimator?.Animate(this, OnShow);
        }
        else
        {
            showAnimator?.AnimateImmediately(OnShow);
        }
    }

  
    public void Hide(bool immediately = false)
    {
        showAnimator?.Cancel(this);

        if (!immediately)
        {
            hideAnimator?.Animate(this, OnHide);
        }
        else
        {
            hideAnimator?.AnimateImmediately(OnHide);
        }
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