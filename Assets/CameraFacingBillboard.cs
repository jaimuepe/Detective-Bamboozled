using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    Transform mainCamera;
    Transform containerTransform;

    private void Awake()
    {
        GameObject container = new GameObject();
        container.name = "GRP_" + transform.gameObject.name;
        container.transform.position = transform.position;
        Transform parentTransform = transform.parent;

        container.transform.parent = parentTransform;
        transform.parent = container.transform;

        containerTransform = container.transform;
    }

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        containerTransform.LookAt(containerTransform.position + mainCamera.rotation * Vector3.forward,
            mainCamera.rotation * Vector3.up);
    }
}
