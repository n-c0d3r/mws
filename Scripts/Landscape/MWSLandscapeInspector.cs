using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MWS;

namespace MWS
{
    [CustomEditor(typeof(MWSLandscape))]
    public class MWSLandscapeInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            MWSLandscape landscape = (MWSLandscape)target;

            EditorGUILayout.Space(10);
            landscape.shader = (Shader)EditorGUILayout.ObjectField("Shader", landscape.shader, typeof(Shader));


            EditorGUILayout.Space(10);

            landscape.size = EditorGUILayout.Vector2IntField("Size",landscape.size);

            landscape.heightMapRes= EditorGUILayout.Vector2IntField("Height Map Res", landscape.heightMapRes);
            landscape.alphaMapRes = EditorGUILayout.Vector2IntField("Alpha Map Res", landscape.alphaMapRes);
            landscape.baseMapRes = EditorGUILayout.Vector2IntField("Base Map Res", landscape.baseMapRes);
            landscape.pixelError= EditorGUILayout.FloatField("Pixel Error", landscape.pixelError);
            landscape.baseMapDist= EditorGUILayout.FloatField("Base Map Dist", landscape.baseMapDist);

            EditorGUILayout.Space(10);

            landscape.tilesParent = (Transform)EditorGUILayout.ObjectField("Tiles Parent",landscape.tilesParent,typeof(Transform));

            landscape.rootTile = (Terrain)EditorGUILayout.ObjectField("Root Tile",landscape.rootTile,typeof(Terrain));

            if (GUILayout.Button("Refresh Tiles"))
            {
                landscape.StartCoroutine(landscape.RefreshTiles());
            }
            if (GUILayout.Button("Refresh Tiles Layers"))
            {
                landscape.StartCoroutine(landscape.RefreshTilesLayers());
            }
            if (GUILayout.Button("Refresh Tiles Settings"))
            {
                landscape.StartCoroutine(landscape.RefreshTilesSettings());
            }

            if(landscape.tiles[0]!=null)
            {
                if (landscape.tiles[0].gameObject.hideFlags == 0)
                {
                    if (GUILayout.Button("Hide Tiles In Inspector"))
                    {
                        landscape.StartCoroutine(landscape.HideTiles());
                    }
                }
                else
                {
                    if (GUILayout.Button("Reveal Tiles In Inspector"))
                    {
                        landscape.StartCoroutine(landscape.RevealTiles());
                    }
                }
            }

            DrawDefaultInspector();


            if (GUILayout.Button("Update Layers"))
            {
                landscape.StartCoroutine(landscape.UpdateLayers());
            }

            for(int i = 0; i < landscape.layers.Count; i++)
            {
                if (GUILayout.Button("Update Layer "+i.ToString()))
                {
                    landscape.StartCoroutine(landscape.UpdateLayer(i));
                }
            }


        }


        public void OnSceneGUI()
        {
            MWSLandscape landscape = (MWSLandscape)target;
            landscape.StartCoroutine(landscape.UpdateAlphaMaps());
        }

    }

}
