Shader "Custom/TargetRenderer" {
	Properties {
		_BaseColor ("Color", Color) = (1,1,1,1)
		_BaseColorMap ("Albedo (RGB)", 2D) = "white" {}
		_EmissiveColor("Emission Color", Color) = (0,0,0)
		_EmissiveColorMap ("Emission (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {

		Tags { "RenderType"="Transparent" "Queue"="Transparent+13" "RenderPipeline"="HDRenderPipeline" }

		LOD 200

		CGPROGRAM

		#pragma surface surf HDRP/Lit fullforwardshadows
		#pragma target 3.0

		sampler2D _BaseColorMap;
		sampler2D _EmissiveColorMap;

		struct Input {
			float2 uv_BaseColorMap;
			float2 uv_EmissiveColorMap;
		};

		half _Glossiness;
		half _Metallic;

		fixed4 _BaseColor;
		fixed4 _EmissiveColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_BaseColorMap, IN.uv_BaseColorMap) * _BaseColor;
			fixed4 e = tex2D(_EmissiveColorMap, IN.uv_EmissiveColorMap) * _EmissiveColor;
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
