using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rabbit))]
public class RabbitEditor : Editor
{
    Rabbit rabbit;

    private void OnEnable()
    {
        rabbit = (Rabbit)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<<"))
        {
            rabbit.direction--;
        }
        if (GUILayout.Button("Move Forward"))
        {
            rabbit.StartCoroutine(rabbit.MoveForward());
        }
        if (GUILayout.Button(">>>"))
        {
            rabbit.direction++;
        }
        GUILayout.EndHorizontal();

        if (rabbit.GetComponent<WaypointController>())
        {
            if (GUILayout.Button("Go to waypoint"))
            {
                rabbit.GetComponent<WaypointController>().GoToWaypoint();
            }
        }

        if (GUI.changed)
        {
            rabbit.UpdateTransform();
        }
    }
}
