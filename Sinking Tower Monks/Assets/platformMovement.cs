using UnityEngine;
using System.Collections;

public class platformMovement : MonoBehaviour {

    public GameObject player;
    public bool playerOn;

	// Use this for initialization
	void Start () {
        playerOn = false;
	    player = GameObject.Find("CharacterRobotBoy");
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerOn = false;
        }
    }
}
