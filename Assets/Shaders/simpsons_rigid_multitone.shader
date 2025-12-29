Shader "Custom/simpsons_rigid_multitone"
{
    Properties
    {
        _Tex0("Tex0", 2D) = "white"
    	_Tex1("Tex1", 2D) = "white"
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            	float2 uv1 : TEXCOORD1;
            };

            TEXTURE2D(_Tex0);
            SAMPLER(sampler_Tex0);

            TEXTURE2D(_Tex1);
            SAMPLER(sampler_Tex1);

            CBUFFER_START(UnityPerMaterial)
                float4 _Tex0_ST;
				float4 _Tex1_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv0 = TRANSFORM_TEX(IN.uv0, _Tex0);
                OUT.uv1 = TRANSFORM_TEX(IN.uv1, _Tex1);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 palColor = SAMPLE_TEXTURE2D(_Tex0, sampler_Tex0, IN.uv0);
                half4 baseColor = SAMPLE_TEXTURE2D(_Tex1, sampler_Tex1, IN.uv0);
                return baseColor;
            }
            ENDHLSL
        }
    }
}
