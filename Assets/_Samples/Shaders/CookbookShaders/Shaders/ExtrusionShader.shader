Shader "CookbookShaders/ExtrusionShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount ("Extrusion Amount", Range(-1, 1)) = 0
		_NormalTex ("Normal Map", 2D) = "bump" {}
		_NormalMapIntensity("Normal Intensity", Range(0,10)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalTex;
		float _NormalMapIntensity;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _Amout;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalTex;
		};

		void vert (inout appdata_full v)
		{
			v.vertex.xyz += v.normal * _Amout;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

			// Normal map
			fixed3 n = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex)).rgb;
			n.x *= _NormalMapIntensity;
			n.y *= _NormalMapIntensity;
			o.Normal = normalize(n);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
