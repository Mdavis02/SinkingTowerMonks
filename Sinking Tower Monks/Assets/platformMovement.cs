using UnityEngine;
using System.Collections;

public class platformMovement : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	    player = GameObject.Find("CharacterRobotBoy");
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
