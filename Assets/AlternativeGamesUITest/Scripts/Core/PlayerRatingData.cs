using System;

namespace AlternativeGamesTest.Service
{
    [Serializable]
    public struct PlayerRatingData
    {
        public string PlayerName;
        public string IconID;
        public int Score;
    }
}