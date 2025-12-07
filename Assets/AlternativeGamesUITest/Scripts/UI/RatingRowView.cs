using AlternativeGamesTest.UI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlternativeGamesTest.UI
{
    public class RatingRowView : UIClickableView
    {
        [SerializeField] private RectTransform selectableFrame;
        [SerializeField] private TMP_Text playerName;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text score;
        [SerializeReference] private UIAnimator selectedAnimation;
        [SerializeReference] private UIAnimator deselectedAnimation;

        public string PlayerName
        {
            set => playerName.text = value;
        }

        public Sprite IconID
        {
            set => icon.sprite = value;
        }

        public int Score
        {
            set => score.text = value.ToString();
        }

        public bool IsSelected
        {
            set
            {
                if (value)
                {
                    deselectedAnimation?.Cancel();
                    selectedAnimation?.Animate(this);
                }
                else
                {
                    selectedAnimation?.Cancel();
                    deselectedAnimation?.Animate(this);
                }

                selectableFrame.gameObject.SetActive(value);
            }
        }

        public PlayerRatingViewData Data
        {
            set
            {
                PlayerName = value.PlayerName;
                IconID = value.Icon;
                Score = value.Score;
            }
        }

        protected override void OnInit(UIAnimationsRunner runner)
        {
            base.OnInit(runner);
            selectedAnimation?.Init(this, runner);
            deselectedAnimation?.Init(this, runner);
        }
    }
}

namespace AlternativeGamesTest.UI.Base
{
    public struct PlayerRatingViewData
    {
        public string PlayerName;
        public Sprite Icon;
        public int Score;
    }
}