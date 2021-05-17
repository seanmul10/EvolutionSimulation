using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexTerrain))]
public class HexTerrainEditor : Editor
{
    HexTerrain terrain;

    private void OnEnable()
    {
        terrain = (HexTerrain)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Mesh") || GUI.changed)
        {
            terrain.CreateMesh();
        }
        if (terrain.GetMesh() != null)
        {
            EditorGUILayout.LabelField("Verts", terrain.GetMesh().vertices.Length.ToString());
            EditorGUILayout.LabelField("Triangles", (terrain.GetMesh().triangles.Length / 3).ToString());
        }
    }
}
