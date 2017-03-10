Shader "CookbookShaders/SilouhetteShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DotProduct ("Rim Effect", Range(-1,1)) = 0.25
	}
	SubShader {
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		Cull Off
		LOD 200
		// Lighting off

		Pass {
	        ZWrite On
	        ColorMask 0
	   
	        CGPROGRAM
	        #pragma vertex vert
	        #pragma fragment frag
	        #include "UnityCG.cginc"
	 
	        struct v2f {
	            float4 pos : SV_POSITION;
	        };
	 
	        v2f vert (appdata_base v)
	        {
	            v2f o;
	            o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	            return o;
	        }
	 
	        half4 frag (v2f i) : COLOR
	        {
	            return half4 (0,0,0,0);
	        }
	        ENDCG  
    	}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha:fade
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		sampler2D _MainTex;
		struct Input {
			float2 uv_MainTex;
			float3 worldNormal;
			float3 viewDir;
		};
		fixed4 _Color;
		float _DotProduct;

		void surf (Input IN, inout SurfaceOutput o) {
			float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			float border = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
			float alpha = (border * (1 - _DotProduct) + _DotProduct);
			o.Alpha = c.a * alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
