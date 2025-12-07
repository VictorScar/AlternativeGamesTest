using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    public class UIScreen : UIView
    {
        [SerializeField] private bool showInInit = false;

        protected override void OnInit(UIAnimationsRunner runner)
        {
            base.OnInit(runner);

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
}