using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAnimator : MonoBehaviour
{
    [SerializeField] protected float duration = 0.3f;
    [Header("Easing Settings")]
    [SerializeField] protected EaseType easeType = EaseType.OutQuad;
    
    
    protected Coroutine _animation;

    public void Init(UIView view)
    {
        OnInit(view);
    }

    protected virtual void OnInit(UIView view)
    {
       
    }

    public Coroutine Animate(UIView view, Action action = null)
    {
        Cancel();
        _animation = StartCoroutine(AnimateInternal(view, action));
        return _animation;
    }

    public void Cancel()
    {
        if (_animation != null)
        {
            StopCoroutine(_animation);
            _animation = null;
        }
    }
    
    protected abstract IEnumerator OnAnimation(UIView view);

    protected virtual void OnAnimationEnded()
    {
        _animation = null;
    }
    
    protected float GetEasedTime(float t)
    {
        return EaseFunctions.Evaluate(easeType, t);
    }
    
    private IEnumerator AnimateInternal(UIView view, Action onComplete)
    {
        yield return OnAnimation(view);
        OnAnimationEnded();
        onComplete?.Invoke();
    }


   
}