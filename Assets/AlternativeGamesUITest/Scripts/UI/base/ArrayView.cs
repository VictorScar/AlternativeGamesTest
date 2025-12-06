using System.Collections.Generic;
using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    public class ArrayView<TView> : UIView where TView : UIView
    {
        [SerializeField] private RectTransform root;
        [SerializeField] private TView prefab;

        protected List<TView> _views = new List<TView>();

        public int Length
        {
            get => _views.Count;
            set
            {
                if (value == Length) return;

                for (int i = 0; i < value; i++)
                {
                    TView view;

                    if (i >= Length)
                    {
                        view = Instantiate(prefab, root);
                        AddView((view));
                    }
                    else
                    {
                        view = _views[i];
                    }

                    view.Show(immediately: true);
                }

                for (int i = value; i < Length; i++)
                {
                    _views[i].Hide(immediately: true);
                }
            }
        }

        protected override void OnInit()
        {
            base.OnInit();

            var defaultViews = GetComponentsInChildren<TView>();

            if (defaultViews != null)
            {
                foreach (var view in defaultViews)
                {
                    AddView(view);
                }
            }
        }

        private void AddView(TView view)
        {
            view.Init();
            _views.Add(view);
            OnViewCreated(view);
        }

        protected virtual void OnViewCreated(TView view)
        {
        }
    }
}