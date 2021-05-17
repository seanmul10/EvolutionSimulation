using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hex
{
    public Vector3 position;
    public Color color;

    public Hex(Vector3 position, Color color)
    {
        this.position = position;
        this.color = color;
    }

    public static Hex Void => new Hex(Vector3.zero, Color.magenta);
}
