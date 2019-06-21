Shader "Custom/UnlitOutlined"{
    //show values to edit in inspector
    Properties{
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0,10)) = 0.03

        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _BlinkFrequency ("Blink Frequency", float) = 2
        _MinAlpha ("Min Alpha", range(0,1)) = 0.5
        _BlinkRisezer ("Blink Size Multiplier", float) = 2
    }

    SubShader{
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

        //The first pass where we render the Object itself
        Pass{
            CGPROGRAM

            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            //texture and transforms of the texture
            sampler2D _MainTex;
            float4 _MainTex_ST;

            //tint of the texture
            fixed4 _Color;
            fixed4 _Glow;

            //the object data that's put into the vertex shader
            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f{
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            //the vertex shader
            v2f vert(appdata v){
                v2f o;
                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET{
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;
                return col;
            }

            ENDCG
        }

        //The second pass where we render the outlines
        Pass{
            Cull front

            CGPROGRAM

            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            //color of the outline
            fixed4 _OutlineColor;
            //thickness of the outline
            float _OutlineThickness;
            float _BlinkRisezer;
            float _BlinkFrequency;
            float _MinAlpha;

            //the object data that's available to the vertex shader
            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f{
                float4 position : SV_POSITION;
            };

            //the vertex shader
            v2f vert(appdata v){
                v2f o;
                //calculate the position of the expanded object
                float3 normal = normalize(v.normal);
                float3 outlineOffset = normal * _OutlineThickness * (1+ (1 + sin(_Time.x * _BlinkFrequency))/2 * _BlinkRisezer);
                float3 position = v.vertex + outlineOffset;
                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(position);

                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET{
                float4 r = _OutlineColor;
                r.a = r.a * (_MinAlpha + ((1 + sin(_Time.x * _BlinkFrequency))/2)*(1-_MinAlpha));
                return r;
            }

            ENDCG
        }
    }

    //fallback which adds stuff we didn't implement like shadows and meta passes
    FallBack "Standard"
}