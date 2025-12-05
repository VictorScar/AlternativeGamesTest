using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

public class ScaleUIAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private CanvasGroup cg;

    [SerializeField] private Vector3 targetScale = new Vector3(1, 2, 1);
    [SerializeField] private float animationSpeed = 2f;
    private Coroutine _animation;
    private TaskCompletionSource<bool> _tcs;
    public event Action onStart; 
    public event Action onEnd; 

    [Button("Animate")]
    public void Animate()
    {
        _tcs = new TaskCompletionSource<bool>();
        
        Debug.Log("Animate");
        if (_animation == null)
        {
            _animation = StartCoroutine(AnimateInternal());
        }
    }


    private IEnumerator AnimateInternal()
    {
        while ((rect.localScale - targetScale).sqrMagnitude > 0.0001f)
        {
            rect.transform.localScale = Vector3.MoveTowards( rect.transform.localScale, targetScale, animationSpeed * Time.deltaTime);
            yield return null;
        }

        _tcs.TrySetResult(true);
        _animation = null;
    }
}