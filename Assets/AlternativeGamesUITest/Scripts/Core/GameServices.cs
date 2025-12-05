namespace AlternativeGamesTest.Service
{
    public class GameServices
    {
        private readonly ConfigService _configService;
        private PlayerStatisticService _statisticService;
        public PlayerStatisticService StatisticService => _statisticService;
        public ConfigService ConfigService => _configService;
        
        public GameServices(ConfigService configService, PlayersStatisticDataConfig mockRatingDataConfig)
        {
            _configService = configService;
            _statisticService = new PlayerStatisticService(mockRatingDataConfig);
        }
    }
}