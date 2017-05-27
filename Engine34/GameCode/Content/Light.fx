
//sampler s0;
//
//texture lightMask;
//sampler lightSampler = sampler_state { Texture = <lightMask>; };
//
//float4 PixelShaderLight(float2 coords: TEXCOORD0) : COLOR0
//{
//	float4 color = tex2D(s0, coords);
//	float4 lightColor = tex2D(lightSampler, coords);
//	return color * lightColor;
//
//}
//
//technique Technique1
//{
//	 pass Pass1
//	{
//		 PixelShader = compile ps_4_0_level_9_1 PixelShaderLight();
//	}
//}
//






//texture ScreenTexture;
//
//sampler screen = sampler_state
//{
//	Texture = <ScreenTexture>;
//};

sampler TextureSampler : register(s0);
Texture2D myTex2D;



float4 PixelShaderFunction(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float4 color = tex2D(TextureSampler, texCoord);
	if (color.a)
		color.rgb = 1 - color.rgb;
	return color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}

//float4 PixelShaderFunction(float2 inCoord : TEXCOORD0) : COLOR0
//{
//	float4 color = tex2D(s0, inCoord);
//
//	return color;
//}
//
//technique Technique1
//{
//	pass Pass1
//	{
//		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
//	}
//}
