using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

namespace AlternativeGamesTest.UI.Utils
{
    [AddComponentMenu("UI/Effects/Gradient")]
    [ExecuteAlways]
    [RequireComponent(typeof(Graphic))]
    public class Gradient : MonoBehaviour, IMaterialModifier
    {
        [Header("Gradient Settings")]
        [Range(0f, 360f)] public float angle = 0f;
    
        [Range(-1f, 1f)] 
        public float offset = 0f; 

        [Range(0.1f, 5f)] 
        [Tooltip("1 = Normal, >1 = Softer, <1 = Sharper")]
        public float scale = 1f;

        public Color colorTop = Color.white;
        public Color colorBottom = Color.black;

        private Graphic _graphic;
        private Material _materialInstance;

        // Кэш ID свойств
        private static readonly int PropsAngle = Shader.PropertyToID("_Angle");
        private static readonly int PropsOffset = Shader.PropertyToID("_Offset");
        private static readonly int PropsScale = Shader.PropertyToID("_Scale");
        private static readonly int PropsColorTop = Shader.PropertyToID("_ColorTop");
        private static readonly int PropsColorBottom = Shader.PropertyToID("_ColorBottom");
    
        // Кэш ID для маски (чтобы копировать настройки родителя)
        private static readonly int PropsStencil = Shader.PropertyToID("_Stencil");
        private static readonly int PropsStencilComp = Shader.PropertyToID("_StencilComp");
        private static readonly int PropsStencilOp = Shader.PropertyToID("_StencilOp");
        private static readonly int PropsStencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
        private static readonly int PropsStencilReadMask = Shader.PropertyToID("_StencilReadMask");
        private static readonly int PropsColorMask = Shader.PropertyToID("_ColorMask");

        private Graphic TargetGraphic => _graphic ? _graphic : (_graphic = GetComponent<Graphic>());

        // 1. Этот метод подменяет Дефолтный материал на Наш Градиентный
        public Material GetModifiedMaterial(Material baseMaterial)
        {
            if (!isActiveAndEnabled) return baseMaterial;

            // Создаем материал, если его нет или шейдер слетел
            if (_materialInstance == null || _materialInstance.shader.name != "UI/GradientShader")
            {
                var shader = Shader.Find("UI/GradientShader");
                if (shader == null)
                {
                    Debug.LogError("Shader 'UI/GradientShader' not found!");
                    return baseMaterial;
                }

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

            // ВАЖНО: Копируем настройки Stencil/ColorMask из материала, который пришел "сверху".
            // (Например, от RoundedImage или стандартной Mask).
            _materialInstance.CopyPropertiesFromMaterial(baseMaterial);

            // Применяем параметры градиента
            UpdateGradientProps(_materialInstance);

            return _materialInstance;
        }

        // 2. Этот метод обновляет параметры КАЖДЫЙ КАДР.
        // Это решает проблему "обновляется только при сохранении".
        void Update()
        {
            if (TargetGraphic.canvasRenderer.materialCount > 0)
            {
                // Берем материал, который ПРЯМО СЕЙЧАС висит на рендере
                Material currentMat = TargetGraphic.canvasRenderer.GetMaterial();
            
                // Если это наш шейдер - обновляем цифры напрямую
                if (currentMat != null && currentMat.shader.name == "UI/GradientMaskedElement")
                {
                    UpdateGradientProps(currentMat);
                }
            }
        }

        void UpdateGradientProps(Material mat)
        {
            mat.SetFloat(PropsAngle, angle);
            mat.SetFloat(PropsOffset, offset);
            mat.SetFloat(PropsScale, scale);
            mat.SetColor(PropsColorTop, colorTop);
            mat.SetColor(PropsColorBottom, colorBottom);
        }

        void OnEnable() => TargetGraphic.SetMaterialDirty();

        void OnDisable()
        {
            TargetGraphic.SetMaterialDirty();
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
        void OnValidate()
        {
            // Защита от некорректных значений
            scale = Mathf.Max(0.001f, scale);

            EditorApplication.delayCall += () =>
            {
                if (this == null) return;
            
                // 1. Принудительно обновляем через Update()
                Update();
            
                // 2. Сообщаем системе UI, что материал изменился (на случай если шейдер слетел)
                TargetGraphic.SetMaterialDirty();
            
                // 3. Перерисовываем окно редактора
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            };
        }
#endif
    }
}
#endif