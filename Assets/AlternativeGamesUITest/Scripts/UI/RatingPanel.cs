using System;
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

        /*private void UpdateScrollPosition(int selectedElementIndex)
        {
            var view = _views[selectedElementIndex];
            var viewport = scroll.viewport;
            var viewRelativePosition = viewport.InverseTransformPoint(view.Rect.position);

            if (viewRelativePosition.y > 0.5f * viewport.sizeDelta.y ||
                viewRelativePosition.y < 0.5f * viewport.sizeDelta.y)
            {
                var scrollPosition = 1f - ((float)selectedElementIndex / (float)Length);
                scroll.verticalNormalizedPosition = scrollPosition;
            }
        }*/

        private void Update()
        {
            Debug.Log(scroll.verticalNormalizedPosition);
        }

        private void UpdateScrollPosition(int selectedElementIndex)
        {
            if (scroll.content == null || scroll.viewport == null) return;

            LayoutRebuilder.ForceRebuildLayoutImmediate(scroll.content);
            Canvas.ForceUpdateCanvases();

            // 2. Получаем rect'ы
            RectTransform targetRect = _views[selectedElementIndex].Rect;
            RectTransform contentRect = scroll.content;
            RectTransform viewportRect = scroll.viewport;

            // 3. Переводим границы элемента в пространство Viewport
            // (Это ключевой момент: мы хотим знать, где элемент относительно "окна просмотра")

            Vector3 targetWorldTopLeft =
                new Vector3(targetRect.position.x, targetRect.TransformPoint(targetRect.rect.max).y, 0);
            // ^ Внимание: targetRect.rect.max.y - это верхняя граница в локальных координатах элемента (обычно height/2 или height)
            // Лучше взять WorldCorners, это надежнее:
            Vector3[] corners = new Vector3[4];
            targetRect.GetWorldCorners(corners);
            // [1] = TopLeft, [0] = BottomLeft (в мировых Y)

            // Переводим мировые Y в локальные Y Вьюпорта
            Vector2 targetTopLocal = viewportRect.InverseTransformPoint(corners[1]);
            Vector2 targetBottomLocal = viewportRect.InverseTransformPoint(corners[0]);

            // В локальных координатах UI (обычно):
            // Центр вьюпорта (0,0). Верх = +height/2. Низ = -height/2.
            // Нам нужно знать границы вьюпорта в ЕГО же локальных координатах:
            float viewTop = viewportRect.rect.yMax; // Например, +500
            float viewBottom = viewportRect.rect.yMin; // Например, -500

            // Паддинг
            float padding = 200f;

            // Текущая позиция скролла (0..1)
            float currentNormPos = scroll.verticalNormalizedPosition;

            // Считаем высоту контента за вычетом вьюпорта (сколько можно скроллить)
            float scrollableRange = contentRect.rect.height - viewportRect.rect.height;
            if (scrollableRange <= 0) return; // Не скроллим

            // Насколько нужно сдвинуть контент в ПИКСЕЛЯХ
            float shift = 0f;

            // Логика:
            // Если "верх" элемента выше "верха" вьюпорта (с учетом паддинга)
            // targetTopLocal.y > (viewTop - padding)
            if (targetTopLocal.y > (viewTop - padding))
            {
                // Нам нужно сдвинуть контент ВНИЗ (уменьшить offset), чтобы элемент опустился.
                // shift будет отрицательным
                shift = (viewTop - padding) - targetTopLocal.y;
            }
            // Если "низ" элемента ниже "низа" вьюпорта (с учетом паддинга)
            // targetBottomLocal.y < (viewBottom + padding)
            else if (targetBottomLocal.y < (viewBottom + padding))
            {
                // Нам нужно сдвинуть контент ВВЕРХ (увеличить offset), чтобы элемент поднялся.
                // shift будет положительным
                shift = (viewBottom + padding) - targetBottomLocal.y;
            }

            // Если shift != 0, применяем его
            if (Mathf.Abs(shift) > 0.1f)
            {
                // Текущий offset в пикселях (от верха)
                // normalizedPos = 1 -> offset = 0
                // normalizedPos = 0 -> offset = scrollableRange
                float currentScrollOffset = (1f - currentNormPos) * scrollableRange;

                // Применяем сдвиг. 
                // Если shift > 0 (надо поднять контент, показать низ), мы увеличиваем offset.
                // Если shift < 0 (надо опустить контент, показать верх), мы уменьшаем offset.
                float newOffset = currentScrollOffset + shift;

                // --- Магнит для низа ---
                // Если это последний элемент, всегда ставим в самый низ
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