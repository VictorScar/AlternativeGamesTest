using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RatingRowView : MonoBehaviour
{
    [SerializeField] private RectTransform selectableFrame;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text score;

    public string IconID
    {
        set
        {
            
        }
    }
}
