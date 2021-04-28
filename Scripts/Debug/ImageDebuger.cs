using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ImageDebuger : MonoBehaviour
{
    public Texture image;
    public float R;
    public float G;
    public float B;
    public float A;
    public MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[CustomEditor(typeof(ImageDebuger))]
public class ImageDebugEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Update"))
        {
            Material mat = new Material(Shader.Find("HDRP/Unlit"));
            mat.SetTexture("Image", ((ImageDebuger)target).image);
            mat.SetFloat("R", ((ImageDebuger)target).R);
            mat.SetFloat("G", ((ImageDebuger)target).G);
            mat.SetFloat("B", ((ImageDebuger)target).B);
            mat.SetFloat("A", ((ImageDebuger)target).A);
            ((ImageDebuger)target).meshRenderer.sharedMaterial = mat;
        }
    }
}
