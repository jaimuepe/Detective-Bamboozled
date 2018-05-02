using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleStudio95Examples {
[RequireComponent(typeof(BoxCollider))]
public class RespawnOnOppositeEnd : MonoBehaviour {

	public Transform target;

	BoxCollider boxCollider;
	bool checkPositionConstantly;

	void Start() {
		boxCollider = GetComponent<BoxCollider>();
		boxCollider.isTrigger = true;
		checkPositionConstantly = target.GetComponent<Rigidbody>() == null;
	}

	void Update() {
		if (checkPositionConstantly)
			OnTargetExitsArea();
	}

	void OnTriggerExit(Collider c) {
		if (c.transform == target) {
			OnTargetExitsArea();
		}
	}

	void OnTargetExitsArea() {
		var point = transform.InverseTransformPoint(target.position);
		point.Scale(boxCollider.size);
		if (Mathf.Abs(point.x) > .5f) point.x *= -1 * 0.99f;
		if (Mathf.Abs(point.y) > .5f) point.y *= -1 * 0.99f;
		if (Mathf.Abs(point.y) > .5f) point.z *= -1 * 0.99f;
		target.position = transform.TransformPoint(point);
	}
}
}