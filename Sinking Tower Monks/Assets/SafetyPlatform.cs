using UnityEngine;
using System.Collections;

public class SafetyPlatform : MonoBehaviour {

    public GameObject player;
    public bool playerOn;

    // Use this for initialization
    void Start () {
        playerOn = false;
        player = GameObject.Find("CharacterRobotBoy");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerOn = true;
            StartCoroutine(timer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerOn = false;
            gameObject.SetActive(false);
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        StopCoroutine(timer());
    }
}
