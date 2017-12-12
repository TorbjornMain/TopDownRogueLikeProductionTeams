Shader "Unlit/MinimapReplacement"
{
	Properties
	{
		_MinimapColor("Minimap Color", Color) = (0.5,0.5,0.5,1)
		_MinimapTexture("Minimap Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "Minimap"="True" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

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

			fixed4 _MinimapColor;
			sampler2D _MinimapTexture;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _MinimapColor * tex2D(_MinimapTexture, i.uv);
				if (col.a < 0.1)
					discard;
				return col;
			}
			ENDCG
		}
	}
}
