using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {
    Vector3 targetPos;
    GameObject target;

    // Use this for initialization
    void Start () {
        target = GameObject.FindWithTag("Target");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
