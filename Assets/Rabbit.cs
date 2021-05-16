using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdditonalMaths;

public class Rabbit : MonoBehaviour
{
    public int xPosition;
    public int yPosition;

    public int direction = 0; // 0 = west, 1 = northwest, 2 = northeast, 3 = east, 4 = southeast, 5 = southwest

    public void UpdateTransform()
    {
        transform.position = HexTerrain.HexPositionToWorldPosition(new Vector3(xPosition, transform.position.y, yPosition));
        transform.rotation = HexTerrain.HexDirectionToWorldRotation(direction);
        direction = direction.UnsignedModulo(6);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(MoveForward());
        if (Input.GetKeyDown(KeyCode.A))
            direction--;
        if (Input.GetKeyDown(KeyCode.D))
            direction++;
        UpdateTransform();
    }

    public IEnumerator MoveForward()
    {
        switch (direction)
        {
            case 0:
                xPosition--;
                yield break;
            case 1:
                xPosition -= yPosition % 2 == 0 ? 0 : 1;
                yPosition++;
                yield break;
            case 2:
                xPosition += yPosition % 2 == 0 ? 1 : 0;
                yPosition++;
                yield break;
            case 3:
                xPosition++;
                yield break;
            case 4:
                xPosition += yPosition % 2 == 0 ? 1 : 0;
                yPosition--;
                yield break;
            case 5:
                xPosition -= yPosition % 2 == 0 ? 0 : 1;
                yPosition--;
                yield break;
        }
    }
}
