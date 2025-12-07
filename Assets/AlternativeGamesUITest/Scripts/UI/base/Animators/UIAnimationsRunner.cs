using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    public class UIAnimationsRunner : MonoBehaviour
    {
        private List<Coroutine> _executeAnimations = new List<Coroutine>();

        private void OnDestroy()
        {
            StopAll();
        }

        public Coroutine StartAnimation(IEnumerator animationIEnumerator)
        {
            var animation = StartCoroutine(animationIEnumerator);
            _executeAnimations.Add(animation);
            return animation;
        }

        public void StopAnimation(Coroutine animation)
        {
            if (animation != null)
            {
                _executeAnimations.Remove(animation);
                StopCoroutine(animation);
            }
        }

        public void StopAll()
        {
            for (var i = _executeAnimations.Count - 1; i >= 0; i--)
            {
                var animation = _executeAnimations[i];
                StopAnimation(animation);
            }
        }
    }
}