void MWSColorHeightBlend_float(float height0,float height1,float ifNotUseColorHeightBlend,float ColorHeightBlend,out float SecondLayerStrength) {
	
	SecondLayerStrength = ifNotUseColorHeightBlend;

	if (height1 > height0) {
		SecondLayerStrength = 1;
	}
	else {
		SecondLayerStrength = 0;
	}
		
	SecondLayerStrength = lerp(ifNotUseColorHeightBlend, SecondLayerStrength, ColorHeightBlend);
	
}