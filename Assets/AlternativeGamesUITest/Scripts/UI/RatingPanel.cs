using System.Collections.Generic;
using UnityEngine;

namespace AlternativeGamesTest.UI
{
    public class RatingPanel : UIView
    {
        [SerializeField] private RectTransform root;
        [SerializeField] private RatingRowView prefab;
        
        private List<RatingRowView> _views = new List<RatingRowView>();

        public int Length
        {
            get => _views.Count;
            set
            {
                if(value == Length) return;

                for (int i = 0; i < value; i++)
                {
                    RatingRowView view;
                    
                    if (i >= Length)
                    {
                        view = Instantiate(prefab, root);
                        view.Init();
                        _views.Add(view);
                    }
                    else
                    {
                        view = _views[i];
                    }
                    
                    view.Show(immediately:true);
                }

                for (int i = value; i < Length; i++)
                {
                    _views[i].Hide(immediately:true);
                }
                
            }
        }

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

        protected override void OnInit()
        {
            base.OnInit();

            var defaultViews = GetComponentsInChildren<RatingRowView>();

            if (defaultViews != null)
            {
                foreach (var view in defaultViews)
                {
                    view.Init();
                    _views.Add(view);
                }
            }
        }
    }
}