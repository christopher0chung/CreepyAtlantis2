// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.29 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.29;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:30,fgrf:100,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-1345-RGB,custl-4696-OUT;n:type:ShaderForge.SFN_Slider,id:6812,x:30898,y:33403,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4682218,max:1;n:type:ShaderForge.SFN_Vector1,id:410,x:31055,y:33473,varname:node_410,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:5980,x:31468,y:33390,varname:node_5980,prsc:2|A-6812-OUT,B-410-OUT;n:type:ShaderForge.SFN_Vector1,id:2005,x:31468,y:33540,varname:node_2005,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:8057,x:31636,y:33452,varname:node_8057,prsc:2|A-5980-OUT,B-2005-OUT;n:type:ShaderForge.SFN_Exp,id:98,x:31807,y:33452,varname:node_98,prsc:2,et:1|IN-8057-OUT;n:type:ShaderForge.SFN_HalfVector,id:2244,x:31636,y:33295,varname:node_2244,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:6544,x:31636,y:33156,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:8614,x:31636,y:33035,varname:node_8614,prsc:2;n:type:ShaderForge.SFN_Dot,id:1697,x:31845,y:33062,varname:node_1697,prsc:2,dt:1|A-8614-OUT,B-6544-OUT;n:type:ShaderForge.SFN_Dot,id:1159,x:31845,y:33235,varname:node_1159,prsc:2,dt:1|A-6544-OUT,B-2244-OUT;n:type:ShaderForge.SFN_Power,id:226,x:32047,y:33335,cmnt:Specular Light,varname:node_226,prsc:2|VAL-1159-OUT,EXP-98-OUT;n:type:ShaderForge.SFN_Posterize,id:968,x:32282,y:33286,varname:node_968,prsc:2|IN-226-OUT,STPS-5730-OUT;n:type:ShaderForge.SFN_Posterize,id:4412,x:32282,y:33155,varname:node_4412,prsc:2|IN-1697-OUT,STPS-5730-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5730,x:32047,y:33234,ptovrint:False,ptlb:Bands,ptin:_Bands,varname:_Bands,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:2937,x:32282,y:32989,ptovrint:False,ptlb:DiffuseColor,ptin:DiffuseColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4140,x:32487,y:32971,cmnt:Diffuse Light,varname:node_4140,prsc:2|A-2937-RGB,B-4412-OUT;n:type:ShaderForge.SFN_AmbientLight,id:91,x:32487,y:33091,varname:node_91,prsc:2;n:type:ShaderForge.SFN_Add,id:9775,x:32786,y:33099,varname:node_9775,prsc:2|A-4140-OUT,B-91-RGB,C-968-OUT;n:type:ShaderForge.SFN_LightColor,id:1090,x:32786,y:32966,varname:node_1090,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:2642,x:32786,y:32837,varname:node_2642,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4696,x:32970,y:32966,varname:node_4696,prsc:2|A-2642-OUT,B-1090-RGB,C-9775-OUT;n:type:ShaderForge.SFN_Color,id:1345,x:32988,y:32721,ptovrint:False,ptlb:node_1345,ptin:_node_1345,varname:node_1345,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:6812-5730-2937-1345;pass:END;sub:END;*/

Shader "Shader Forge/Posterize" {
    Properties {
        _Gloss ("Gloss", Range(0, 1)) = 0.4682218
        _Bands ("Bands", Float ) = 2
        DiffuseColor ("DiffuseColor", Color) = (0.5,0.5,0.5,1)
        _node_1345 ("node_1345", Color) = (0.5,0.5,0.5,1)
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
            uniform float _Gloss;
            uniform float _Bands;
            uniform float4 DiffuseColor;
            uniform float4 _node_1345;
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
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float3 emissive = _node_1345.rgb;
                float3 finalColor = emissive + (attenuation*_LightColor0.rgb*((DiffuseColor.rgb*floor(max(0,dot(lightDirection,normalDirection)) * _Bands) / (_Bands - 1))+UNITY_LIGHTMODEL_AMBIENT.rgb+floor(pow(max(0,dot(normalDirection,halfDirection)),exp2(((_Gloss*10.0)+1.0))) * _Bands) / (_Bands - 1)));
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
            uniform float _Gloss;
            uniform float _Bands;
            uniform float4 DiffuseColor;
            uniform float4 _node_1345;
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
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 finalColor = (attenuation*_LightColor0.rgb*((DiffuseColor.rgb*floor(max(0,dot(lightDirection,normalDirection)) * _Bands) / (_Bands - 1))+UNITY_LIGHTMODEL_AMBIENT.rgb+floor(pow(max(0,dot(normalDirection,halfDirection)),exp2(((_Gloss*10.0)+1.0))) * _Bands) / (_Bands - 1)));
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
