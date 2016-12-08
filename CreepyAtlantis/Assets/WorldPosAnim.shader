// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32720,y:32715,varname:node_4013,prsc:2|diff-1304-RGB,voffset-8517-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32443,y:32712,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:11,c3:1,c4:1;n:type:ShaderForge.SFN_FragmentPosition,id:6839,x:30795,y:33198,varname:node_6839,prsc:2;n:type:ShaderForge.SFN_Add,id:3039,x:31682,y:33393,varname:node_3039,prsc:2|A-7371-OUT,B-4311-OUT;n:type:ShaderForge.SFN_Time,id:7914,x:31190,y:33188,varname:node_7914,prsc:2;n:type:ShaderForge.SFN_Sin,id:7924,x:31848,y:33393,varname:node_7924,prsc:2|IN-3039-OUT;n:type:ShaderForge.SFN_Append,id:8517,x:32509,y:33078,varname:node_8517,prsc:2|A-1592-OUT,B-7393-OUT,C-5447-OUT;n:type:ShaderForge.SFN_Vector1,id:1592,x:32515,y:33233,varname:node_1592,prsc:2,v1:0;n:type:ShaderForge.SFN_ObjectScale,id:1104,x:32119,y:33194,varname:node_1104,prsc:2,rcp:False;n:type:ShaderForge.SFN_Divide,id:5447,x:32195,y:33393,varname:node_5447,prsc:2|A-7696-OUT,B-1104-X;n:type:ShaderForge.SFN_Multiply,id:4311,x:31507,y:33533,varname:node_4311,prsc:2|A-7914-T,B-4143-OUT;n:type:ShaderForge.SFN_Vector1,id:4143,x:31508,y:33675,cmnt:Horz Freq,varname:node_4143,prsc:2,v1:-3;n:type:ShaderForge.SFN_Divide,id:7393,x:32194,y:32909,varname:node_7393,prsc:2|A-1653-OUT,B-1104-Y;n:type:ShaderForge.SFN_Sin,id:3578,x:31847,y:32909,varname:node_3578,prsc:2|IN-8935-OUT;n:type:ShaderForge.SFN_Add,id:8935,x:31681,y:32909,varname:node_8935,prsc:2|A-6358-OUT,B-4446-OUT;n:type:ShaderForge.SFN_Multiply,id:4446,x:31506,y:33049,varname:node_4446,prsc:2|A-7914-T,B-7203-OUT;n:type:ShaderForge.SFN_Vector1,id:7203,x:31509,y:33188,cmnt:Vert Freq,varname:node_7203,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:1653,x:32023,y:32908,varname:node_1653,prsc:2|A-3578-OUT,B-6426-OUT;n:type:ShaderForge.SFN_Multiply,id:7696,x:32023,y:33393,varname:node_7696,prsc:2|A-7924-OUT,B-2212-OUT;n:type:ShaderForge.SFN_Vector1,id:6426,x:32018,y:33047,cmnt:Vert Amp,varname:node_6426,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Vector1,id:2212,x:32022,y:33529,cmnt:Horz Amp,varname:node_2212,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:7371,x:31514,y:33406,varname:node_7371,prsc:2|A-2288-OUT,B-4423-OUT;n:type:ShaderForge.SFN_Multiply,id:6358,x:31506,y:32924,varname:node_6358,prsc:2|A-3785-OUT,B-8843-OUT;n:type:ShaderForge.SFN_Vector1,id:2288,x:31515,y:33344,cmnt:Horz Pd,varname:node_2288,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:3785,x:31506,y:32859,cmnt:Vert Pd,varname:node_3785,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:8843,x:31300,y:32933,varname:node_8843,prsc:2|A-6839-Y,B-6839-Z;n:type:ShaderForge.SFN_Add,id:4423,x:31302,y:33423,varname:node_4423,prsc:2|A-6839-X,B-6839-Z;proporder:1304;pass:END;sub:END;*/

Shader "Shader Forge/WorldPosAnim" {
    Properties {
        _Color ("Color", Color) = (1,11,1,1)
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_7914 = _Time + _TimeEditor;
                v.vertex.xyz += float3(0.0,((sin(((0.5*(mul(unity_ObjectToWorld, v.vertex).g+mul(unity_ObjectToWorld, v.vertex).b))+(node_7914.g*(-1.0))))*0.2)/objScale.g),((sin(((2.0*(mul(unity_ObjectToWorld, v.vertex).r+mul(unity_ObjectToWorld, v.vertex).b))+(node_7914.g*(-3.0))))*0.2)/objScale.r));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float4 node_7914 = _Time + _TimeEditor;
                v.vertex.xyz += float3(0.0,((sin(((0.5*(mul(unity_ObjectToWorld, v.vertex).g+mul(unity_ObjectToWorld, v.vertex).b))+(node_7914.g*(-1.0))))*0.2)/objScale.g),((sin(((2.0*(mul(unity_ObjectToWorld, v.vertex).r+mul(unity_ObjectToWorld, v.vertex).b))+(node_7914.g*(-3.0))))*0.2)/objScale.r));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = _Color.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
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
                float4 node_7914 = _Time + _TimeEditor;
                v.vertex.xyz += float3(0.0,((sin(((0.5*(mul(unity_ObjectToWorld, v.vertex).g+mul(unity_ObjectToWorld, v.vertex).b))+(node_7914.g*(-1.0))))*0.2)/objScale.g),((sin(((2.0*(mul(unity_ObjectToWorld, v.vertex).r+mul(unity_ObjectToWorld, v.vertex).b))+(node_7914.g*(-3.0))))*0.2)/objScale.r));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
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
