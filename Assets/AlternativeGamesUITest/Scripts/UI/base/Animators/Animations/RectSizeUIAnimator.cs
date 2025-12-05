using System;
using System.Collections;
using AlternativeGamesTest.UI;
using UnityEngine;

[Serializable]
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
        Vector2 startSize = view.Rect.sizeDelta;
        Vector2 targetSize = new Vector2(
            _defaultSize.x * targetSizeMultiplier.x,
            _defaultSize.y * targetSizeMultiplier.y
        );

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;


            float t = Mathf.Clamp01(elapsed / duration);
            float easedT = GetEasedTime(t);
            view.Rect.sizeDelta = Vector2.LerpUnclamped(startSize, targetSize, easedT);

            yield return null;
        }
        
        view.Rect.sizeDelta = targetSize;
    }
}