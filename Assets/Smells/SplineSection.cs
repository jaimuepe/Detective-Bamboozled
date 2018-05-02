using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SplineSection
{
    public Transform startPoint, startTangent, endPoint, endTangent;

    public SplineSection prev, next;

    public Vector3 GetPositionAt(float t)
    {
        if ((t < 0f) && (prev != null) && prev.IsValid())
        {
            return prev.GetPositionAt(t + 1.0f);
        }
        else if ((t > 1f) && (next != null) && next.IsValid())
        {
            return next.GetPositionAt(t - 1.0f);
        }
        else if (IsValid())
        {
            float t2 = t * t;
            float t3 = t2 * t;
            Vector3 p0 = startPoint.position, p1 = endPoint.position, m0 = startTangent.position - p0, m1 = endTangent.position - p1;
            return (p0 * ((2.0f * t3) - (3.0f * t2) + 1.0f))
                + (m0 * (t3 + (-2.0f * t2) + t))
                + (p1 * ((-2.0f * t3) + (3.0f * t2)))
                + (m1 * (t3 - t2));
        }
        return Vector3.zero;
    }

    public bool IsValid()
    {
        return (startPoint != null) && (startTangent != null) && (endPoint != null) && (endTangent != null);
    }

    void OnDrawGizmos()
    {
        if (IsValid())
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(startPoint.position, startTangent.position);
            Gizmos.DrawLine(endPoint.position, endTangent.position);
            for (int i = 0; i < 8; i++)
            {
                Gizmos.DrawLine(GetPositionAt(((float)i) / 8f),
                                GetPositionAt(((float)(i + 1)) / 8f));
            }
        }
    }
}
