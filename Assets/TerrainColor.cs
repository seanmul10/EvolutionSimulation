using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TerrainColor
{
    public float range;
    public float heightOffset;

    public Color minColor;
    public Color maxColor;

    public override string ToString()
    {
        return base.ToString() + " Range: " + range.ToString() + " HeightOffset: " + heightOffset.ToString() + " Min Color: " + minColor.ToString() + " Max Color: " + maxColor.ToString();
    }
}
