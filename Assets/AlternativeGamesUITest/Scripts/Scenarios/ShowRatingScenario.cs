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


        protected override void OnInit()
        {
            base.OnInit();
            _screen = Data.UISystem.GetScreen<RatingScreen>();
            _panel = _screen.RatingPanel;
            _statisticService = Data.Services.StatisticService;
        }

        protected override Task RunInternal(CancellationToken token)
        {
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
            _screen.Show();

            return Task.CompletedTask;
        }
    }
}