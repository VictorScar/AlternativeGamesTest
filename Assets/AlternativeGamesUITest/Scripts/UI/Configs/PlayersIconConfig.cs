using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Configs/PlayerIcons", fileName = "PlayerIconsConfig")]
public class PlayersIconConfig : ScriptableObject
{
    [SerializeField] private PlayerIconData[] datas;
    [SerializeField] private Sprite defaultIcon;
    
    public Sprite GetIconByID(string id)
    {
        if (datas != null)
        {
            foreach (var data in datas)
            {
                if (data.ID == id)
                {
                    return data.Icon;
                }
            }
        }

        return defaultIcon;
    }
}
