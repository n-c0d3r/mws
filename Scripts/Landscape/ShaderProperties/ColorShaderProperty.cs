using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;

namespace MWS
{
    namespace ShaderProperties
    {
        [System.Serializable]
        public class ColorShaderProperty
        {

            public string reference;
            public Color value;

            public ColorShaderProperty(string reference,Color value)
            {
                this.reference = reference;
                this.value = value;
            }
        }
    }
    
}


