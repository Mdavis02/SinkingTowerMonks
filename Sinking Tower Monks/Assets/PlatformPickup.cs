using UnityEngine;
using System.Collections;

public class PlatformPickup : MonoBehaviour {
    public GameObject safetyPlatform;
    GameObject safePlat;
	// Use this for initialization
	void Start () {
	    safePlat = Instantiate(safetyPlatform) as GameObject;
        safePlat.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            safePlat.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
