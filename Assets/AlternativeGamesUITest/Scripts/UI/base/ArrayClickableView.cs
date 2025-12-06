using System;
using System.Collections.Generic;

namespace AlternativeGamesTest.UI.Base
{
    public class ArrayClickableView<TView> : ArrayView<TView> where TView:UIClickableView
    {
        private List<ClickableViewHandler<TView>> _handlers = new List<ClickableViewHandler<TView>>();
        
        public event Action<int> onElementClicked;
        
        private void OnDestroy()
        {
            foreach (var handler in _handlers)
            {
                handler.onElementClicked -= OnElementClicked;
                handler.Dispose();
            }
        }
        
        protected override void OnInit()
        {
            base.OnInit();
            _handlers.Clear();
        }

        protected override void OnViewCreated(TView view)
        {
            base.OnViewCreated(view);
            var handler = new ClickableViewHandler<TView>(view);
            handler.onElementClicked += OnElementClicked;
            _handlers.Add(handler);
        }
        
        private void OnElementClicked(TView clickedView)
        {
            onElementClicked?.Invoke(_views.IndexOf(clickedView));
        }

        private class ClickableViewHandler<TView>: IDisposable where TView:UIClickableView
        {
            private readonly TView _view;
            public event Action <TView> onElementClicked;
            
            public ClickableViewHandler(TView view)
            {
                _view = view;
                _view.onClick += OnClick;
            }
            
            public void Dispose()
            {
                _view.onClick -= OnClick;
            }
            
            private void OnClick()
            {
                onElementClicked?.Invoke(_view);
            }
        }
    }
}