using AlternativeGamesTest.Service;
using AlternativeGamesTest.UI;
using UnityEngine;

namespace AlternativeGamesTest.Scenario
{
    public class ScenarioContainer : MonoBehaviour
    {
        private ScenarioData _scenarioData;
        private ScenarioBase[] _scenarios;

        public void Init(GameServices gameServices, UISystem uiSystem)
        {
            _scenarioData = new ScenarioData
            {
                Services = gameServices,
                UISystem = uiSystem
            };

            _scenarios = GetComponentsInChildren<ScenarioBase>();

            if (_scenarios != null)
            {
                foreach (var scenario in _scenarios)
                {
                    scenario.Init(_scenarioData);
                }
            }
        }

        public T GetScenario<T>() where T : ScenarioBase
        {
            if (_scenarios != null)
            {
                foreach (var scenario in _scenarios)
                {
                    if (scenario is T typedScenario)
                    {
                        return typedScenario;
                    }
                }
            }

            return null;
        }
    }
}