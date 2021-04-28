using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;

namespace MWS
{
    namespace ShaderProperties
    {
        [System.Serializable]
        public class Vector4ShaderProperty
        {

            public string reference;
            public Vector4 value;

            public Vector4ShaderProperty(string reference, Vector4 value)
            {
                this.reference = reference;
                this.value = value;
            }
        }
    }

}


