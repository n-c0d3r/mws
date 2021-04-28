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
    }

}

