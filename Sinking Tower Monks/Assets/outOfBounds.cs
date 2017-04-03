using UnityEngine;
using System.Collections;

public class outOfBounds : MonoBehaviour {

    public GameObject deathPlayer;
    public GameObject deathScreen;
    public GameObject victoryScreen;
    GameObject boss;
    bool gameEnd = false;
    //public GameObject deathEnemy;
    //public GameObject[] platform = new GameObject[5];

    // Use this for initialization
    void Start () {
        boss = victoryScreen;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameEnd == false)
        {
            if (boss.transform.position.y < -7.7)
            {
                Destroy(boss.gameObject);
                victoryScreen.gameObject.SetActive(true);
                gameEnd = true;
            }
        }
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
            gameEnd = true;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            Vector3 playerpos = other.transform.position;
            Destroy(other);
            //Instantiate(deathEnemy, playerpos, Quaternion.identity);
        }
        else if (other.gameObject.tag == "Boss1")
        {
            boss = other.gameObject;
        }
    }
}
