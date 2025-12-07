namespace AlternativeGamesTest.Service
{
    public class GameServices
    {
        private readonly ConfigService _configService;
        private readonly PlayerStatisticService _statisticService;
        private readonly InputService _inputService;
        public PlayerStatisticService StatisticService => _statisticService;
        public ConfigService ConfigService => _configService;
        public InputService InputService => _inputService;
        
        public GameServices(ConfigService configService, PlayersStatisticDataConfig mockRatingDataConfig, InputService inputService)
        {
            _configService = configService;
            _statisticService = new PlayerStatisticService(mockRatingDataConfig);
            _inputService = inputService;
        }
    }
}