Shader "PPE/HandDrawnShader"
{
	Properties
	{
		_ScreenWidth("Screen Width", Int) = 720
		_ScreenHeight("Screen Height", Int) = 720

		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			int _ScreenWidth;
			int _ScreenHeight;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed3 col = tex2D(_MainTex, i.uv);
				
				int xKernel[3][3] = { {-1,-2,-1}, {0,0,0}, {1, 2, 1} };
				int yKernel[3][3] = { {-1,0,1 },{ -2,0,2},{ -1, 0, 1 } };

				int pixelX = i.uv.x * _ScreenWidth;
				int pixelY = i.uv.y * _ScreenHeight;

				float xVal = 0;
				float yVal = 0;
				for (int x = 0; x < 3; x++)
				{
					for (int y = 0; y < 3; y++)
					{
						int refX = pixelX + x - 1;
						int refY = pixelY + y - 1;

						float2 uv = float2((float)(refX) / _ScreenWidth, (float)(refY) / _ScreenHeight);

						fixed3 pixelCol = tex2D(_MainTex, uv);

						float grayScale = (pixelCol.r + pixelCol.g + pixelCol.b) / 3;

						xVal += grayScale * xKernel[x][y];
						yVal += grayScale * yKernel[x][y];
					}
				}
				
				//col.rgb *= 0.5;

				float colour = 1 - (sqrt((xVal * xVal) + (yVal * yVal)));

				if (colour < 0.1)
				{
					col.rgb = colour.rrr;
				}
				/*if (col.b < 0.5)
				{
					col.b = col.r;
				}
				*/

		
				// Returns final color using alpha blending
				return fixed4(col, 1.0);
			}
			ENDCG
		}
	}
}


