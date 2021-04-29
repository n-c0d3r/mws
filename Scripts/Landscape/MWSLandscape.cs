using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MWS;



namespace MWS
{
    public class MWSLandscape : MonoBehaviour
    {
        public ImageDebuger debuger;

        [HideInInspector]
        public float chunksDistance;

        [HideInInspector]
        public Vector2Int size;

        [HideInInspector]
        public Vector2Int heightMapRes;

        [HideInInspector]
        public Vector2Int alphaMapRes;

        [HideInInspector]
        public Vector2Int baseMapRes;

        [HideInInspector]
        public float pixelError;

        [HideInInspector]
        public float baseMapDist;

        [HideInInspector]
        public Terrain rootTile;

        [HideInInspector]
        public Transform tilesParent;

        [HideInInspector]
        public Shader shader;

        [HideInInspector]
        public bool useChunksCulling;
        [HideInInspector]
        public bool useChunksCullingForSceneCam;

        public List<Terrain> tiles=new List<Terrain>();

        [Space(10)]

        public int maxLayerCount = 8;
        public List<MWSTileLayer> layers = new List<MWSTileLayer>();

        [HideInInspector]
        public List<TerrainLayer> terrainLayers = new List<TerrainLayer>();

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (useChunksCulling)
            {
                StartCoroutine(ChunksCulling());

            }
            else
            {
                StartCoroutine(DontCulling());
            }
        }

        public List<Terrain> GetNeighborList()
        {
            AddToTerrains(rootTile);


            return tiles;
        }

        public void AddToTerrains(Terrain terrain)
        {

            tiles.Add(terrain);

            terrain.materialTemplate = new Material(shader);

            terrain.transform.parent = tilesParent;
            
            if ((!tiles.Contains(terrain.topNeighbor)) && terrain.topNeighbor!=null)
            {
                AddToTerrains(terrain.topNeighbor);
            }
            if ((!tiles.Contains(terrain.rightNeighbor)) && terrain.rightNeighbor != null)
            {
                AddToTerrains(terrain.rightNeighbor);
            }
            if ((!tiles.Contains(terrain.bottomNeighbor)) && terrain.bottomNeighbor != null)
            {
                AddToTerrains(terrain.bottomNeighbor);
            }
            if ((!tiles.Contains(terrain.leftNeighbor)) && terrain.leftNeighbor != null)
            {
                AddToTerrains(terrain.leftNeighbor);
            }
        }

        
        public Terrain[] GetNeighbors()
        {
            return GetNeighborList().ToArray();
        }

        public IEnumerator RefreshTiles()
        {
            tiles.Clear();
            tiles = GetNeighborList();

            StartCoroutine(RefreshTilesLayers());
            StartCoroutine(RefreshTilesSettings());
            yield return null;
        }


        public IEnumerator RefreshTilesSettings()
        {
            for(int i = 0; i < tiles.Count; i++)
            {
                tiles[i].heightmapPixelError = pixelError;
                tiles[i].basemapDistance=baseMapDist;
                tiles[i].terrainData.heightmapResolution = heightMapRes.x;
                tiles[i].terrainData.baseMapResolution = baseMapRes.x;
                tiles[i].terrainData.alphamapResolution = alphaMapRes.x;
            }
            yield return null;
        }

        public IEnumerator RefreshTilesLayers()
        {

            //Clear
            terrainLayers.Clear();

            //Create Layers
            for(int i = 0; i < layers.Count; i++)
            {
                TerrainLayer terrainLayer = new TerrainLayer();
                System.Guid g = System.Guid.NewGuid();
                string GuidString = System.Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");
                UnityEditor.AssetDatabase.CreateAsset(terrainLayer,"Assets/MWS/TerrainLayers/terrainLayer" + GuidString + ".terrainlayer");
                terrainLayers.Add(terrainLayer);
            }


            //Apply Layers
            TerrainLayer[] terrainLayerArray= terrainLayers.ToArray();
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].terrainData.terrainLayers = terrainLayerArray;
                Debug.Log(tiles[i].terrainData.terrainLayers.Length);
            }
            yield return null;
        }

        public IEnumerator UpdateLayers()
        {
            for(int i = 0; i < layers.Count; i++)
            {
                StartCoroutine(UpdateLayer(i));
            }
            yield return null;
        }

        public IEnumerator UpdateLayer(int index)
        {
            int layerCount = layers.Count;
            if (layerCount <= maxLayerCount)
            {

                int i = index;

                for (int t = 0; t < tiles.Count; t++)
                {
                    tiles[t].materialTemplate.SetFloat("LayerStrength" + index.ToString(), 1);
                }


                //Set Texture Properties
                for (int j = 0; j < tiles.Count; j++)
                {
                    for(int t=0;t< layers[index].textureProperies.Length; t++)
                    {
                        tiles[j].materialTemplate.SetTexture(layers[index].textureProperies[t].reference+i.ToString(), layers[index].textureProperies[t].value);

                    }
                }

                //Set Color Properties
                for (int j = 0; j < tiles.Count; j++)
                {
                    for (int t = 0; t < layers[index].colorProperies.Length; t++)
                    {
                        tiles[j].materialTemplate.SetColor(layers[index].colorProperies[t].reference + i.ToString(), layers[index].colorProperies[t].value);

                    }
                }

                //Set Float Properties
                for (int j = 0; j < tiles.Count; j++)
                {
                    for (int t = 0; t < layers[index].floatProperies.Length; t++)
                    {
                        tiles[j].materialTemplate.SetFloat(layers[index].floatProperies[t].reference + i.ToString(), layers[index].floatProperies[t].value);

                    }
                }

                //Set Vector2 Properties
                for (int j = 0; j < tiles.Count; j++)
                {
                    for (int t = 0; t < layers[index].vector2Properies.Length; t++)
                    {
                        tiles[j].materialTemplate.SetVector(layers[index].vector2Properies[t].reference + i.ToString(), layers[index].vector2Properies[t].value);

                    }
                }

                //Set Vector3 Properties
                for (int j = 0; j < tiles.Count; j++)
                {
                    for (int t = 0; t < layers[index].vector3Properies.Length; t++)
                    {
                        tiles[j].materialTemplate.SetVector(layers[index].vector3Properies[t].reference + i.ToString(), layers[index].vector3Properies[t].value);

                    }
                }

                //Set Vector4 Properties
                for (int j = 0; j < tiles.Count; j++)
                {
                    for (int t = 0; t < layers[index].vector4Properies.Length; t++)
                    {
                        tiles[j].materialTemplate.SetVector(layers[i].vector4Properies[t].reference + i.ToString(), layers[i].vector4Properies[t].value);

                    }
                }


                int startHidden = layers.Count-1;
                for(int j= startHidden + 1; j < maxLayerCount; j++)
                {
                    for (int t = 0; t < tiles.Count; t++)
                    {
                        tiles[t].materialTemplate.SetFloat("LayerStrength"+j.ToString(),0);
                    }
                }


            }
            yield return null;
        }

        public void UpdateTileLayers()
        {

        }

        public void UpdateTileLayer(int index)
        {

        }

        public IEnumerator UpdateAlphaMaps()
        {
            
            int alphaMapCount = layers.Count/4;
            if (layers.Count % 4 != 0)
            {
                alphaMapCount++;
            }
            if (layers.Count < 4)
            {
                alphaMapCount = 1;
            }



            for (int i = 0; i < tiles.Count; i++)
            {
                for(int j = 0; j < alphaMapCount; j++)
                {

                    debuger.image = tiles[i].terrainData.alphamapTextures[j];
                    tiles[i].materialTemplate.SetTexture("AlphaMap"+(j).ToString(),tiles[i].terrainData.alphamapTextures[j]);

                }
            }


            yield return null;
        }

        public IEnumerator HideTiles()
        {
            for(int i = 0; i < tiles.Count; i++)
            {
                tiles[i].gameObject.hideFlags = HideFlags.HideInHierarchy;
            }
            yield return null;
        }
        public IEnumerator RevealTiles()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].gameObject.hideFlags = HideFlags.None;
            }
            yield return null;
        }

        public IEnumerator DontCulling()
        {
            for(int i = 0; i < tiles.Count; i++)
            {
                tiles[i].gameObject.SetActive(true);
            }
            yield return null;
        }

        public IEnumerator ChunksCulling()
        {
            if (useChunksCulling)
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    //Check
                    bool hide = true;
#if UNITY_EDITOR
                    if(useChunksCullingForSceneCam)
                        if (Vector3.Distance(UnityEditor.SceneView.currentDrawingSceneView.camera.transform.position, tiles[i].transform.position) <= chunksDistance)
                        {
                            hide = false;
                            goto afterCheck;
                        }
#endif
                    for (int j = 0; j < Camera.allCameras.Length; j++)
                    {
                        if (Vector3.Distance(Camera.allCameras[j].transform.position, tiles[i].transform.position + tiles[i].terrainData.size / 2) <= chunksDistance)
                        {
                            hide = false;
                            goto afterCheck;
                        }
                    }
                    {

                    }

                //afterCheck
                afterCheck: { }

                    if (hide)
                    {
                        tiles[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        tiles[i].gameObject.SetActive(true);
                    }


                }
            }
            
            yield return null;
        }

    }

}
