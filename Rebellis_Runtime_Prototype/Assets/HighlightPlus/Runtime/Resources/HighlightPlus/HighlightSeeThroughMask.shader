Shader "HighlightPlus/Geometry/SeeThroughMask" {
Properties {
    _MainTex ("Texture", Any) = "white" {}
    _Color ("Color", Color) = (1,1,1) // not used; dummy property to avoid inspector warning "material has no _Color property"
}
     SubShader
    {
        Tags { "Queue"="Transparent+201" "RenderType"="Transparent" "DisableBatching"="True" }
   
        // See through effect
        Pass
        { 
            Name "See-through mask"
            Stencil {
                WriteMask 3
                Ref 1
                Comp always
                Pass replace
            }

            ZTest Always
            ZWrite On
            ColorMask 0

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "CustomVertexTransform.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos: SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.pos    = ComputeVertexPosition(v.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return 0;
            }
            ENDCG
        }

    }
}