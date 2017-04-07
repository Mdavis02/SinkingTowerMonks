using UnityEngine;
using System.Collections;

public class PlatformPickup : MonoBehaviour {
    GameObject safetyPlatform;

	// Use this for initialization
	void Start () {
	    safetyPlatform = GameObject.Find("safePlat");
        safetyPlatform.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            safetyPlatform.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
