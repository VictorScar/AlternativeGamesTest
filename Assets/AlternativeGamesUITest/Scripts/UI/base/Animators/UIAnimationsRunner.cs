using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    public class UIAnimationsRunner : MonoBehaviour
    {
        private List<Coroutine> _executeAnimations = new List<Coroutine>();

        public Coroutine PlayAnimation(IEnumerator animationIEnumerator)
        {
            var animation = StartCoroutine(animationIEnumerator);
            _executeAnimations.Add(animation);
            return animation;
        }

        public void StopAll()
        {
            
        }
    }
}