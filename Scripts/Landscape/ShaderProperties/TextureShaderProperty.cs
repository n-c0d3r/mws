using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;

namespace MWS
{
    namespace ShaderProperties
    {
        [System.Serializable]
        public class TextureShaderProperty
        {

            public string reference;
            public Texture value;

            public TextureShaderProperty(string reference, Texture value)
            {
                this.reference = reference;
                this.value = value;
            }
        }
    }

}


