using System.Collections.Generic;
using AlternativeGamesTest.UI.Base;

namespace AlternativeGamesTest.UI
{
    public class RatingPanel : ArrayClickableView<RatingRowView>
    {
        public List<PlayerRatingViewData> Data
        {
            set
            {
                if (value != null)
                {
                    Length = value.Count;

                    for (var i = 0; i < value.Count; i++)
                    {
                        var data = value[i];
                        _views[i].Data = data;
                    }
                }

                Length = 0;
            }
        }

        public void SelectView(int viewIndex)
        {
            if (viewIndex >= 0 && viewIndex < Length)
            {
                for (int i = 0; i < Length; i++)
                {
                    _views[i].IsSelected = i == viewIndex;
                }
            }
        }
    }
}