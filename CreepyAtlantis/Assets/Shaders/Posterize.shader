// Shader created with Shader Forge v1.33 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.33;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.03921569,fgcg:0.09019608,fgcb:0.07843138,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-5544-OUT,custl-3227-OUT;n:type:ShaderForge.SFN_Slider,id:6812,x:30560,y:33793,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4682218,max:1;n:type:ShaderForge.SFN_Vector1,id:410,x:30717,y:33863,varname:node_410,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:5980,x:31130,y:33780,varname:node_5980,prsc:2|A-6812-OUT,B-410-OUT;n:type:ShaderForge.SFN_Vector1,id:2005,x:31130,y:33930,varname:node_2005,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:8057,x:31298,y:33842,varname:node_8057,prsc:2|A-5980-OUT,B-2005-OUT;n:type:ShaderForge.SFN_Exp,id:98,x:31469,y:33842,varname:node_98,prsc:2,et:1|IN-8057-OUT;n:type:ShaderForge.SFN_HalfVector,id:2244,x:31298,y:33685,varname:node_2244,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:6544,x:31298,y:33546,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:8614,x:31298,y:33425,varname:node_8614,prsc:2;n:type:ShaderForge.SFN_Dot,id:1697,x:31507,y:33452,varname:node_1697,prsc:2,dt:1|A-8614-OUT,B-6544-OUT;n:type:ShaderForge.SFN_Dot,id:1159,x:31507,y:33625,varname:node_1159,prsc:2,dt:1|A-6544-OUT,B-2244-OUT;n:type:ShaderForge.SFN_Power,id:226,x:31709,y:33725,cmnt:Specular Light,varname:node_226,prsc:2|VAL-1159-OUT,EXP-98-OUT;n:type:ShaderForge.SFN_Posterize,id:968,x:31944,y:33676,varname:node_968,prsc:2|IN-226-OUT,STPS-5730-OUT;n:type:ShaderForge.SFN_Posterize,id:4412,x:31944,y:33545,varname:node_4412,prsc:2|IN-1697-OUT,STPS-5730-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5730,x:31709,y:33624,ptovrint:False,ptlb:Bands,ptin:_Bands,varname:_Bands,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:2937,x:31944,y:33379,ptovrint:False,ptlb:DiffuseColor,ptin:DiffuseColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4140,x:32149,y:33361,cmnt:Diffuse Light,varname:node_4140,prsc:2|A-2937-RGB,B-4412-OUT;n:type:ShaderForge.SFN_AmbientLight,id:91,x:32149,y:33481,varname:node_91,prsc:2;n:type:ShaderForge.SFN_Add,id:9775,x:32448,y:33489,varname:node_9775,prsc:2|A-4140-OUT,B-91-RGB,C-968-OUT;n:type:ShaderForge.SFN_LightColor,id:1090,x:32448,y:33356,varname:node_1090,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:2642,x:32448,y:33227,varname:node_2642,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4696,x:32632,y:33356,varname:node_4696,prsc:2|A-2642-OUT,B-1090-RGB,C-9775-OUT;n:type:ShaderForge.SFN_Color,id:1345,x:32559,y:32640,ptovrint:False,ptlb:EmissionColor,ptin:_EmissionColor,varname:node_1345,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:8894,x:32602,y:32936,ptovrint:False,ptlb:FogColor,ptin:_FogColor,varname:node_8894,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:1346,x:31853,y:33932,ptovrint:False,ptlb:node_1346,ptin:_node_1346,varname:node_1346,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:4820,x:31854,y:34109,ptovrint:False,ptlb:node_4820,ptin:_node_4820,varname:node_4820,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:6652,x:32182,y:33999,varname:node_6652,prsc:2|A-1346-RGB,B-4820-RGB;n:type:ShaderForge.SFN_Add,id:9161,x:32180,y:34158,varname:node_9161,prsc:2|A-1346-RGB,B-4820-RGB;n:type:ShaderForge.SFN_Subtract,id:2419,x:32182,y:33869,varname:node_2419,prsc:2|A-1346-RGB,B-4820-RGB;n:type:ShaderForge.SFN_Divide,id:1233,x:32181,y:33731,varname:node_1233,prsc:2|A-1346-RGB,B-4820-RGB;n:type:ShaderForge.SFN_Lerp,id:3030,x:32861,y:32816,varname:node_3030,prsc:2|A-1345-RGB,B-8894-RGB,T-8222-OUT;n:type:ShaderForge.SFN_Lerp,id:3227,x:33018,y:33006,varname:node_3227,prsc:2|A-4696-OUT,B-8894-RGB,T-8222-OUT;n:type:ShaderForge.SFN_Divide,id:8222,x:32401,y:32813,varname:node_8222,prsc:2|A-1410-Z,B-5749-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:1410,x:32073,y:32718,varname:node_1410,prsc:2;n:type:ShaderForge.SFN_Vector1,id:5749,x:32097,y:32947,varname:node_5749,prsc:2,v1:100;n:type:ShaderForge.SFN_Multiply,id:5544,x:33042,y:32794,varname:node_5544,prsc:2|A-6819-OUT,B-3030-OUT;n:type:ShaderForge.SFN_Slider,id:6819,x:32809,y:32625,ptovrint:False,ptlb:Emission Multiplier,ptin:_EmissionMultiplier,varname:node_6819,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:5;proporder:6812-5730-2937-1345-8894-6819;pass:END;sub:END;*/

Shader "Shader Forge/Posterize" {
    Properties {
        _Gloss ("Gloss", Range(0, 1)) = 0.4682218
        _Bands ("Bands", Float ) = 2
        DiffuseColor ("DiffuseColor", Color) = (0.5,0.5,0.5,1)
        _EmissionColor ("EmissionColor", Color) = (0.5,0.5,0.5,1)
        _FogColor ("FogColor", Color) = (0,0,0,1)
        _EmissionMultiplier ("Emission Multiplier", Range(0, 5)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform float _Gloss;
            uniform float _Bands;
            uniform float4 DiffuseColor;
            uniform float4 _EmissionColor;
            uniform float4 _FogColor;
            uniform float _EmissionMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float node_8222 = (i.posWorld.b/100.0);
                float3 emissive = (_EmissionMultiplier*lerp(_EmissionColor.rgb,_FogColor.rgb,node_8222));
                float3 finalColor = emissive + lerp((attenuation*_LightColor0.rgb*((DiffuseColor.rgb*floor(max(0,dot(lightDirection,normalDirection)) * _Bands) / (_Bands - 1))+UNITY_LIGHTMODEL_AMBIENT.rgb+floor(pow(max(0,dot(normalDirection,halfDirection)),exp2(((_Gloss*10.0)+1.0))) * _Bands) / (_Bands - 1))),_FogColor.rgb,node_8222);
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform float _Gloss;
            uniform float _Bands;
            uniform float4 DiffuseColor;
            uniform float4 _EmissionColor;
            uniform float4 _FogColor;
            uniform float _EmissionMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_8222 = (i.posWorld.b/100.0);
                float3 finalColor = lerp((attenuation*_LightColor0.rgb*((DiffuseColor.rgb*floor(max(0,dot(lightDirection,normalDirection)) * _Bands) / (_Bands - 1))+UNITY_LIGHTMODEL_AMBIENT.rgb+floor(pow(max(0,dot(normalDirection,halfDirection)),exp2(((_Gloss*10.0)+1.0))) * _Bands) / (_Bands - 1))),_FogColor.rgb,node_8222);
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
