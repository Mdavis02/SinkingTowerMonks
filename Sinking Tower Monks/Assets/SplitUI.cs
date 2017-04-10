using UnityEngine;
using System.Collections;

public class SplitUI : MonoBehaviour {
    float speed;
	// Use this for initialization
	void Start () {
        speed = 25f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0, transform.position.z), speed * Time.deltaTime);
    }
}
