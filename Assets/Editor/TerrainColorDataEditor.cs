using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainColorData))]
public class TerrainColorDataEditor : Editor
{
    TerrainColorData terrainColorData;

    private void OnEnable()
    {
        terrainColorData = (TerrainColorData)target;
    }

    public override void OnInspectorGUI()
    {
        TerrainColor[] colors = terrainColorData.terrainColors;
        float totalRange = 0f;
        for (int i = 0; i < colors.Length; i++)
        {
            GUILayout.BeginHorizontal();
            //colors[i].name = EditorGUILayout.TextField(new GUIContent("Name"), colors[i].name);
            colors[i].flags = (TerrainFlags)EditorGUILayout.EnumFlagsField(new GUIContent("Flags"), colors[i].flags);
            GUILayout.EndHorizontal();
            colors[i].range = EditorGUILayout.FloatField(new GUIContent("Range"), colors[i].range);
            colors[i].heightOffset = EditorGUILayout.FloatField(new GUIContent("Height Offset"), colors[i].heightOffset);
            GUILayout.BeginHorizontal();
                colors[i].minColor = EditorGUILayout.ColorField(new GUIContent("Min Color"), colors[i].minColor, true, true, false);
                GUI.enabled = false;
                    EditorGUILayout.FloatField(totalRange, GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth * 0.2f));
                GUI.enabled = true;
                GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
                colors[i].maxColor = EditorGUILayout.ColorField(new GUIContent("Max Color"), colors[i].maxColor, true, true, false);
                GUI.enabled = false;
                    EditorGUILayout.FloatField(totalRange + colors[i].range, GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth * 0.2f));
                GUI.enabled = true;
            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
            totalRange += colors[i].range;
        }
        if (totalRange != 1f)
            EditorGUILayout.HelpBox("The total range of colors in the terrain color data does not add up to 1 (" + totalRange + "). Make sure the range value of each color adds up to 1.", MessageType.Error);
        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
