Shader "Custom/SlimeShader"
{
    Properties
    {
        _Color("Color",Color) = (0,0,1,0.1)
        _MainTex ("texture", 2D) = "white" {}
    }
        SubShader
    {
Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent"}
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off Lighting Off

        LOD 100
        Pass
        {
           Stencil {
                Ref 0
                Comp Equal
                Pass IncrSat
                Fail IncrSat
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col;
                fixed4 tex = tex2D(_MainTex, i.uv);

                col.rgb = i.color.rgb * tex.rgb;
                col.a = i.color.a * tex.a;
                if (col.a == 0) {
                    discard;
                }
                return col;
            }
            ENDCG
        }
 
    }
}
