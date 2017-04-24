using UnityEngine;

using System.Collections;

namespace UnityStandardAssets._2D
{
    public class EnemyMovement : MonoBehaviour
    {

        public int direction = 1;
        GameObject player;
        enum state { patrol, attack, death };
        public int stateInt;
        public int attackState;
        float force;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            force = GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().playerForce;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y < -6)
            {
                Destroy(this.gameObject);
            }
            if (player.transform.position.y >= transform.position.y - 1 && player.transform.position.y <= transform.position.y + 1)
            {
                stateInt = 1;
                if (direction == -1 && player.transform.position.x > transform.position.x)
                {
                    direction = direction * -1;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
                else if (direction == 1 && player.transform.position.x < transform.position.x)
                {
                    direction = direction * -1;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
                //if (Math.Abs(player.transform.position.x - transform.position.x) > .5)
                //{
                //    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .01f * Time.deltaTime);
                //}
                
                else
                {
                    //StartCoroutine(attackFunct());
                    attackFunct();
                    
                    stateInt = 1;
                }
            }
            else
            {
                stateInt = 2;
                if (direction > 0)
                {
                    transform.position = new Vector2(transform.position.x + .01f, transform.position.y);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x - .01f, transform.position.y);
                }
            }


        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (stateInt != 1)
            {
                if (other.gameObject.tag == "TurnPoint")
                {
                    direction = direction * -1;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
            }
            if (other.gameObject.tag == "Player")
            {
                stateInt = 3;
            }

        }

        //void OnTriggerStay2D(Collider2D other)
        //{
            
        //}

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                stateInt = 1;
            }
        }

        void attackFunct()
        {
            //yield return new WaitForSeconds(1f);
            attackState = Random.Range(1, 10);
            if (attackState == 2)
            {
                //Debug.Log("attacking");
                if (player.transform.position.x > transform.position.x)
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
            attackState = 1;
            //StopCoroutine(attackFunct());
        }
    }
}
