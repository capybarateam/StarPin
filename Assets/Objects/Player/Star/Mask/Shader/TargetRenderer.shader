Shader "Custom/TargetRenderer" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_EmissionColor("Emission Color", Color) = (0,0,0)
		_EmissionMap ("Emission (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {

		Tags { "RenderType"="Transparent" "Queue"="Transparent+13" }

		LOD 200

		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _EmissionMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_EmissionMap;
		};

		half _Glossiness;
		half _Metallic;

		fixed4 _Color;
		fixed4 _EmissionColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 e = tex2D(_EmissionMap, IN.uv_EmissionMap) * _EmissionColor;
			o.Albedo = c.rgb;
			o.Emission = e.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
