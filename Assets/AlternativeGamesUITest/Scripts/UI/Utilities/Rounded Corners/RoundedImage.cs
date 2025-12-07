using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

namespace AlternativeGamesTest.UI.Utils
{
    [AddComponentMenu("UI/Rounded Image")]
    [ExecuteAlways]
    public class RoundedImage : Image
    {
        [Header("Rounded Settings")]
        [Min(0f)] // Теперь это значение в ПИКСЕЛЯХ, а не от 0 до 1
        public float cornerRadius = 20f; 
        public float softness = 1f;

        private Material _materialInstance;

        // Кэш ID
        static readonly int PropsRectSize = Shader.PropertyToID("_RectSize");
        static readonly int PropsRadius = Shader.PropertyToID("_Radius");
        static readonly int PropsSoftness = Shader.PropertyToID("_Softness");
        static readonly int PropsPivot = Shader.PropertyToID("_Pivot");
        static readonly int PropsColorMask = Shader.PropertyToID("_ColorMask");

        public override Material GetModifiedMaterial(Material baseMaterial)
        {
            Material materialFromUGUI = base.GetModifiedMaterial(baseMaterial);

            if (!IsActive()) return materialFromUGUI;

            if (_materialInstance == null || _materialInstance.shader == null ||
                _materialInstance.shader.name != "UI/RoundedImage")
            {
                var shader = Shader.Find("UI/RoundedImage");
                if (shader == null) return materialFromUGUI;

                if (_materialInstance != null)
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying) DestroyImmediate(_materialInstance);
                    else Destroy(_materialInstance);
#else
                Destroy(_materialInstance);
#endif
                }
                _materialInstance = new Material(shader);
                _materialInstance.hideFlags = HideFlags.HideAndDontSave;
            }

            _materialInstance.CopyPropertiesFromMaterial(materialFromUGUI);

            var mask = GetComponent<Mask>();
            if (mask != null && mask.enabled)
            {
                _materialInstance.SetFloat(PropsColorMask, mask.showMaskGraphic ? 15f : 0f);
            }

            UpdateMyProperties(_materialInstance);

            return _materialInstance;
        }

        // Наш магический метод обновления через CanvasRenderer
        void Update()
        {
            if (canvasRenderer.materialCount > 0)
            {
                Material currentMat = canvasRenderer.GetMaterial();
                if (currentMat != null && currentMat.shader.name == "UI/RoundedImage")
                {
                    UpdateMyProperties(currentMat);
                }
            }
        }

        void UpdateMyProperties(Material mat)
        {
            if (rectTransform == null) return;
            Vector2 size = rectTransform.rect.size;
        
            // 1. Считаем физический предел радиуса (половина меньшей стороны)
            float maxPossibleRadius = Mathf.Min(size.x, size.y) / 2f;
        
            // 2. Берем ваш радиус в пикселях, но не даем ему превысить предел
            // Теперь при увеличении size, cornerRadius (20px) останется 20px, 
            // пока объект не станет слишком маленьким.
            float r = Mathf.Min(cornerRadius, maxPossibleRadius);

            mat.SetVector(PropsRectSize, size);
            mat.SetFloat(PropsRadius, r);
            mat.SetFloat(PropsSoftness, Mathf.Max(1f, softness));
            mat.SetVector(PropsPivot, rectTransform.pivot);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (_materialInstance != null)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) DestroyImmediate(_materialInstance);
                else Destroy(_materialInstance);
#else
            Destroy(_materialInstance);
#endif
                _materialInstance = null;
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            softness = Mathf.Max(0.01f, softness);
            cornerRadius = Mathf.Max(0f, cornerRadius); // Защита от отрицательных значений
        
            EditorApplication.delayCall += () =>
            {
                if (this) 
                {
                    Update(); 
                    UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                }
            };
        }
#endif
    }
}
#endif