using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MWS;

namespace MWS
{
    [CustomEditor(typeof(MWSPlants))]
    public class MWSPlantsInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }

}