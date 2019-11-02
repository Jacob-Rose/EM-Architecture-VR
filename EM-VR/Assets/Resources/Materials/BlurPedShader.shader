// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Jake/TransparentBlur" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_MixTex("Mix (RGBA)", 2D) = "white" {}
		_BlurAmount("Mix Amount", Range(0,1)) = 1.0
		_Transparency("Transparency", Range(0,1)) = 1.0
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
					sampler2D _MixTex;
					float _BlurAmount;
					float _Transparency;

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
						fixed4 mainCol = tex2D(_MainTex, i.texcoord);
						fixed4 otherCol = tex2D(_MixTex, i.texcoord);
						fixed4 col = lerp(mainCol, otherCol, _BlurAmount);

						return fixed4(col.rgb, col.a * _Transparency);
					}
				ENDCG
			}
	}

			SubShader
					{
							Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "Geometry"}
							blend SrcAlpha OneMinusSrcAlpha

							LOD 200
							CGPROGRAM
							#pragma surface surf Lambert exclude_path:prepass

							sampler2D _MainTex;
							fixed4 _Color;

							struct Input
							{
									float2 uv_MainTex;
							};

							void surf(Input IN, inout SurfaceOutput o)
							{
									fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
									o.Albedo = c.rgb;
									o.Alpha = c.a;
							}
							ENDCG
					}

}
