
Shader "MiDaEm/Spec Bump Emissive" {

Properties {


	_Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _BumpMap ("Normalmap", 2D) = "bump" {}
    
    _Shininess ("Shininess", Range (0.03, 1)) = 0.078125
	_SpecTex ("Spec (RGB), Gloss (A)", 2D) = "white" {}

    _Emissive("Emissive", 2D) = "black" {}
	_EmissiveColor("_EmissiveColor", Color) = (1,1,1,1)
	_EmissiveIntensity("_EmissiveIntensity", Range(0,10) ) = 0.5
    
    
 

}

SubShader { 

    Tags { "IgnoreProjector"="True" "RenderType"="Opaque" }

    LOD 400

    

CGPROGRAM
#pragma surface surf BlinnPhong alphatest:_Cutoff
#pragma exclude_renderers flash
 

inline fixed4 LightingMobileBlinnPhong (SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)

{

    fixed diff = max (0, dot (s.Normal, lightDir));

    fixed nh = max (0, dot (s.Normal, halfDir));

    fixed spec = pow (nh, s.Specular*128) * s.Gloss;

    

    fixed4 c;

    c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten*2);

    c.a = 0.0;

    return c;

}

 

sampler2D _MainTex;

sampler2D _BumpMap;
fixed4 _Color;

half _Shininess;

sampler2D _Emissive;

float4 _EmissiveColor;

float _EmissiveIntensity;

sampler2D _SpecTex;
 

struct Input {

    float2 uv_MainTex;

    float2 uv_Emissive;

};

 

void surf (Input IN, inout SurfaceOutput o) {

    fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
    fixed4 specTex = tex2D(_SpecTex, IN.uv_MainTex);
    
    _SpecColor = specTex;

    o.Albedo = tex.rgb * _Color.rgb;

    o.Alpha = tex.a;
    
    o.Gloss = specTex.a;

    o.Specular = _Shininess;

    o.Normal = UnpackNormal (tex2D(_BumpMap, IN.uv_MainTex));

    

    float4 Tex2D1=tex2D(_Emissive,(IN.uv_Emissive.xyxy).xy);

    float4 Multiply0=Tex2D1 * _EmissiveColor;

    float4 Multiply2=Multiply0 * _EmissiveIntensity.xxxx;

    

    o.Emission = Multiply2;

}

ENDCG

}

 

FallBack "Bumped/Specular"

}