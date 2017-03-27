using UnityEngine;
using System.Collections;

public class platformSpawn : MonoBehaviour {

    public GameObject platform;
    public GameObject platformBroken;
    public GameObject boss1;
    public GameObject player;
    int randomSpawn;
    int offset = 1;
    bool placeholder = false;
    bool boss1Spawn = false;
    int k = 1;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("CharacterRobotBoy");
        StartCoroutine(spawnRate());
        spawnRate();
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator spawnRate ()
    {
        for (int i = 0; i < 12; i++)
        {
            int brokenPlat = 1/*Random.Range(-2, 4)*/;
            int randomSpawn = Random.Range(-2, 2) + offset;
            int doublePlatform = Random.Range(-1, 2);
            if (brokenPlat >= 0)
            {
                Instantiate(platform, new Vector3(randomSpawn, 8.8f, 0), Quaternion.identity);
                if (doublePlatform > 0 && randomSpawn != 1 && randomSpawn != -1 && randomSpawn != 0)
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
            if (k > 0)
            {
                Instantiate(platform, new Vector3(0, 8.8f, 0), Quaternion.identity);
                if (boss1Spawn == false)
                {
                    Instantiate(boss1, new Vector3(0, 11f, 0), Quaternion.identity);
                    boss1Spawn = true;
                }
            }
            else
            {
                Instantiate(platform, new Vector3(-3, 8.8f, 0), Quaternion.identity);
                Instantiate(platform, new Vector3(3, 8.8f, 0), Quaternion.identity);
            }
            k = k * -1;
            yield return new WaitForSeconds(3.5f);
        }
    }
}
