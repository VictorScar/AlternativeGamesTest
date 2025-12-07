using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlternativeGamesTest.Service;
using AlternativeGamesTest.UI;
using AlternativeGamesTest.UI.Base;
using UnityEngine;

namespace AlternativeGamesTest.Scenario
{
    public class ShowRatingScenario : ScenarioBase
    {
        private RatingScreen _screen;
        private RatingPanel _panel;
        private PlayerStatisticService _statisticService;
        private UISelectionInputController _selectionInput;
        
        private int _selectedElement;
        
        protected override void OnInit()
        {
            base.OnInit();
            _screen = Data.UISystem.GetScreen<RatingScreen>();
            _panel = _screen.RatingPanel;
            _statisticService = Data.Services.StatisticService;
            _selectionInput = Data.Services.InputService.UISelectionController;
        }

        protected override async Task RunInternal(CancellationToken token)
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
            _selectionInput.onInputPerfomed += ChangeSelectedElement;
            _screen.Show();
            _panel.SelectView(_selectedElement);

            await _screen.CloseBtn.WaitForClickAsync(Token);
        }

        protected override void OnStop()
        {
            if(_panel) _panel.onElementClicked -= OnElementClicked;
            _selectionInput.onInputPerfomed -= ChangeSelectedElement;
            _screen?.Hide();
        }

        private void OnElementClicked(int viewIndex)
        {
            _selectedElement = viewIndex;
            _panel.SelectView(_selectedElement);
        }
        
        private void ChangeSelectedElement(int inputDirection)
        {
            _selectedElement += inputDirection;
            _selectedElement = Mathf.Clamp(_selectedElement,0, _panel.Length);
            
            _panel.SelectView(_selectedElement);
        }
    }
}