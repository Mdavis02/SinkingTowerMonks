using UnityEngine;
using System.Collections;

public class platformSpawn : MonoBehaviour {

    public GameObject platform;
    public GameObject killzone;
    public GameObject platformBroken;
    public GameObject defPickup;
    public GameObject boss1;
    public GameObject player;
    public GameObject startPlat;
    public GameObject platPickup;
    public GameObject enemy;
    GameObject childEnemy;
    GameObject childPickup;
    GameObject parentPickup;
    GameObject currentPickup;
    int enemySpawn;
    int pickupSpawn;
    int pickupSpawn2;
    int randomDist;
    int randomSpawn;
    int offset = 1;
    bool placeholder = false;
    bool boss1Spawn = false;
    bool started = false;
    int k = 0;
	// Use this for initialization
	void Start () {
        pickupSpawn = Random.Range(6, 7);
        randomDist = Random.Range(0,1);
        if (randomDist == 0)
        {
            pickupSpawn2 = pickupSpawn + 4;
        }
        else if (randomDist == 1)
        {
            pickupSpawn2 = pickupSpawn - 4;
        }
        player = GameObject.Find("CharacterRobotBoy");
        startPlat = GameObject.Find("StartPlatform");
        killzone = GameObject.Find("Killzone");
        spawnRate();
        Debug.Log("Pickup Spawn is: " + pickupSpawn);
        Debug.Log("Pickup Spawn 2 is: " + pickupSpawn2);
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null || killzone == null)
        {
            Destroy(this.gameObject);
        }
        if (startPlat == null && started == false)
        {
            StartCoroutine(spawnRate());
            started = true;
        }
    }

    IEnumerator spawnRate ()
    {
        for (int i = 0; i < 12; i++)
        {
            enemySpawn = Random.Range(0, 5);
            int brokenPlat = 1/*Random.Range(-2, 4)*/;
            int randomSpawn = Random.Range(-2, 2) + offset;
            int doublePlatform = Random.Range(-1, 2);
            if (brokenPlat >= 0)
            {
                parentPickup = Instantiate(platform, new Vector3(randomSpawn, 8.8f, 0), Quaternion.identity) as GameObject;
                if (enemySpawn == 3 || enemySpawn == 5 || enemySpawn == 1)
                {
                    Instantiate(enemy, new Vector3(randomSpawn, 10f, 0), Quaternion.identity);
                }
                if (i == pickupSpawn)
                {
                    childPickup = Instantiate(platPickup, new Vector3(randomSpawn, 10f, 0), Quaternion.identity) as GameObject;
                    childPickup.transform.parent = parentPickup.transform;
                }
                if (i == pickupSpawn2)
                {
                    childPickup = Instantiate(defPickup, new Vector3(randomSpawn, 10f, 0), Quaternion.identity) as GameObject;
                    childPickup.transform.parent = parentPickup.transform;
                }
                if (doublePlatform > 0 && (randomSpawn > 2 || randomSpawn < -2) && randomSpawn != 0)
                {
                    Instantiate(platform, new Vector3(randomSpawn * -1, 8.8f, 0), Quaternion.identity);
                }
                yield return new WaitForSeconds(3.5f);
                offset = offset * -1;
            }
            else
            {
                Instantiate(platformBroken, new Vector3(randomSpawn, 8.8f, 0), Quaternion.identity);
                if (doublePlatform > 0 && randomSpawn != 1 && randomSpawn != -1 && randomSpawn != 0)
                {
                    Instantiate(platformBroken, new Vector3(randomSpawn * -1, 8.8f, 0), Quaternion.identity);
                }
                yield return new WaitForSeconds(3.5f);
                offset = offset * -1;
            }
            //Debug.Log(i);
        }

        placeholder = true;
        

        while (placeholder == true)
        {
            if (k == 0 || k == 4)
            {
                Instantiate(platform, new Vector3(-3.75f, 8.8f, 0), Quaternion.identity);
                if (k == 4)
                {
                    k = 0;
                }
            }
            else if (k == 1 || k == 3)
            {
                Instantiate(platform, new Vector3(0, 8.8f, 0), Quaternion.identity);
                if (boss1Spawn == false)
                {
                    Instantiate(boss1, new Vector3(0, 11f, 0), Quaternion.identity);
                    boss1Spawn = true;
                }
            }
            else if (k == 2)
            {
                //Instantiate(platform, new Vector3(-3.75f, 8.8f, 0), Quaternion.identity);
                Instantiate(platform, new Vector3(3.75f, 8.8f, 0), Quaternion.identity);
            }
            k++;
            yield return new WaitForSeconds(2.5f);
        }
    }
}
