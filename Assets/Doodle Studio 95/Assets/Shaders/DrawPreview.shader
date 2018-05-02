 Shader "Hidden/DrawPreview" {
 Properties {
     _Cutoff("Cutoff", float) = 0.5
     // added here to debug them in the inspector
     _Layer0 ("Layer", 2D) = "(0,0,0,0)" {}     
     _Layer1 ("Layer", 2D) = "(0,0,0,0)" {}     
     _Layer2 ("Layer", 2D) = "sdfsdfs" {}     
     _Layer3 ("Layer", 2D) = "(0,0,0,0)" {}     
 }
 
 SubShader {
     Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
     LOD 100
     
    //  ZWrite Off
    //  Blend SrcAlpha OneMinusSrcAlpha
    //  Cull Off 
     Lighting Off
     
     Pass {  
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             
             #include "UnityCG.cginc"
 
             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
             };
             float _Cutoff;
             sampler2D _Layer0;
             float4 _Layer0_ST;
             sampler2D _Layer1;
             sampler2D _Layer2;
             sampler2D _Layer3;
             
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _Layer0);
                 return o;
             }
             
             fixed4 frag (v2f i) : SV_Target
             {
                 fixed4 col = tex2D(_Layer0, i.texcoord);
                 clip(col.a - _Cutoff);

                 // Removing layer support for now   TODO: restore when multiple layers are supported
                 return col;
                 
                 fixed4 l = tex2D(_Layer1, i.texcoord);
                 col = lerp(col, l, l.a);
                 l = tex2D(_Layer2, i.texcoord);
                 col = lerp(col, l, l.a);
                 l = tex2D(_Layer3, i.texcoord);
                 return col;
             }
         ENDCG
     }
 }
 
 }