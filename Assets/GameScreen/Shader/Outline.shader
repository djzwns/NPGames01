﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline" 
{
	Properties {
		_MainTex( "Base 2D", 2D ) = ""{}
		_Color( "color", Color ) = (1, 1, 1, 1)
		_OutlineColor( "Outline Color", Color ) = (1, 1, 1, 1)
		_OutlineSize( "Outline Size", Range( 0.0, 1.0 )) = 0.1
		_Cutoff( "CutOff Shadow Alpha", Range( 0.0, 1.0 )) = 1
	}
	CGINCLUDE
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform fixed4 _OutlineColor;
		uniform fixed _OutlineSize;

		struct v2f
		{
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
		};

		fixed4 frag( v2f i ) : COLOR 
		{
			return float4(_OutlineColor.xyz, tex2D( _MainTex, i.uv ).a * _OutlineColor.a);
		}

		v2f vmain_scale( appdata_full v )
		{
			v2f o;

			o.pos = mul( UNITY_MATRIX_MV, float4(v.vertex.xy * float2(_OutlineSize, _OutlineSize), v.vertex.zw) );
			o.uv = v.texcoord.xy;

			return o;
		}

		v2f vmain( appdata_full v, float4 offsetVector )
		{
			v2f o;
			o.pos = UnityObjectToClipPos( v.vertex.xyzw + offsetVector );
			o.uv = v.texcoord.xy;
			return o;
		}
	ENDCG

		SubShader
		{
			Tags {"Queue" = "Tranparent" "IgnoreProjector" = "False" "RenderType" = "Transparent"}
			LOD 200

			Pass {
				Cull Back
				ZWrite Off
				ZTest LEqual
				AlphaTest greater 0
				Stencil {
					Ref 0
					Comp equal
					Pass incrsat

					ZFail keep
					Fail keep
				}
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				v2f vert( appdata_full v )
				{
					return vmain( v, float4(-_OutlineSize, 0, 0, 0) );
				}
				ENDCG
			}

			Pass {
				Name "Caster"
				Tags {"LightMode" = "ShadowCaster"}
				Offset 1, 1

					Fog {Mode Off}
					ZWrite On
					ZTest LEqual
					Cull Off
					Lighting Off

					CGPROGRAM
#pragma vertex vert_shadow
#pragma fragment frag_shadow
#pragma multi_compile_shadowcaster
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"
					struct v2f_shadow
					{
						V2F_SHADOW_CASTER;
						float2 uv : TEXCOORD1;
					};

					uniform float4 _MainText_ST;
					v2f_shadow vert_shadow( appdata_base v )
					{
						v2f_shadow o;
						TRANSFER_SHADOW_CASTER( o );
						o.uv = TRANSFORM_TEX( v.texcoord, _MainText );
						return o;
					}
					uniform fixed _Cutoff;

					float4 frag_shadow( v2f_shadow i ) : COLOR{
						fixed4 texcol = tex2D( _MainTex, i.uv );
						clip( texcol.a - _Cutoff );
						SHADOW_CASTER_FRAGMENT(i)
					}
					ENDCG
			}
		}
	FallBack "Diffuse"
}
