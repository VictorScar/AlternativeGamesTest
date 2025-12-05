Shader "UI/GradientMaskedElement"
{
    Properties
    {    
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _ColorTop ("Top Color", Color) = (1,1,1,1)
        _ColorBottom ("Bottom Color", Color) = (0,0,0,1)
        _Angle ("Angle", Float) = 0
        _Offset ("Offset", Float) = 0
        _Scale ("Scale", Float) = 1 // Новый параметр
        
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True" "PreviewType"="Plane"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 color : COLOR;
                float4 worldPosition : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ColorTop;
            float4 _ColorBottom;
            float _Angle;
            float _Offset;
            float _Scale; // Новый параметр
            float4 _ClipRect;

            v2f vert(appdata v)
            {
                v2f o;
                o.worldPosition = v.vertex;
                o.pos = UnityObjectToClipPos(o.worldPosition);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = o.uv * 2 - 1; 
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float rad = radians(_Angle);
                float2 dir = float2(cos(rad), sin(rad));

                // Проекция на ось градиента (-1..1)
                float t_raw = dot(i.uv2, dir);

                // Применяем масштаб (плавность) относительно центра
                // Если Scale > 1, t_raw делится на большое число -> диапазон значений уменьшается -> градиент растягивается
                t_raw /= max(0.001, _Scale);

                // Приводим к 0..1
                float t = t_raw * 0.5 + 0.5;
                
                // Смещение
                t -= _Offset;

                // Обрезаем
                t = saturate(t);
                
                // Дополнительно сглаживаем для красоты (Smoothstep)
                // Это делает переход "S-образным", убирая жесткие линии в середине
                t = smoothstep(0, 1, t);

                float4 grad = lerp(_ColorBottom, _ColorTop, t);
                float4 tex = tex2D(_MainTex, i.uv);
                float4 finalColor = tex * grad * i.color;

                #ifdef UNITY_UI_CLIP_RECT
                finalColor.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                #endif
                
                return finalColor;
            }
            ENDCG
        }
    }
}
