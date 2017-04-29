using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(timeOut());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator timeOut()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);
    }
}
