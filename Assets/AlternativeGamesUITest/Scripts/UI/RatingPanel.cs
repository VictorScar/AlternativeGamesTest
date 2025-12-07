using System.Collections.Generic;
using AlternativeGamesTest.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace AlternativeGamesTest.UI
{
    public class RatingPanel : ArrayClickableView<RatingRowView>
    {
        [SerializeField] private ScrollRect scroll;
        [SerializeField] private float scrollPadding = 200f;

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
                else
                {
                    Length = 0;
                }
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

                UpdateScrollPosition(viewIndex);
            }
        }

        private void UpdateScrollPosition(int selectedElementIndex)
        {
            if (scroll.content == null || scroll.viewport == null) return;

            LayoutRebuilder.ForceRebuildLayoutImmediate(scroll.content);
            Canvas.ForceUpdateCanvases();

            RectTransform targetRect = _views[selectedElementIndex].Rect;
            RectTransform contentRect = scroll.content;
            RectTransform viewportRect = scroll.viewport;

            Vector3 targetWorldTopLeft =
                new Vector3(targetRect.position.x, targetRect.TransformPoint(targetRect.rect.max).y, 0);

            Vector3[] corners = new Vector3[4];
            targetRect.GetWorldCorners(corners);

            Vector2 targetTopLocal = viewportRect.InverseTransformPoint(corners[1]);
            Vector2 targetBottomLocal = viewportRect.InverseTransformPoint(corners[0]);

            float viewTop = viewportRect.rect.yMax;
            float viewBottom = viewportRect.rect.yMin;
            float padding = 200f;
            float currentNormPos = scroll.verticalNormalizedPosition;
            float scrollableRange = contentRect.rect.height - viewportRect.rect.height;

            if (scrollableRange <= 0) return;

            float shift = 0f;

            if (targetTopLocal.y > (viewTop - padding))
            {
                shift = (viewTop - padding) - targetTopLocal.y;
            }
            else if (targetBottomLocal.y < (viewBottom + padding))
            {
                shift = (viewBottom + padding) - targetBottomLocal.y;
            }
          
            if (Mathf.Abs(shift) > 0.1f)
            {
                float currentScrollOffset = (1f - currentNormPos) * scrollableRange;
                float newOffset = currentScrollOffset + shift;
               
                if (selectedElementIndex == Length - 1)
                {
                    newOffset = scrollableRange; // Max offset
                }
                else
                {
                    newOffset = Mathf.Clamp(newOffset, 0, scrollableRange);
                }

                scroll.verticalNormalizedPosition = 1f - (newOffset / scrollableRange);
            }
        }
    }
}