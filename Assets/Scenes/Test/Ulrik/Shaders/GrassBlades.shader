Shader "Custom/GrassBlades"
{
    Properties
    {
        _BaseColor("Base color", Color) = (0, 0.5, 0,1) // Color of the base
        _TipColor("Tip color", Color) = (0, 1, 0, 0)// Color of the tip
    }
    SubShader
    {
        // UniversalPipeline needed to have this render in URP
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "IgnoreProjector" = "True"}

        Pass{
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}
            Cull off // No culling since the grass must be double sided
            
            HLSLPROGRAM
            //Signal this shader requires a compute buffer
            #pragma prefer _hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 5.0

            //Lighting and shadow keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHT
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT

            // Register our functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            // Include logic file
            #include "GrassBlades.hlsl"

            ENDHLSL
        } 
    }
    FallBack "Diffuse"
}
