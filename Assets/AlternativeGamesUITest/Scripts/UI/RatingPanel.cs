using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingPanel : UIView
{
    [SerializeField] private UIClickableView[] views;

    protected override void OnInit()
    {
        base.OnInit();

        if (views != null)
        {
            foreach (var view in views)
            {
                view.Init();
            }
        }
    }

    void Start()
    {
        Init();
    }
}