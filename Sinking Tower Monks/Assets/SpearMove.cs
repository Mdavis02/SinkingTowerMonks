using UnityEngine;
using System.Collections;

public class SpearMove : MonoBehaviour {

    //public GameObject player;
    Vector3 target;

    // Use this for initialization
    void Start () {
        //player = GameObject.Find("CharacterRobotBoy");
        //target = player.transform.position;
        //Quaternion rotation = Quaternion.LookRotation
        //     (target - transform.position, transform.TransformDirection(Vector3.up));
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime);
        transform.Translate(Vector3.down * (Time.deltaTime* 3));
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.transform.position.x > transform.position.x)
            {
                other.GetComponent<Rigidbody2D>().AddForce(transform.right * 3000);
                //player.GetComponent<Rigidbody2D>().AddForce(transform.up * -1000);
                Destroy(this.gameObject);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().AddForce(transform.right * -3000);
                //player.GetComponent<Rigidbody2D>().AddForce(transform.up * -1000);
                Destroy(this.gameObject);
            }
        }
    }
}
