using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;

namespace MWS
{
    namespace ShaderProperties
    {
        [System.Serializable]
        public class Vector3ShaderProperty
        {

            public string reference;
            public Vector3 value;

            public Vector3ShaderProperty(string reference, Vector3 value)
            {
                this.reference = reference;
                this.value = value;
            }
        }
    }

}


