using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public int direction = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (direction > 0)
        {
            transform.position = new Vector2(transform.position.x + .01f, transform.position.y);
        }
	    else
        {
            transform.position = new Vector2(transform.position.x - .01f, transform.position.y);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TurnPoint")
        {
            direction = direction * -1;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }
}
