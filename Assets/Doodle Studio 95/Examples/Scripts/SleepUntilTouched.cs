using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleStudio95Examples {
public class SleepUntilTouched : MonoBehaviour {
	public Vector3 force = Vector3.back;
	public bool wakeUpWithOtherObjects = false;

	public UnityEngine.Events.UnityEvent onTouched;
	
	Rigidbody rb;
	// bool collision = false;
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;
		rb.mass = 0.2f;
	}
	
	
	void FixedUpdate () {
		
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Player" || wakeUpWithOtherObjects) {
			WakeUp();
			if (onTouched != null) {
				onTouched.Invoke();
			}
		}
		// collision = true;
	}

	void OnCollisionExit(Collision c) {
		// collision = false;
	}

	public void WakeUp() {
		rb.isKinematic = false;
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		var bc = GetComponent<BoxCollider>();
		if (bc)
			rb.AddForceAtPosition(force, transform.TransformPoint(bc.center + Vector3.up * bc.size.y * .45f), ForceMode.Force);
		
	}
}
}