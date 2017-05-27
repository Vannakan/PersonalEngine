
sampler TextureSampler : register(s0);
Texture2D myTex2D;



float4 PixelShaderFunction(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float4 color = tex2D(TextureSampler, texCoord);

	if (!any(color)) return color;

	float step = 1.0 / 7;

	if (texCoord.x < (step * 1)) color = float4(1, 0, 0, 1);
	else if (texCoord.x < (step * 2)) color = float4(1, .5, 0, 1);
	else if (texCoord.x < (step * 3)) color = float4(1, 1, 0, 1);
	else if (texCoord.x < (step * 4)) color = float4(0, 1, 0, 1);
	else if (texCoord.x < (step * 5)) color = float4(0, 0, 1, 1);
	else if (texCoord.x < (step * 6)) color = float4(.3, 0, .8, 1);
	else                            color = float4(1, .8, 1, 1);

	return color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}


