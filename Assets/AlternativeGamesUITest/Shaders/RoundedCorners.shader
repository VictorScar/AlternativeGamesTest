Shader "UI/RoundedRectSprite"
{
    Properties
    {
        [MainTexture] _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _CornerRadius ("Corner Radius (px)", Float) = 16
        _Size ("Rect Size (px)", Vector) = (100,100,0,0)
        
        // Градиент
        [Toggle] _UseGradient ("Use Gradient", Float) = 0
        _GradientTop ("Gradient Top", Color) = (1,1,1,1)
        _GradientBottom ("Gradient Bottom", Color) = (1,1,1,1)
        _GradientAngle ("Gradient Angle (deg)", Range(0, 360)) = 90
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float2 uv       : TEXCOORD0;
                float4 color    : COLOR;
                float2 localPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float  _CornerRadius;
            float4 _Size;
            
            // Градиент
            float _UseGradient;
            fixed4 _GradientTop;
            fixed4 _GradientBottom;
            float _GradientAngle;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv     = TRANSFORM_TEX(v.uv, _MainTex);
                o.color  = v.color * _Color;

                float2 size = _Size.xy;
                o.localPos = (v.uv - 0.5) * size;

                return o;
            }

            float sdRoundRect(float2 p, float2 size, float radius)
            {
                float2 d = abs(p) - (size - radius);
                return length(max(d, 0.0)) - radius + min(max(d.x, d.y), 0.0);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 sizePx = _Size.xy;
                float  r      = _CornerRadius;

                float maxR = 0.5 * min(sizePx.x, sizePx.y);
                r = clamp(r, 0.0, maxR);

                float2 hs = sizePx * 0.5;
                float dist = sdRoundRect(i.localPos, hs, r);
                float aa   = fwidth(dist);
                float mask = saturate(0.5 - dist / aa);

                // ✅ ГРАДИЕНТ С ПОВОТОМ
                float2 normalizedPos = (i.localPos / sizePx) + 0.5; // 0..1
                
                // Направление градиента (радианы)
                float angleRad = _GradientAngle * UNITY_PI / 180.0;
                float2 gradientDir = float2(cos(angleRad), sin(angleRad));
                float2 gradientStart = 0.5 - 0.5 * gradientDir;
                
                // Прогресс градиента 0..1
                float t = saturate(dot(normalizedPos - gradientStart, gradientDir));
                fixed4 gradientColor = lerp(_GradientBottom, _GradientTop, t);
                
                // Выбор: градиент или обычный цвет
                fixed4 baseColor = lerp(_Color, gradientColor, _UseGradient);

                fixed4 texCol = tex2D(_MainTex, i.uv) * i.color * baseColor;
                texCol.a *= mask;

                return texCol;
            }
            ENDCG
        }
    }
}
