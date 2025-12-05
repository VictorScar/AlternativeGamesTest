using System;
using System.Collections;
using System.Collections.Generic;
using AlternativeGamesTest.UI;
using UnityEngine;

public class TestAnimator : UIView
{
    [SerializeReference] private FadeUIAnimator fgv;
   private void Start()
   {
       Init();
   }

  // [Button("In")]
    public void Show()
    {
        showAnimator?.Cancel(this);
        hideAnimator?.Cancel(this);
        showAnimator.Animate(this, OnShow);
    }

   // [Button("Out")]
    public void Hide()
    {
        showAnimator?.Cancel(this);
        hideAnimator?.Cancel(this);
        hideAnimator.Animate(this, OnHide);
    }
    
    private void OnShow()
    {
        Debug.Log("On Show");
    }
    
    private void OnHide()
    {
        Debug.Log("On Hide");
    }
}
