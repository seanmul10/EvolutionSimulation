using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rabbit))]
public class WaypointController : MonoBehaviour
{
    public int x;
    public int y;

    Rabbit rabbit;

    private void OnEnable()
    {
        rabbit = GetComponent<Rabbit>();
    }

    public void GoToWaypoint()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 centre = HexTerrain.HexPositionToWorldPosition(new Vector3(x, transform.position.y, y));
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(centre, 0.25f);
        Gizmos.DrawLine(centre, transform.position);
    }
}
