using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    
    public string IconID
    {
        set
        {
            
        }
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

    public PlayerRatingData Data
    {
        set
        {
            PlayerName = value.PlayerName;
            IconID = value.IconID;
            Score = value.Score;
        }
    }
}
