using UnityEngine;
using System.Collections;

public class platformMovement : MonoBehaviour {
    public GameObject killzone;
    public GameObject player;
    public bool playerOn;
    public GameObject startPlat;

	// Use this for initialization
	void Start () {
        playerOn = false;
	    player = GameObject.Find("CharacterRobotBoy");
        killzone = GameObject.Find("Killzone");
        startPlat = GameObject.Find("StartPlatform");
    }
	
	// Update is called once per frame
	void Update () {
        if (startPlat == null && player != null && killzone!= null)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
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
