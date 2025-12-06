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
        _gameServices = new GameServices(configService, mockRatingDataConfig);

        uiSystem.Init();
        scenarioContainer.Init(_gameServices, uiSystem);

        var ratingScenario = scenarioContainer.GetScenario<ShowRatingScenario>();
        ratingScenario.Run(_gameCancellationSource.Token);
    }
}