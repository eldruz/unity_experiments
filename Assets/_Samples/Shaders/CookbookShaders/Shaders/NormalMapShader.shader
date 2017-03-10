Shader "CookbookShaders/NormalMapShader" {
	Properties {
		_MainTint ("Diffuse Tint", Color) = (1,1,1,1)
		_NormalTex ("Normal Map", 2D) = "bump" {}
		_NormalMapIntensity("Normal Intensity", Range(0,10)) = 1
	}
	SubShader {
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		sampler2D _NormalTex;
		float4 _MainTint;
		float _NormalMapIntensity;
		struct Input {
			float2 uv_NormalTex;
		};
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _MainTint.rgb;
			// float3 normalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
			// o.Normal = normalMap.rgb;

			fixed3 n = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex)).rgb;
			n.x *= _NormalMapIntensity;
			n.y *= _NormalMapIntensity;
			o.Normal = normalize(n);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
