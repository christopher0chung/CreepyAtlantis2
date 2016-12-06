// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-2460-OUT,custl-5085-OUT,voffset-8213-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:8068,x:32734,y:33086,varname:node_8068,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3406,x:32734,y:32952,varname:node_3406,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6869,x:31858,y:32654,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31858,y:32782,prsc:2,pt:True;n:type:ShaderForge.SFN_HalfVector,id:9471,x:31858,y:32933,varname:node_9471,prsc:2;n:type:ShaderForge.SFN_Dot,id:7782,x:32070,y:32697,cmnt:Lambert,varname:node_7782,prsc:2,dt:1|A-6869-OUT,B-9684-OUT;n:type:ShaderForge.SFN_Dot,id:3269,x:32070,y:32871,cmnt:Blinn-Phong,varname:node_3269,prsc:2,dt:1|A-9684-OUT,B-9471-OUT;n:type:ShaderForge.SFN_Multiply,id:2746,x:32465,y:32866,cmnt:Specular Contribution,varname:node_2746,prsc:2|A-7782-OUT,B-5267-OUT,C-4865-RGB;n:type:ShaderForge.SFN_Tex2d,id:851,x:32070,y:32349,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1941,x:32465,y:32693,cmnt:Diffuse Contribution,varname:node_1941,prsc:2|A-544-OUT,B-7782-OUT;n:type:ShaderForge.SFN_Color,id:5927,x:32070,y:32534,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5927,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Exp,id:1700,x:32070,y:33054,varname:node_1700,prsc:2,et:1|IN-9978-OUT;n:type:ShaderForge.SFN_Slider,id:5328,x:31529,y:33056,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_5328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Power,id:5267,x:32268,y:32940,varname:node_5267,prsc:2|VAL-3269-OUT,EXP-1700-OUT;n:type:ShaderForge.SFN_Add,id:2159,x:32734,y:32812,cmnt:Combine,varname:node_2159,prsc:2|A-1941-OUT,B-2746-OUT;n:type:ShaderForge.SFN_Multiply,id:5085,x:32979,y:32952,cmnt:Attenuate and Color,varname:node_5085,prsc:2|A-2159-OUT,B-3406-RGB,C-8068-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:9978,x:31858,y:33056,varname:node_9978,prsc:2,a:1,b:11|IN-5328-OUT;n:type:ShaderForge.SFN_Color,id:4865,x:32266,y:33095,ptovrint:False,ptlb:Spec Color,ptin:_SpecColor,varname:node_4865,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:32734,y:32646,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2460,x:32927,y:32598,cmnt:Ambient Light,varname:node_2460,prsc:2|A-544-OUT,B-7528-RGB;n:type:ShaderForge.SFN_Multiply,id:544,x:32268,y:32448,cmnt:Diffuse Color,varname:node_544,prsc:2|A-851-RGB,B-5927-RGB;n:type:ShaderForge.SFN_FragmentPosition,id:1431,x:31354,y:33590,varname:node_1431,prsc:2;n:type:ShaderForge.SFN_Add,id:9563,x:31859,y:33325,varname:node_9563,prsc:2|A-1431-Y,B-1431-Z;n:type:ShaderForge.SFN_Time,id:7353,x:31860,y:33449,varname:node_7353,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5476,x:32065,y:33316,varname:node_5476,prsc:2|A-8013-OUT,B-9563-OUT;n:type:ShaderForge.SFN_Multiply,id:9341,x:32065,y:33441,varname:node_9341,prsc:2|A-7353-T,B-5014-OUT;n:type:ShaderForge.SFN_Add,id:2696,x:32240,y:33301,varname:node_2696,prsc:2|A-5476-OUT,B-9341-OUT;n:type:ShaderForge.SFN_Sin,id:284,x:32406,y:33301,varname:node_284,prsc:2|IN-2696-OUT;n:type:ShaderForge.SFN_Multiply,id:7968,x:32582,y:33300,varname:node_7968,prsc:2|A-284-OUT,B-7701-OUT;n:type:ShaderForge.SFN_Divide,id:7535,x:32753,y:33301,varname:node_7535,prsc:2|A-7968-OUT,B-4318-Y;n:type:ShaderForge.SFN_Append,id:8213,x:33068,y:33470,varname:node_8213,prsc:2|A-6178-OUT,B-7535-OUT,C-3309-OUT;n:type:ShaderForge.SFN_Vector1,id:6178,x:33074,y:33625,varname:node_6178,prsc:2,v1:0;n:type:ShaderForge.SFN_ObjectScale,id:4318,x:32751,y:33633,varname:node_4318,prsc:2,rcp:False;n:type:ShaderForge.SFN_Divide,id:3309,x:32754,y:33785,varname:node_3309,prsc:2|A-7686-OUT,B-4318-X;n:type:ShaderForge.SFN_Multiply,id:7686,x:32582,y:33785,varname:node_7686,prsc:2|A-9843-OUT,B-6111-OUT;n:type:ShaderForge.SFN_Vector1,id:6111,x:32581,y:33921,cmnt:Horz Amp,varname:node_6111,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Sin,id:9843,x:32407,y:33785,varname:node_9843,prsc:2|IN-9238-OUT;n:type:ShaderForge.SFN_Add,id:9238,x:32241,y:33785,varname:node_9238,prsc:2|A-6125-OUT,B-1355-OUT;n:type:ShaderForge.SFN_Multiply,id:6125,x:32073,y:33798,varname:node_6125,prsc:2|A-9404-OUT,B-6076-OUT;n:type:ShaderForge.SFN_Vector1,id:9404,x:32074,y:33736,cmnt:Horz Pd,varname:node_9404,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:1355,x:32066,y:33925,varname:node_1355,prsc:2|A-5606-T,B-3428-OUT;n:type:ShaderForge.SFN_Vector1,id:3428,x:32067,y:34067,cmnt:Horz Freq,varname:node_3428,prsc:2,v1:-3;n:type:ShaderForge.SFN_Time,id:5606,x:31861,y:33933,varname:node_5606,prsc:2;n:type:ShaderForge.SFN_Add,id:6076,x:31861,y:33815,varname:node_6076,prsc:2|A-1431-X,B-1431-Z;n:type:ShaderForge.SFN_Vector1,id:5014,x:32068,y:33580,cmnt:Vert Freq,varname:node_5014,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:8013,x:32065,y:33251,cmnt:Vert Pd,varname:node_8013,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:7701,x:32577,y:33439,cmnt:Vert Amp,varname:node_7701,prsc:2,v1:0.2;proporder:851-5927-5328-4865;pass:END;sub:END;*/

Shader "Shader Forge/CustomLightingVertexAnim" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Gloss ("Gloss", Range(0, 1)) = 0
        _SpecColor ("Spec Color", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _Color;
            uniform float _Gloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_7353 = _Time + _TimeEditor;
                float4 node_5606 = _Time + _TimeEditor;
                v.vertex.xyz += float3(0.0,((sin(((0.5*(mul(unity_ObjectToWorld, v.vertex).g+mul(unity_ObjectToWorld, v.vertex).b))+(node_7353.g*(-1.0))))*0.2)/objScale.g),((sin(((2.0*(mul(unity_ObjectToWorld, v.vertex).r+mul(unity_ObjectToWorld, v.vertex).b))+(node_5606.g*(-3.0))))*0.2)/objScale.r));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 node_544 = (_Diffuse_var.rgb*_Color.rgb); // Diffuse Color
                float3 emissive = (node_544*UNITY_LIGHTMODEL_AMBIENT.rgb);
                float node_7782 = max(0,dot(lightDirection,normalDirection)); // Lambert
                float3 finalColor = emissive + (((node_544*node_7782)+(node_7782*pow(max(0,dot(normalDirection,halfDirection)),exp2(lerp(1,11,_Gloss)))*_SpecColor.rgb))*_LightColor0.rgb*attenuation);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _Color;
            uniform float _Gloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_7353 = _Time + _TimeEditor;
                float4 node_5606 = _Time + _TimeEditor;
                v.vertex.xyz += float3(0.0,((sin(((0.5*(mul(unity_ObjectToWorld, v.vertex).g+mul(unity_ObjectToWorld, v.vertex).b))+(node_7353.g*(-1.0))))*0.2)/objScale.g),((sin(((2.0*(mul(unity_ObjectToWorld, v.vertex).r+mul(unity_ObjectToWorld, v.vertex).b))+(node_5606.g*(-3.0))))*0.2)/objScale.r));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 node_544 = (_Diffuse_var.rgb*_Color.rgb); // Diffuse Color
                float node_7782 = max(0,dot(lightDirection,normalDirection)); // Lambert
                float3 finalColor = (((node_544*node_7782)+(node_7782*pow(max(0,dot(normalDirection,halfDirection)),exp2(lerp(1,11,_Gloss)))*_SpecColor.rgb))*_LightColor0.rgb*attenuation);
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_7353 = _Time + _TimeEditor;
                float4 node_5606 = _Time + _TimeEditor;
                v.vertex.xyz += float3(0.0,((sin(((0.5*(mul(unity_ObjectToWorld, v.vertex).g+mul(unity_ObjectToWorld, v.vertex).b))+(node_7353.g*(-1.0))))*0.2)/objScale.g),((sin(((2.0*(mul(unity_ObjectToWorld, v.vertex).r+mul(unity_ObjectToWorld, v.vertex).b))+(node_5606.g*(-3.0))))*0.2)/objScale.r));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
