using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlternativeGamesTest.Service;
using AlternativeGamesTest.UI;
using AlternativeGamesTest.UI.Base;

namespace AlternativeGamesTest.Scenario
{
    public class ShowRatingScenario : ScenarioBase
    {
        private RatingScreen _screen;
        private RatingPanel _panel;
        private PlayerStatisticService _statisticService;
        private int _selectedElement;

        protected override void OnInit()
        {
            base.OnInit();
            _screen = Data.UISystem.GetScreen<RatingScreen>();
            _panel = _screen.RatingPanel;
            _statisticService = Data.Services.StatisticService;
        }

        protected override Task RunInternal(CancellationToken token)
        {
            _selectedElement = 0;
            var ratingData = _statisticService.GetRatingData();
            var ratingViewData = new List<PlayerRatingViewData>();

            if (ratingData != null)
            {
                foreach (var data in ratingData)
                {
                    var icon = Data.Services.ConfigService.PlayersIconConfig.GetIconByID(data.IconID);

                    ratingViewData.Add(new PlayerRatingViewData
                    {
                        Icon = icon,
                        PlayerName = data.PlayerName,
                        Score = data.Score
                    });
                }
            }

            _panel.Data = ratingViewData;
            _panel.onElementClicked += OnElementClicked;
            _panel.SelectView(_selectedElement);
            _screen.Show();

            return Task.CompletedTask;
        }

        protected override void OnStop()
        {
            if(_panel) _panel.onElementClicked -= OnElementClicked;
        }

        private void OnElementClicked(int viewIndex)
        {
            _selectedElement = viewIndex;
            _panel.SelectView(_selectedElement);
        }
    }
}