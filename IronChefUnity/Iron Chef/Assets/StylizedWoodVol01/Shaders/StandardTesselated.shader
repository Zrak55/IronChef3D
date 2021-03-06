Shader "Standard (Tesselation)" 
{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map (RGB)", 2D) = "bump" {}
		_BumpScale("Normal Power" , Float) = 1
		_Occlusion("Occlusion Map (R)" , 2D) = "white" {}
		_Metallic("Metallic (R) , Smoothness (A)" , 2D) = "black" {}
		_ParallaxMap("Heightmap (A)", 2D) = "black" {}
		_EdgeLength("Edge length", Range(3,50)) = 10
		_Parallax("Height", Range(0.0, 2.0)) = 0.5
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma target 4.6
#pragma surface surf Standard fullforwardshadows vertex:disp tessellate:tessEdge
#include "Tessellation.cginc"

		sampler2D _ParallaxMap;
		float4 _ParallaxMap_ST;
		float _EdgeLength;
		fixed _Parallax;
		float _BumpScale;
		sampler2D _MainTex;
		sampler2D _Occlusion;
		sampler2D _BumpMap;
		sampler2D _Metallic;

		struct appdata 
		{
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
			float2 texcoord1 : TEXCOORD1;
			float2 texcoord2 : TEXCOORD2;
		};

		float4 tessEdge(appdata v0, appdata v1,appdata v2)
		{
			return UnityEdgeLengthBasedTessCull(v0.vertex, v1.vertex, v2.vertex, _EdgeLength, _Parallax * 1.5f);
		}

		void disp(inout appdata v)
		{
			float d = (tex2Dlod(_ParallaxMap, float4(v.texcoord * _ParallaxMap_ST,0,0)).a - 0.5 * 2) * _Parallax ;
			v.vertex.xyz += v.normal * d;
		}

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			fixed4 m = tex2D(_Metallic,IN.uv_MainTex);
			o.Metallic = m.r;
			o.Occlusion = tex2D(_Occlusion,IN.uv_MainTex).x;
			o.Smoothness = m.a;
			o.Normal = normalize(UnpackScaleNormal(tex2D(_BumpMap, IN.uv_MainTex), _BumpScale));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}