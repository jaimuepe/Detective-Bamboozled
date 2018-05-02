using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleStudio95Examples {
[RequireComponent(typeof(Camera))]
public class SimpleFollowingCamera : MonoBehaviour {

	public Transform m_Target;
	public float m_Distance = 10;
	public float m_Speed = 1.0f;
	public Vector2 m_Center;
	public Bounds m_Bounds = DefaultBounds;

	static Bounds DefaultBounds { get { 
		var b = new Bounds();
		b.size = Vector3.one * 1000;
		return b;
	}}

	Camera cam;

	void Start () {
		cam = GetComponent<Camera>();
		transform.position = m_Target.position + Vector3.back * m_Distance;
	}
	
	void Update () {
		var pos = m_Target.position;
		// Keep in bounds
		pos = m_Bounds.ClosestPoint(pos);

		var sp = cam.WorldToViewportPoint(pos);
		Vector2 delta = new Vector2(
			sp.x - (m_Center.x * .5f + 0.5f),
			sp.y - (m_Center.y * .5f + 0.5f)
		);
		float d = Time.deltaTime * m_Speed;
		transform.Translate(delta.x * d, delta.y * d, 0);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(m_Bounds.center, m_Bounds.size);
		Gizmos.color = Color.white;
	}
}
}