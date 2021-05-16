using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TerrainLevel
{
    public float height;
    public Color color;

    public TerrainLevel(float height, Color color)
    {
        this.height = height;
        this.color = color;
    }
}
