using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;

namespace MWS
{
    namespace ShaderProperties
    {
        [System.Serializable]
        public class FloatShaderProperty
        {

            public string reference;
            public float value;

            public FloatShaderProperty(string reference, float value)
            {
                this.reference = reference;
                this.value = value;
            }
        }
    }

}


