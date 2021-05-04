using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MWS;

namespace MWS
{

    public class MWSEditor : EditorWindow
    {
        [MenuItem("MWS/Create/New World")]
        public static void CreateNewWorld()
        {
            GameObject MWSGObj = new GameObject("MWS");
            MWS mws = MWSGObj.AddComponent<MWS>();

            string id = System.Guid.NewGuid().ToString();
            mws.id = id;

            AssetDatabase.CreateFolder("Assets/MWS/Datas","world-"+id.ToString());
            AssetDatabase.CreateFolder("Assets/MWS/Datas/world-" + id.ToString(),"layers");

            GameObject landscapeGObj = new GameObject("Landscape");
            MWSLandscape landscape = landscapeGObj.AddComponent<MWSLandscape>();


            mws.landscape = landscape;
            landscape.world = mws;

            GameObject tilesGObj = new GameObject("Tiles");
            tilesGObj.transform.parent = mws.transform;
            landscape.tilesParent = tilesGObj.transform;

            GameObject plantsGObj = new GameObject("Plants");
            MWSPlants plants = plantsGObj.AddComponent<MWSPlants>();

            mws.plants = plants;
            plants.world = mws;

            landscape.transform.parent = MWSGObj.transform;
            plants.transform.parent = MWSGObj.transform;

        }

        [MenuItem("MWS/Fix/Refresh World")]
        public static void RefreshWorld()
        {
            MWS mws = GameObject.FindObjectOfType<MWS>();
            mws.plants.world = mws;
        }
    }

}
