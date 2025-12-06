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

        protected override void OnInit()
        {
            base.OnInit();
            ratingPanel.Init();
            closeBtn.Init();
        }
    }
}