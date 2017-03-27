using UnityEngine;
using System.Collections;

public class platformBroken : MonoBehaviour {

    public GameObject fragments;

	// Use this for initialization
	void Start () {
        StartCoroutine(destroyPlat());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            destroyPlat();
        }
    }

    IEnumerator destroyPlat()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
