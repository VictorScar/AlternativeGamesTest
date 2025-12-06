using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    public static class EaseFunctions
    {
        public static float Evaluate(EaseType type, float t)
        {
            switch (type)
            {
                case EaseType.Linear: return t;
                case EaseType.InSine:    return 1f - Mathf.Cos((t * Mathf.PI) / 2f);
                case EaseType.OutSine:   return Mathf.Sin((t * Mathf.PI) / 2f);
                case EaseType.InOutSine: return -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;
            
                // Quad (Более выраженное ускорение)
                case EaseType.InQuad:    return t * t;
                case EaseType.OutQuad:   return 1f - (1f - t) * (1f - t);
                case EaseType.InOutQuad: return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;

                // Cubic (Резкое ускорение)
                case EaseType.InCubic:   return t * t * t;
                case EaseType.OutCubic:  return 1f - Mathf.Pow(1f - t, 3f);
                case EaseType.InOutCubic: return t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
           
                //Пружина
                case EaseType.OutBack:
                    const float c1 = 1.70158f;
                    const float c3 = c1 + 1f;
                    return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);

                default: return t;
            }
        }
    }
}