Shader "UI/Gradient"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color1 ("Gradient Color 1", Color) = (1,1,1,1)
        _Color2 ("Gradient Color 2", Color) = (0,0,0,1)
        _GradientDirection ("Gradient Direction (0=Horizontal, 1=Vertical)", Range(0,1)) = 0
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
        
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };
            
            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };
            
            sampler2D _MainTex;
            fixed4 _Color1;
            fixed4 _Color2;
            float _GradientDirection;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            
            v2f vert(appdata_t v)
            {
                v2f OUT;
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                
                OUT.texcoord = v.texcoord;
                OUT.color = v.color;
                
                return OUT;
            }
            
            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                
                float t = _GradientDirection > 0.5 ? IN.texcoord.y : IN.texcoord.x;
                fixed4 gradient = lerp(_Color1, _Color2, t);
                
                color.rgb *= gradient.rgb;
                color.a *= gradient.a;
                
                return color;
            }
            ENDCG
        }
    }
}
