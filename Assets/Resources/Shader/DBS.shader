Shader "Custom/DBS" 
{
	Properties 
	{
		_MainTex ("Diffuse Texture", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_SpecularTex ("Specular Texture", 2D) = "white" {}
		_SpecPow ("Specular Power", Range(0.01, 128)) = 45
    }
    
	SubShader 
	{
		Tags { "RenderType" = "Opaque" }
		LOD 500

		CGPROGRAM
		#pragma surface surf SpecularReflections
      
		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _SpecularTex;
		float _SpecPow;

		half4 LightingSpecularReflections (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
		{
			half3 h = normalize (lightDir + viewDir);

			half diff = max (0, dot (s.Normal, lightDir));
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, s.Specular * _SpecPow) * s.Gloss;

			half4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * s.Gloss) * (atten * 2);
			c.a = s.Alpha + _LightColor0.a * spec * atten;
			return c;
		}

		struct Input 
		{
			float2 uv_MainTex;
		};
      
		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 texD = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 texS = tex2D (_SpecularTex, IN.uv_MainTex);

			o.Albedo = texD.rgb;
			o.Specular = texS.rgb * _SpecPow;
			o.Gloss = 1.0;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		}
		ENDCG
	}
    Fallback "Diffuse"
}