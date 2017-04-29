using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour {
    Vector3 targetPos;
    GameObject target;

    // Use this for initialization
    void Start () {
        target = GameObject.FindWithTag("Target");
        targetPos = target.transform.position;
        StartCoroutine(timeOut());
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 7f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.transform.position.x > transform.position.x)
            {
                other.GetComponent<Rigidbody2D>().AddForce(transform.right * 1000);
                //player.GetComponent<Rigidbody2D>().AddForce(transform.up * -1000);
                Destroy(this.gameObject);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().AddForce(transform.right * -1000);
                //player.GetComponent<Rigidbody2D>().AddForce(transform.up * -1000);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator timeOut()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
