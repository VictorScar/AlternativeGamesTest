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
        [SerializeField] private UIAnimator selectedAnimation;
        [SerializeField] private UIAnimator deselectedAnimation;

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
                    deselectedAnimation?.Cancel(this);
                    selectedAnimation?.Animate(this);
                }
                else
                {
                    selectedAnimation?.Cancel(this);
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
    }

    public struct PlayerRatingViewData
    {
        public string PlayerName;
        public Sprite Icon;
        public int Score;
    }
}