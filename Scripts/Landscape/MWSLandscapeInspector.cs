using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MWS;


#if UNITY_EDITOR
namespace MWS
{
    [CustomEditor(typeof(MWSLandscape))]
    public class MWSLandscapeInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            MWSLandscape landscape = (MWSLandscape)target;

            if (landscape.shader == null)
            {
                landscape.shader = Shader.Find("MWS/MWSLandscape");
            }

            if (landscape.setup == false)
            {
                
                landscape.size = EditorGUILayout.Vector3IntField("Size", landscape.size);
                landscape.heightMapRes = EditorGUILayout.Vector2IntField("Height Map Res", landscape.heightMapRes);
                landscape.alphaMapRes = EditorGUILayout.Vector2IntField("Alpha Map Res", landscape.alphaMapRes);
                landscape.baseMapRes = EditorGUILayout.Vector2IntField("Base Map Res", landscape.baseMapRes);
                landscape.pixelError = EditorGUILayout.FloatField("Pixel Error", landscape.pixelError);
                landscape.baseMapDist = EditorGUILayout.FloatField("Base Map Dist", landscape.baseMapDist);
                /*
                landscape.size = new Vector3Int(2000,2000,2000);
                landscape.heightMapRes = new Vector2Int(1025,1025);
                landscape.alphaMapRes = new Vector2Int(1024,1024);
                landscape.baseMapRes = new Vector2Int(1024, 1024);
                landscape.pixelError = 20;
                landscape.baseMapDist = 1000;*/
                if (GUILayout.Button("Setup"))
                {
                    landscape.StartCoroutine(landscape.Setup());
                }
            }
            else
            {
                

                if (landscape.chunksDistance == 0)
                {
                    landscape.chunksDistance = 2000;
                }

                EditorGUILayout.Space(10);
                landscape.shader = (Shader)EditorGUILayout.ObjectField("Shader", landscape.shader, typeof(Shader));


                EditorGUILayout.Space(10);

                landscape.size = EditorGUILayout.Vector3IntField("Size", landscape.size);
                landscape.chunksDistance = EditorGUILayout.FloatField("Chunks Distance", landscape.chunksDistance);
                landscape.useChunksCulling = EditorGUILayout.Toggle("Chunks Culling", landscape.useChunksCulling);
                landscape.useChunksCullingForSceneCam = EditorGUILayout.Toggle("[Editor Only] Chunks Culling For Scene Cam", landscape.useChunksCullingForSceneCam);

                landscape.heightMapRes = EditorGUILayout.Vector2IntField("Height Map Res", landscape.heightMapRes);
                landscape.alphaMapRes = EditorGUILayout.Vector2IntField("Alpha Map Res", landscape.alphaMapRes);
                landscape.baseMapRes = EditorGUILayout.Vector2IntField("Base Map Res", landscape.baseMapRes);
                landscape.pixelError = EditorGUILayout.FloatField("Pixel Error", landscape.pixelError);
                landscape.baseMapDist = EditorGUILayout.FloatField("Base Map Dist", landscape.baseMapDist);

                EditorGUILayout.Space(10);

                landscape.tilesParent = (Transform)EditorGUILayout.ObjectField("Tiles Parent", landscape.tilesParent, typeof(Transform));

                landscape.rootTile = (Terrain)EditorGUILayout.ObjectField("Root Tile", landscape.rootTile, typeof(Terrain));

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

                if (landscape.tiles[0] != null)
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

                for (int i = 0; i < landscape.layers.Count; i++)
                {
                    if (GUILayout.Button("Update Layer " + i.ToString()))
                    {
                        landscape.StartCoroutine(landscape.UpdateLayer(i));
                    }
                }
            }

            

            
            

        }


        public void OnSceneGUI()
        {
            MWSLandscape landscape = (MWSLandscape)target;
            landscape.StartCoroutine(landscape.UpdateAlphaMaps());
            if (!Application.isPlaying)
            {
                if(landscape.useChunksCulling)
                    landscape.StartCoroutine(landscape.ChunksCulling());
                else
                {
                    landscape.StartCoroutine(landscape.DontCulling());
                }
            }
        }

    }

}
#endif