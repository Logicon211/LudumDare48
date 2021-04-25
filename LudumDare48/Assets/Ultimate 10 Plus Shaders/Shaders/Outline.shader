/*
    ░█████╗░██╗░░░██╗████████╗██╗░░░░░██╗███╗░░██╗███████╗  ░██████╗██╗░░██╗░█████╗░██████╗░███████╗██████╗░
    ██╔══██╗██║░░░██║╚══██╔══╝██║░░░░░██║████╗░██║██╔════╝  ██╔════╝██║░░██║██╔══██╗██╔══██╗██╔════╝██╔══██╗
    ██║░░██║██║░░░██║░░░██║░░░██║░░░░░██║██╔██╗██║█████╗░░  ╚█████╗░███████║███████║██║░░██║█████╗░░██████╔╝
    ██║░░██║██║░░░██║░░░██║░░░██║░░░░░██║██║╚████║██╔══╝░░  ░╚═══██╗██╔══██║██╔══██║██║░░██║██╔══╝░░██╔══██╗
    ╚█████╔╝╚██████╔╝░░░██║░░░███████╗██║██║░╚███║███████╗  ██████╔╝██║░░██║██║░░██║██████╔╝███████╗██║░░██║
    ░╚════╝░░╚═════╝░░░░╚═╝░░░╚══════╝╚═╝╚═╝░░╚══╝╚══════╝  ╚═════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═════╝░╚══════╝╚═╝░░╚═╝

                █▀▀▄ █──█ 　 ▀▀█▀▀ █──█ █▀▀ 　 ░█▀▀▄ █▀▀ ▀█─█▀ █▀▀ █── █▀▀█ █▀▀█ █▀▀ █▀▀█ 
                █▀▀▄ █▄▄█ 　 ─░█── █▀▀█ █▀▀ 　 ░█─░█ █▀▀ ─█▄█─ █▀▀ █── █──█ █──█ █▀▀ █▄▄▀ 
                ▀▀▀─ ▄▄▄█ 　 ─░█── ▀──▀ ▀▀▀ 　 ░█▄▄▀ ▀▀▀ ──▀── ▀▀▀ ▀▀▀ ▀▀▀▀ █▀▀▀ ▀▀▀ ▀─▀▀
____________________________________________________________________________________________________________________________________________

        ▄▀█ █▀ █▀ █▀▀ ▀█▀ ▀   █░█ █░░ ▀█▀ █ █▀▄▀█ ▄▀█ ▀█▀ █▀▀   ▄█ █▀█ ▄█▄   █▀ █░█ ▄▀█ █▀▄ █▀▀ █▀█ █▀
        █▀█ ▄█ ▄█ ██▄ ░█░ ▄   █▄█ █▄▄ ░█░ █ █░▀░█ █▀█ ░█░ ██▄   ░█ █▄█ ░▀░   ▄█ █▀█ █▀█ █▄▀ ██▄ █▀▄ ▄█
____________________________________________________________________________________________________________________________________________
License:
    The license is ATTRIBUTION 3.0

    More license info here:
        https://creativecommons.org/licenses/by/3.0/
____________________________________________________________________________________________________________________________________________
This shader has NOT been tested on any other PC configuration except the following:
    CPU: Intel Core i5-6400
    GPU: NVidia GTX 750Ti
    RAM: 16GB
    Windows: 10 x64
    DirectX: 11
____________________________________________________________________________________________________________________________________________
*/

Shader "Ultimate 10+ Shaders/Outline"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Range(0, 4)) = 0.25
        _MovementDirection ("Movement Direction", float) = (0, -1, 0, 1)
        _RimPower("Rim Power", Range(0, 4)) = 0.25
        
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Geometry" "Queue"="Transparent" }
        LOD 200
        Cull [_Cull]

        Pass{
            ZWrite Off
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f{
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
            };

            fixed4 _OutlineColor;
            half _OutlineWidth;

            v2f vert(appdata input){
                input.vertex += float4(input.normal * _OutlineWidth, 1);

                v2f output;

                output.pos = UnityObjectToClipPos(input.vertex);
                output.normal = mul(unity_ObjectToWorld, input.normal);

                return output;
            }

            fixed4 frag(v2f input) : SV_Target
            {
                return _OutlineColor;
            }

            ENDCG
        }

        ZWrite On
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        #ifndef SHADER_API_D3D11
            #pragma target 3.0
        #else
            #pragma target 4.0
        #endif

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half2 _MovementDirection;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;

        };

        fixed4 _Color;
        sampler2D _MainTex;

        fixed4 _OutlineColor;
        float _RimPower;
        fixed4 pixel;
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            pixel = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            fixed2 scrolledUV = IN.uv_MainTex;

            fixed xScrollValue = 2 * _Time;
            fixed yScrollValue = 2 * _Time;

            scrolledUV += fixed2(xScrollValue, yScrollValue);

            half4 c = tex2D(_MainTex, scrolledUV) * _Color;


            o.Albedo = c.rgb;

            half rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));
            o.Emission = pow(rim, _RimPower) * _OutlineColor;
            //o.Emission = rim * _OutlineColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
