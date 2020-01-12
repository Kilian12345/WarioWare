Shader "Custom/SineWarp"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TimeTest ("_Time", float) = 10.0
		_Test1 ("_Test1", float) = 32.0
		_Test2 ("_Test2", float) = 0.1
		_Test3 ("_Test3", float) = 0.1

		_WaterDeep ("_WaterDeep", float) = 0.1
		_WaterDeepOffset ("_WaterDeepOffset", float) = 0.1


		_WaterColor ("_WaterColor", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 100
		GrabPass{
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 screenUV : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _TimeTest;
			float _Test1;
			float _Test2;
			float _Test3;

			fixed4 _WaterColor;
			float _WaterDeep;
			float _WaterDeepOffset;

			sampler2D _GrabTexture;// builtin uniform for grabbed texture
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.screenUV = ComputeGrabScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
				_WaterColor -= (i.screenUV.y + _WaterDeepOffset) * _WaterDeep;
				fixed4 grab = tex2Dproj(_GrabTexture, i.screenUV + float4( sin((_Time.x * _TimeTest)+i.screenUV.x* _Test1 + i.screenUV.y * _Test3)* _Test2, 0, 0, 0));
				fixed4 tex = tex2D(_MainTex, i.uv);

				tex.a = ceil(tex.a);
				//grab.rgb *= tex.a;

				return grab * _WaterColor;
			}
			ENDCG
		}
	}
}
