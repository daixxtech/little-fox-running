Shader "Custom/ScrollingBackground"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" { }
        _SecondTex ("Second Texture", 2D) = "white" { }
        _MainSpeed ("Main Speed", Range(0.1, 2)) = 1
        _SecondSpeed ("Second Speed", Range(0.1, 2)) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct a2v
            {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
            };

            struct v2f
            {
                float4 pos: SV_POSITION;
                float4 uv: TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecondTex;
            float4 _SecondTex_ST;
            float _MainSpeed;
            float _SecondSpeed;

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex) + frac(float2(_MainSpeed, 0) * _Time.y);
                o.uv.zw = TRANSFORM_TEX(v.uv, _SecondTex) + frac(float2(_SecondSpeed, 0) * _Time.y);
                return o;
            }

            fixed4 frag(v2f i): SV_Target
            {
                fixed4 mainCol = tex2D(_MainTex, i.uv.xy);
                fixed4 detailCol = tex2D(_SecondTex, i.uv.zw);
                fixed4 col = lerp(mainCol, detailCol, detailCol.a);
                return mainCol;
            }
            ENDCG

        }
    }
}
