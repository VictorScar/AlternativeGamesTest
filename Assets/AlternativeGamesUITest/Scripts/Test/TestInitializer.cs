using System.Threading;
using AlternativeGamesTest.Scenario;
using AlternativeGamesTest.Service;
using AlternativeGamesTest.UI.Base;
using UnityEngine;

public class TestInitializer : MonoBehaviour
{
    [SerializeField] private PlayersIconConfig iconsConfig;
    [SerializeField] private UISystem uiSystem;
    [SerializeField] private ScenarioContainer scenarioContainer;
    [SerializeField] private PlayersStatisticDataConfig mockRatingDataConfig;
    [SerializeField] private InputService inputService;
    private GameServices _gameServices;
    private CancellationTokenSource _gameCancellationSource;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        _gameCancellationSource = new CancellationTokenSource();
        var configService = new ConfigService(iconsConfig);
        inputService.Init();
        _gameServices = new GameServices(configService, mockRatingDataConfig, inputService);

        uiSystem.Init();
        scenarioContainer.Init(_gameServices, uiSystem);

        var ratingScenario = scenarioContainer.GetScenario<ShowRatingScenario>();
        ratingScenario.Run(_gameCancellationSource.Token);
    }
}