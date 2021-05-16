using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "TerrainColorData", order = 1)]
public class TerrainColorData : ScriptableObject
{
    public TerrainColor[] terrainColors;

    public static TerrainColorData EmptyDataObject
    {
        get
        {
            TerrainColorData tcd = CreateInstance<TerrainColorData>();
            tcd.name = "Empty Terrain Color Data";
            tcd.terrainColors = new TerrainColor[0];
            return tcd;
        }
    }
}
