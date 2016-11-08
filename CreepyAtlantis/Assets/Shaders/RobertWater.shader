Shader "Unlit/RobertWater"
{
	// "public variables" exposed to the material
	Properties
	{
	// _MainTex is internal variable name
	// "Texture" is the label displayed in Unity Inspector
	// [2D] is type of variable
	// "white" is the default value
		_MainTex ("Texture", 2D) = "white" {}
		_WaveFreq("Wave Frequency Scalar", Float) = 10
		_Amplitude("Amplitude", Float) = 2
	}

	// Let's actually shading stuff...
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100 // Level of Detail

		// One draw call
		Pass
		{
			CGPROGRAM // Shader code starts here, use HLSL when googling
			// pragma is pre-compiler directive
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			// ".cginc" is just more shader code
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			// vertex 2 fragment
			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			// needs to match properties version
			sampler2D _MainTex;
			float4 _MainTex_ST; // S = Scale, T = Transform
			float _WaveFreq;
			float _Amplitude;
			
			v2f vert (appdata v)
			{
				v2f o;
				// mul = "multiply", MVP = "Model View Projection"
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.vertex += float4(sin(_Time.y* _WaveFreq + o.vertex.z ) * _Amplitude, sin(_Time.y* -_WaveFreq * 3 + o.vertex.x) * _Amplitude, 0, 0);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			// Runs for every pixel
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv + float2(_Time.x, 0));
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
