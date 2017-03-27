using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BossMovement : MonoBehaviour {

    //public GameObject boss;
    //public GameObject[] platforms = new GameObject[6];
    public GameObject player;
    public bool knockedDown = false;
    public bool inAir = false;
    public bool teleporting = false;
    public GameObject[] platforms;
    private Animator bossAnim;
    int i = 0;
    int k = 0;
    int attack;
    // Use this for initialization
    void Start () {
        bossAnim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        //Debug.Log("Boss position is: " + boss.transform.position);
        //GameObject.Find("Killzone")
        if (transform.position.y <= -2 && transform.position.y > -3/*knockedDown == false && inAir == false*/)
        {
            StartCoroutine(outOfBoundsTeleport());
            //platforms = GameObject.FindGameObjectsWithTag("Platform");
            Debug.Log("Boss about to die");

            bossAnim.SetBool("disappear", true);
            outOfBoundsTeleport();
            //bossAnim.SetBool("disappear", false);
            //if (platforms[k].transform.position.y > -2)
            //{
            //    transform.position = new Vector3(platforms[k].transform.position.x, platforms[k].transform.position.y + 2, platforms[k].transform.position.z);
            //}
            //else
            //{
            //    k++;
            //}

            //bossAnim.SetBool("disappear", false);
            //bossAnim.SetBool("appear", true);
            //outOfBoundsTeleport();
            //bossAnim.SetBool("appear", false);

        }
        else
        {

            bossAnim.SetBool("disappear", false);
        }
        //attackFunct();
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        //    if (i < 6)
        //    {
        //        if (other.gameObject.tag == "Platform")
        //        {
        //            //Debug.Log("Platform entered boss trigger");
        //            platforms[i] = other.gameObject;
        //            i++;
        //        }
        //    }
        //    else
        //    {
        //        i = 0;
        //    }
    }

    //IEnumerator attackFunct()
    //{


    //        //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_grounded = false;

    //    yield return new WaitForSeconds(2);
    //}

    IEnumerator outOfBoundsTeleport()
    {
        
        if (platforms[k].transform.position.y > -2 && platforms[k].transform.position.y < 1.5)
        {
            
            yield return new WaitForSeconds(.3f);
            transform.position = new Vector3(platforms[k].transform.position.x, platforms[k].transform.position.y + 2, platforms[k].transform.position.z);
            
            //outOfBoundsAppear();
        }
        else
        {
            k++;
        }

        
        //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_grounded = false;    
    }

    //IEnumerator outOfBoundsAppear()
    //{
        
    //    yield return new WaitForSeconds(.5f);
    //    bossAnim.SetBool("disappear", false);
    //    //bossAnim.SetBool("appear", true);
    //    yield return new WaitForSeconds(1f);
    //    bossAnim.SetBool("appear", false);
        
    //}
}
