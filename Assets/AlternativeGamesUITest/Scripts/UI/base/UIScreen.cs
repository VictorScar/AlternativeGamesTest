using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : UIView
{
    [SerializeField] private bool showInInit = false;

    protected override void OnInit()
    {
        base.OnInit();

        if (showInInit)
        {
            Show(immediately:true);
        }
        else
        {
            Hide(immediately:true);
        }
    }
}
