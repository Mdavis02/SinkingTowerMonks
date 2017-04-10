using UnityEngine;
using System.Collections;

public class DefensePickup : MonoBehaviour {

    public GameObject shield;
    GameObject Shieldobj;
    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("CharacterRobotBoy");
        Shieldobj = Instantiate(shield, player.transform.position, Quaternion.identity) as GameObject;
        Shieldobj.transform.parent = player.transform;
        Shieldobj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Shieldobj.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
