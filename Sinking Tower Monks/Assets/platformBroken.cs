using UnityEngine;
using System.Collections;

public class platformBroken : MonoBehaviour {

    public GameObject fragments;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(destroyPlat());
            destroyPlat();
        }
    }

    IEnumerator destroyPlat()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(fragments, new Vector3((transform.position.x + .4f), transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(fragments, new Vector3((transform.position.x - .4f), transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(fragments, new Vector3((transform.position.x + .8f), transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(fragments, new Vector3((transform.position.x - .8f), transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(fragments, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
