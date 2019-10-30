// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Jake/TransparentBlur" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_BlurAmount("Blur Speed", Float) = 100.0
	}

		SubShader{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass {
				CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma multi_compile_fog

					#include "UnityCG.cginc"

					struct appdata_t {
						float4 vertex : POSITION;
						float2 texcoord : TEXCOORD0;
					};

					struct v2f {
						float4 vertex : SV_POSITION;
						half2 texcoord : TEXCOORD0;
						UNITY_FOG_COORDS(1)
					};

					sampler2D _MainTex;
					float4 _MainTex_ST;
					float _BlurAmount;

					v2f vert(appdata_t v)
					{
						v2f o;
						o.vertex = UnityObjectToClipPos(v.vertex);
						o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
						UNITY_TRANSFER_FOG(o,o.vertex);
						return o;
					}

					const int quality = 1;

					fixed4 frag(v2f i) : SV_Target
					{
						fixed4 center = tex2D(_MainTex, i.texcoord);
						
						//fixed4 left = tex2D(_MainTex, i.texcoord + float3(-_BlurAmount, 0, 0));
						//fixed4 right = tex2D(_MainTex, i.texcoord + float3(_BlurAmount, 0, 0));
						half4 col = center;
						for (int inc = 0; inc <= quality; ++inc)
						{
							col += tex2D(_MainTex, i.texcoord + half2(_BlurAmount * inc, 0.0f));
						}

						//col /= quality;
						//UNITY_APPLY_FOG(i.fogCoord, col);
						return col;
					}
				ENDCG
			}
	}

}
