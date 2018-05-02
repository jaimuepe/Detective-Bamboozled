using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : MonoBehaviour
{

    LineRenderer lr;
    Transform myTransform;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        myTransform = transform;

        for (int i = 0; i < lr.positionCount - 1; i++)
        {
            Vector3 start = lr.GetPosition(i);
            Vector3 end = lr.GetPosition(i + 1);

            AddColliderToLine(i, start, end);
        }
    }

    private void AddColliderToLine(int index, Vector3 startPos, Vector3 endPos)
    {
        BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider>();
        col.isTrigger = true;
        col.transform.parent = myTransform; // Collider is added as child object of line
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint; // setting position of collider object
        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(startPos.z - endPos.z) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.z < endPos.z && startPos.x < endPos.x) || (endPos.z < startPos.z && endPos.x < startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, angle, 0);
    }
}
