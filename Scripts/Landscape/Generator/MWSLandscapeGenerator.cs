using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MWSLandscapeGenerator : EditorWindow
{

    [MenuItem("MWS/Landscape/Generator")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MWSLandscapeGenerator));
    }

}
