Shader "Custom/DefaultCull" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Player1Pos("Player1Pos", Vector) = (0,0,0,1)
		_Player2Pos("Player2Pos", Vector) = (0,0,0,1)
		_Player3Pos("Player3Pos", Vector) = (0,0,0,1)
		_Player4Pos("Player4Pos", Vector) = (0,0,0,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float4 _Player1Pos;
		float4 _Player2Pos;
		float4 _Player3Pos;
		float4 _Player4Pos;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			
			float4 pixelProj = mul(UNITY_MATRIX_VP, float4(IN.worldPos,1));
			float depth = pixelProj.z / pixelProj.w;
			//o.Albedo.rgb = depth;
			float4 player1Proj = mul(UNITY_MATRIX_VP, _Player1Pos);
			if (player1Proj.z / player1Proj.w < depth)
			{
				
			}
			if (distance(player1Proj.xy, pixelProj.xy) < 0.005)
			{
				o.Albedo.rgb = 1;
			}

		}
		ENDCG
	}
	Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
