using UnityEngine;
using System.Collections;

public class outOfBounds : MonoBehaviour {

    public GameObject deathPlayer;
    public GameObject deathScreen;
    //public GameObject deathEnemy;
    //public GameObject[] platform = new GameObject[5];

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Something entered");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {

            //Debug.Log("Something left");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            Vector3 playerpos = new Vector3(other.transform.position.x, -5, other.transform.position.z);
            Destroy(other.gameObject);
            Instantiate(deathPlayer, playerpos, Quaternion.identity);
            deathScreen.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Vector3 playerpos = other.transform.position;
            Destroy(other);
            //Instantiate(deathEnemy, playerpos, Quaternion.identity);
        }
    }
}
