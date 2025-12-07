using AlternativeGamesTest.UI.Base;
using UnityEngine;

namespace AlternativeGamesTest.UI
{
    public class RatingScreen : UIScreen
    {
        [SerializeField] private RatingPanel ratingPanel;
        [SerializeField] private UIClickableView closeBtn;
    
        public RatingPanel RatingPanel => ratingPanel;
        public UIClickableView CloseBtn => closeBtn;

        protected override void OnInit(UIAnimationsRunner runner)
        {
            base.OnInit(runner);
            ratingPanel.Init(runner);
            closeBtn.Init(runner);
        }
    }
}