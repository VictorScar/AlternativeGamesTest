using System.Collections;
using System.Collections.Generic;
using AlternativeGamesTest.Service;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/PlayersStatisticMock", fileName = "PlayersStatisticMockConfig")]
public class PlayersStatisticDataConfig : ScriptableObject
{
    [SerializeField] private PlayerRatingData[] data;

    public PlayerRatingData[] Data => data;
}
