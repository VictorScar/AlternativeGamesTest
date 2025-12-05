using System.Collections;
using UnityEngine;

public class RectSizeUIAnimator : UIAnimator
{
    [SerializeField] private Vector2 targetSizeMultiplier = Vector2.one;
    private Vector2 _defaultSize;
    protected override void OnInit(UIView view)
    {
        base.OnInit(view);
        _defaultSize = view.Rect.sizeDelta;
    }

    protected override IEnumerator OnAnimation(UIView view)
    {
        // 1. Запоминаем начальные данные
        Vector2 startSize = view.Rect.sizeDelta;

        // 2. Считаем конечную цель (ваша логика)
        Vector2 targetSize = new Vector2(
            _defaultSize.x * targetSizeMultiplier.x,
            _defaultSize.y * targetSizeMultiplier.y
        );

        float elapsed = 0f;

        // 3. Цикл работает пока не пройдет нужное время
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Нормализованное время от 0 до 1
            float t = elapsed / duration;

            // ДОБАВЛЯЕМ ПЛАВНОСТЬ (Easing)
            // Если хотите линейно - уберите эту строку.
            // SmoothStep делает "медленный старт -> быстро -> медленный стоп"
            t = Mathf.SmoothStep(0f, 1f, t);

            // Используем Lerp между СТАРТОМ и ФИНИШЕМ
            view.Rect.sizeDelta = Vector2.LerpUnclamped(startSize, targetSize, t);

            yield return null;
        }
    }
}