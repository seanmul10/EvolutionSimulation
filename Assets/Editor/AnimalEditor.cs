using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Animal))]
public class AnimalEditor : Editor
{
    Animal animal;

    private void OnEnable()
    {
        animal = (Animal)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<<"))
        {
            animal.Turn(-1);
        }
        if (GUILayout.Button("Move Forward"))
        {
            animal.MoveForward();
        }
        if (GUILayout.Button(">>>"))
        {
            animal.Turn(1);
        }
        GUILayout.EndHorizontal();

        if (animal.GetComponent<WaypointController>())
        {
            if (GUILayout.Button("Go to waypoint"))
            {
                animal.GetComponent<WaypointController>().GoToWaypoint();
            }
        }
    }
}
