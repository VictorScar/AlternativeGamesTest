using System;
using UnityEngine;

namespace AlternativeGamesTest.Service
{
    [Serializable]
    public class PlayerStatisticService
    {
       private PlayersStatisticDataConfig _mockRatingDataConfig;

        public PlayerStatisticService(PlayersStatisticDataConfig mockRatingDataConfig)
        {
            _mockRatingDataConfig = mockRatingDataConfig;
        }


        public PlayerRatingData[] GetRatingData()
        {
            return _mockRatingDataConfig.Data;
        }
    }
}