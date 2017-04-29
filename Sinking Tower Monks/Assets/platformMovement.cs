using UnityEngine;
using System.Collections;

public class platformMovement : MonoBehaviour {
    public GameObject killzone;
    public GameObject player;
    public bool playerOn;
    public GameObject startPlat;
    float scene;

	// Use this for initialization
	void Start () {
        playerOn = false;
	    player = GameObject.Find("CharacterRobotBoy");
        killzone = GameObject.Find("Killzone");
        startPlat = GameObject.Find("StartPlatform");
        if (Application.loadedLevelName == "Game")
        {
            scene = 1;
        }
        else if (Application.loadedLevelName == "Level2")
        {
            scene = 1.5f;
        }
        else if (Application.loadedLevelName == "Level3")
        {
            scene = 2f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (startPlat == null && player != null && killzone!= null)
        {
            transform.Translate(Vector3.down * (Time.deltaTime * scene));
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
