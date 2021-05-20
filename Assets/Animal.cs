using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdditonalMaths;

public class Animal : MonoBehaviour
{
    HexTerrain terrain;

    public int position;
    public HexDirection direction;

    private void Awake()
    {
        terrain = FindObjectOfType<HexTerrain>();
    }

    public void MoveForward()
    {
        Hex hex = terrain.hexArray.GetNeighbour(position, direction);
        if ((hex.terrainFlags & TerrainFlags.CAN_WALK_ON) != 0)
        {
            position = terrain.hexArray.HexDisplacement(direction);
            transform.position = hex.position + new Vector3(0f, transform.localScale.y, 0f);
        }
    }

    public void Turn(int amount)
    {
        direction = (HexDirection)((int)direction + amount).UnsignedModulo(6);
        transform.rotation = HexTerrain.HexDirectionToWorldRotation((int)direction);
    }
}
