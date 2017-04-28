using UnityEngine;
using System.Collections;

public class progressBar : MonoBehaviour {
    public int count = 0;
    public int meas = 80;
    GameObject startPlat;
	// Use this for initialization
	void Start () {

        startPlat = GameObject.FindWithTag("StartPlatform");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void FixedUpdate()
    {
        if (startPlat == null && transform.position.y < 1.54)
        {
            count++;
            if (count % meas == 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);
            }
        }
        
    }

    
}
