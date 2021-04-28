using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;

namespace MWS
{
    namespace ShaderProperties
    {
        [System.Serializable]
        public class Vector2ShaderProperty
        {

            public string reference;
            public Vector2 value;

            public Vector2ShaderProperty(string reference, Vector2 value)
            {
                this.reference = reference;
                this.value = value;
            }
        }
    }

}


