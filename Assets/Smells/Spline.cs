using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour {

    public List<SplineSection> sections = new List<SplineSection>();

    private void Start()
    {
        for (int i = 0; i < sections.Count - 1; i ++)
        {
            sections[i].next = sections[i + 1];

            if (i != 0)
            {
                sections[i].prev = sections[i - 1];
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < sections.Count; i ++)
        {
            SplineSection splineSection = sections[i];
            if (splineSection.IsValid())
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(splineSection.startPoint.position, splineSection.startTangent.position);
                Gizmos.DrawLine(splineSection.endPoint.position, splineSection.endTangent.position);
                Gizmos.color = Color.white;
                for (int j = 0; j < 16; j++)
                {
                    Gizmos.DrawLine(splineSection.GetPositionAt(i / 16f), splineSection.GetPositionAt(((float)i + 1) / 16f));
                }
            }
        }
    }
}
