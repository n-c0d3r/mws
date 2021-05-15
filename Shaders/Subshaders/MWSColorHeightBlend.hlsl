void MWSColorHeightBlend_float(float height0,float height1,bool useColorHeightBlend,float ifNotUseColorHeightBlend,out float SecondLayerStrength) {
	
	SecondLayerStrength = ifNotUseColorHeightBlend;

	if (useColorHeightBlend) {
		if (height1 > height0) {
			SecondLayerStrength = 1;
		}
		else {
			SecondLayerStrength = 0;
		}
	}
	else {

	}
}