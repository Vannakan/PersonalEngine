
texture ScreenTexture;

sampler screen = sampler_state
{
	Texture = <ScreenTexture>;
};


float4 PixelShaderFunction(float2 inCoord : TEXCOORD0) : COLOR
{
	float4 color = tex2D(screen, inCoord);

	color.rgb = (color.r + color.g + color.b) / 3.0f;

	return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
    }
}