using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class Thunderbolt : MonoBehaviour
    {

        GameObject boss;
        GameObject player;
        int counter;
        float force;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (counter > 39)
            {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            counter++;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            force = (GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().playerForce * 6);
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                boss = GameObject.FindWithTag("Boss1");
                player = other.gameObject;
                if (player.transform.position.x > boss.transform.position.x)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
                    //player.GetComponent<Rigidbody2D>().AddForce(transform.up * -1000);
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().AddForce(transform.right * (-1 * force));
                    //player.GetComponent<Rigidbody2D>().AddForce(transform.up * -1000);
                }
            }
        }
    }
}
