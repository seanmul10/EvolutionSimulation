using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TerrainColor
{
    public string name;
    public TerrainFlags flags;
    public float range;
    public float heightOffset;

    public Color minColor;
    public Color maxColor;

    public override string ToString()
    {
        return base.ToString() + "Name: " + name + " Range: " + range.ToString() + " HeightOffset: " + heightOffset.ToString() + " Min Color: " + minColor.ToString() + " Max Color: " + maxColor.ToString();
    }
}
