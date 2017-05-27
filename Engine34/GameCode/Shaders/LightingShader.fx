


texture myTex2D;
sampler TextureSampler : register(s0);
sampler lightSampler = sampler_state { Texture = <myTex2D>; };



float4 PixelShaderFunction(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float4 color = tex2D(TextureSampler, texCoord);
	float4 lightColor = tex2D(lightSampler, texCoord);


	
	return color * lightColor;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}


