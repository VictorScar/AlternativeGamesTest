using UnityEngine;

public class RatingScreen : UIScreen
{
    [SerializeField] private RatingPanel ratingPanel;

    public RatingPanel RatingPanel => ratingPanel;
    
    protected override void OnInit()
    {
        base.OnInit();
        ratingPanel.Init();
    }
}
