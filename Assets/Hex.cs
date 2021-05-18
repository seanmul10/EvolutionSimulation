using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hex
{
    public Vector3 position;
    public Color color;
    public bool[] ridgeFlags;
    public float[] neighbourHeights;

    int ridgeCount;

    public Hex(Vector3 position, Color color)
    {
        this.position = position;
        this.color = color;
        ridgeFlags = new bool[6];
        neighbourHeights = new float[6];
        ridgeCount = 0;
    }

    public int Ridges => ridgeCount;

    public static Hex Void => new Hex(Vector3.zero, Color.magenta);

    public void SetRidge(int index, bool value)
    {
        ridgeFlags[index] = value;
        ridgeCount += value ? 1 : -1;
    }
}
