Shader "UI/RoundedImageV2"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        _Radius ("Radius", Float) = 10
        _Softness ("Softness", Float) = 1
        _RectSize ("Rect Size", Vector) = (100,100,0,0)
        _Pivot ("Pivot", Vector) = (0.5, 0.5, 0, 0)

        // Свойства для Mask (ОБЯЗАТЕЛЬНЫ)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
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
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT

            struct appdata
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };

            fixed4 _Color;
            float4 _RectSize;
            float4 _Pivot;
            float _Radius;
            float _Softness;
            sampler2D _MainTex;
            float4 _ClipRect;

            float sdRoundedRect(float2 p, float2 halfSize, float r)
            {
                float2 d = abs(p) - (halfSize - r);
                return min(max(d.x, d.y), 0.0) + length(max(d, 0.0)) - r;
            }

            v2f vert(appdata v)
            {
                v2f OUT;
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                OUT.texcoord = v.texcoord;
                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 localPos = (IN.texcoord - _Pivot.xy) * _RectSize.xy;
                float2 halfSize = _RectSize.xy * 0.5;
                
                float r = max(0, min(_Radius, min(halfSize.x, halfSize.y)));
                float dist = sdRoundedRect(localPos, halfSize, r);
                float alpha = 1.0 - smoothstep(-0.5, max(0.01, _Softness) - 0.5, dist);
                
                fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
                color.a *= alpha;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
                
                // Вырезаем пиксели для корректной работы Mask (Stencil)
                clip(color.a - 0.01);
                
                return color;
            }
            ENDCG
        }
    }
}
