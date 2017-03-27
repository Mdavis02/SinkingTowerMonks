using UnityEngine;
using System.Collections;

public class Floating2D : MonoBehaviour {
	public float verticalBounce = .1f;
	public float bounceTime = 1;
	public float spinPerSecond = 0;
	private float sy, rfactor, sfactor;
	private Vector3 srot;
	void Start() {
		rfactor = Random.value * bounceTime; sfactor = 2*Mathf.PI/bounceTime;
		sy = transform.localPosition.y;
		srot = transform.localRotation.eulerAngles;
	}
	void Update() {
		if(verticalBounce > 0) { //for bouncing powerups
			Vector3 pos = transform.localPosition; pos.y = sy + verticalBounce * Mathf.Sin(sfactor * (rfactor + Time.unscaledTime));
			transform.localPosition = pos;
		}
		if(spinPerSecond != 0) { //for spinning powerups
			Quaternion rot = transform.localRotation;
			rot.eulerAngles = new Vector3(srot.x,srot.y,srot.z + spinPerSecond * Time.unscaledTime);
			transform.localRotation = rot;
		}
	}
}
