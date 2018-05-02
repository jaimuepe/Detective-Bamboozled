using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spline))]
public class SplineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Spline spline = target as Spline;

        if (GUILayout.Button("Add section"))
        {
            GameObject goSplineIdx = new GameObject("Section " + spline.sections.Count);

            GameObject goStart = new GameObject("Start");
            GameObject goEnd = new GameObject("End");
            GameObject goStartTangent = new GameObject("Start tangent");
            GameObject goEndTangent = new GameObject("End tangent");

            goStart.transform.SetParent(goSplineIdx.transform);
            goEnd.transform.SetParent(goSplineIdx.transform);
            goStartTangent.transform.SetParent(goSplineIdx.transform);
            goEndTangent.transform.SetParent(goSplineIdx.transform);

            goSplineIdx.transform.SetParent(spline.transform, false);

            SplineSection section = new SplineSection()
            {
                startPoint = goStart.transform,
                endPoint = goEnd.transform,
                startTangent = goStartTangent.transform,
                endTangent = goEndTangent.transform
            };

            spline.sections.Add(section);
        }
    }
}
