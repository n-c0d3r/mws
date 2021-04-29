using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;
using MWS.ShaderProperties;

namespace MWS
{
    [System.Serializable]
    public class MWSTileLayer
    {
        public ColorShaderProperty[] colorProperies;
        public FloatShaderProperty[] floatProperies;
        public Vector2ShaderProperty[] vector2Properies;
        public Vector3ShaderProperty[] vector3Properies;
        public Vector4ShaderProperty[] vector4Properies;
        public TextureShaderProperty[] textureProperies;
        
        public MWSTileLayer()
        {
            colorProperies=new ColorShaderProperty[0];
            floatProperies = new FloatShaderProperty[0];
            vector2Properies = new Vector2ShaderProperty[0];
            vector3Properies = new Vector3ShaderProperty[0];
            vector4Properies = new Vector4ShaderProperty[0];
            textureProperies = new TextureShaderProperty[0];
        }

    }

}

