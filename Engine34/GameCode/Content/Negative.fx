
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


