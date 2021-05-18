using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexArray
{
    Hex[] hexArray;
    int width;
    int height;

    public const float HEX_RADIUS = 0.5773502691896257645092f;
    public const float HALF_HEX_RADIUS = 0.5773502691896257645092f / 2;

    public HexArray(int width, int height)
    {
        this.width = width;
        this.height = height;
        hexArray = new Hex[Count];
    }

    public Hex this[int index]
    {
        get { return hexArray[index]; }
        set { hexArray[index] = value; }
    }

    public Hex this[int i, int j]
    {
        get
        {
            if (i >= 0 && j >= 0 && i < width + (j % 2 == 0 ? 0 : 1) && j < height)
                return hexArray[i + j * width + (j / 2)];
            else
                return Hex.Void;
        }
        set
        {
            if (i >= 0 && j >= 0 && i < width + (j % 2 == 0 ? 0 : 1) && j < height)
                hexArray[i + j * width + (j / 2)] = value;
        }
    }

    public int Count => width * height + height / 2;

    public Hex GetNeighbour(int index, HexDirection direction)
    {
        int i = index + HexDisplacement(direction);
        if (i >= 0 && i < Count)
            return hexArray[i];
        return Hex.Void;
    }

    public Hex GetNeighbour(int xIndex, int yIndex, HexDirection direction)
    {
        int currentHex = GetHexIndex(this[xIndex, yIndex]);
        return GetNeighbour(currentHex, direction);
    }

    public static HexArray NoiseToHexTerrain(float[,] noiseMap, int width, int height, TerrainColorData terrainColorData)
    {
        int i = 0;
        HexArray hexArray = new HexArray(width, height);
        for (float y = 0; i < height; y += HEX_RADIUS + HALF_HEX_RADIUS, i++)
        {
            int j = 0;
            for (float x = i % 2 == 0 ? 0f : -0.5f; x < width; x++, j++)
            {
                float currentRange = 0f;
                float terrainValue = Noise.AccessNoiseMapWithFloats(noiseMap, x, y);
                bool isSet = false;
                foreach (TerrainColor color in terrainColorData.terrainColors)
                {
                    if (terrainValue <= Mathf.Min(currentRange + color.range, 1f))
                    {
                        hexArray[j, i] = new Hex(new Vector3(x, color.heightOffset, y), Color.Lerp(color.minColor, color.maxColor, (terrainValue - currentRange) * (1f / color.range)));
                        isSet = true;
                        break;
                    }
                    currentRange += color.range;
                }
                if (!isSet)
                {
                    Debug.LogWarning("Error with terrain data. Value: " + terrainValue + " Current range: " + currentRange);
                    hexArray[j, i] = new Hex(new Vector3(x, 0f, y), Color.magenta);
                }
            }
        }
        return hexArray;
    }

    public int GetHexIndex(Hex hex)
    {
        return System.Array.IndexOf(hexArray, hex);
    }

    public int HexDisplacement(HexDirection direction)
    {
        switch (direction)
        {
            case HexDirection.EAST:
                return -1;
            case HexDirection.NORTHEAST:
                return width;
            case HexDirection.NORTHWEST:
                return width + 1;
            case HexDirection.WEST:
                return 1;
            case HexDirection.SOUTHWEST:
                return -width;
            case HexDirection.SOUTHEAST:
                return -width - 1;
            default:
                return 0;
        }
    }
}
