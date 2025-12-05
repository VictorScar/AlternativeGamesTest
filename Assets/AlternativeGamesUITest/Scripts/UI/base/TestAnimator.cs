using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimator : UIView
{
   private void Start()
   {
       Init();
   }

   [Button("In")]
    public void Show()
    {
        showAnimator?.Cancel();
        hideAnimator?.Cancel();
        showAnimator.Animate(this, OnShow);
    }

    [Button("Out")]
    public void Hide()
    {
        showAnimator?.Cancel();
        hideAnimator?.Cancel();
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
