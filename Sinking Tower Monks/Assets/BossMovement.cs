using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BossMovement : MonoBehaviour {

    //public GameObject boss;
    //public GameObject[] platforms = new GameObject[6];
    //GameObject victoryScreen;
    public GameObject lightning;
    public GameObject spear;
    GameObject player;
    public bool knockedDown = false;
    public bool inAir = false;
    public bool teleporting = false;
    public GameObject[] platforms;
    private Animator bossAnim;
    public bool inbetweenMove;
    public bool isAttacking = false;
    public bool runningAway = false;
    public bool isMoving;
    public bool downed = false;
    public int moveChoice;
    public bool vulnerable = false;
    bool teleportOK = false;
    bool lightningBolt = false;
    bool spearAttack = false;
    bool approached = false;
    int i = 0;
    int k = 0;
    int j = 0;
    int l = 0;
    public int tell = 1;
    public int moveTimer = 0;
    // Use this for initialization
    void Awake () {
        bossAnim = GetComponent<Animator>();
        
        inbetweenMove = true;
        
    }

    private void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        
    }

	// Update is called once per frame
	void Update () {
       
        if (player != null)
        {
            platforms = GameObject.FindGameObjectsWithTag("Platform");

            
            if (teleportOK == true && downed == false)
            {
                vulnerable = false;
                StartCoroutine(runningAwayTeleport());
                runningAwayTeleport();
            }
            if (lightningBolt == true && downed == false)
            {
                Instantiate(lightning, new Vector3(0, transform.position.y, transform.position.z), Quaternion.identity);
                lightningBolt = false;
            }
            if (spearAttack == true && downed == false)
            {
                Instantiate(spear, new Vector3(transform.position.x - .5f, transform.position.y + .5f, transform.position.z), Quaternion.identity);
                Instantiate(spear, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), Quaternion.identity);
                Instantiate(spear, new Vector3(transform.position.x + .5f, transform.position.y + .5f, transform.position.z), Quaternion.identity);
                spearAttack = false;
            }
            if (runningAway == true && downed == false)
            {
                isMoving = true;
                
                StartCoroutine(runningAwayTeleport());
                bossAnim.SetBool("disappear", true);
                runningAwayTeleport();
                
            }
            if (moveChoice == 1 && isMoving == false && downed == false && approached == true)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                
                vulnerable = true;
                StartCoroutine(lineOfSightTeleport());
                bossAnim.SetBool("disappear", true);
                transform.position = new Vector3((player.transform.position.x * -1), player.transform.position.y, player.transform.position.z);
                lineOfSightTeleport();


                moveChoice = Random.Range(0, 2);

            }
            //if (moveChoice == 2 && isMoving == false && downed == false)
            //{
            //    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //    //GetComponent<Rigidbody2D>().isKinematic = true;
            //    vulnerable = true;
            //    StartCoroutine(spearAttackTeleport());
            //    bossAnim.SetBool("disappear", true);
            //    transform.position = new Vector3(0, 3.8f, transform.position.z);
            //    spearAttackTeleport();
            //}
            if (transform.position.y <= -2 && GetComponent<Rigidbody2D>().velocity.y > -14/*&& transform.position.y > -3*//*knockedDown == false && inAir == false*/)
            {
                
                isMoving = true;
                StartCoroutine(outOfBoundsTeleport());
               

                bossAnim.SetBool("disappear", true);
                outOfBoundsTeleport();
                

            }
            else
            {

                bossAnim.SetBool("disappear", false);
            }
        }

        //Debug.Log("moveInbetween is: " + inbetweenMove);
        //attackFunct();
	}

    private void FixedUpdate()
    {
        if (player != null)
        {
            moveTimer++;
            if (moveTimer > 180 && isMoving == false && downed == false)
            {
                //inbetweenMove = false;
                //StartCoroutine(moveAttack());
                moveAttack();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && vulnerable == false && downed == false)
        {
            //Debug.Log("Player is in boss range");
            approached = true;
            runningAway = true;
        }
       
    }

    

    IEnumerator outOfBoundsTeleport()
    {
        
        if (platforms[k].transform.position.y > -1 && platforms[k].transform.position.y < 5 && platforms[j].GetComponent<platformMovement>().playerOn == false)
        {
            yield return new WaitForSeconds(.3f);
            transform.position = new Vector3(platforms[k].transform.position.x, platforms[k].transform.position.y + 2, platforms[k].transform.position.z);
            Debug.Log("Out of Bounds Target = : " + platforms[k].transform.position.x + " " + platforms[k].transform.position.y);
            isMoving = false;
            
            
            //k = 0;
            //outOfBoundsAppear();
            //StopCoroutine(outOfBoundsTeleport());
        }
        else
        {
            k++;
        }

        
        //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_grounded = false;    
    }

    IEnumerator runningAwayTeleport()
    {

        if (platforms[j].transform.position.y > -1 && platforms[j].GetComponent<platformMovement>().playerOn == false)
        {

            yield return new WaitForSeconds(.3f);
            transform.position = new Vector3(platforms[j].transform.position.x, platforms[j].transform.position.y + 2, platforms[j].transform.position.z);
            Debug.Log("Running Away Target = : " + platforms[k].transform.position.x + " " + platforms[k].transform.position.y);
            runningAway = false;
            isMoving = false;
            teleportOK = false;
            j = 0;
            StopCoroutine(runningAwayTeleport());
            //outOfBoundsAppear();
        }
        else
        {
            j++;
        }


        //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_grounded = false;    
    }

    IEnumerator lineOfSightTeleport()
    {
            yield return new WaitForSeconds(.3f);
            
        
        //runningAway = false;
        //isMoving = false;
        yield return new WaitForSeconds(2f);
        if (GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeAll)
        {
            lightningBolt = true;
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //GetComponent<Rigidbody2D>().isKinematic = false;
        teleportOK = true;
        StopCoroutine(lineOfSightTeleport());
            //outOfBoundsAppear();
        //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_grounded = false;    
    }

    IEnumerator spearAttackTeleport()
    {
        yield return new WaitForSeconds(.3f);


        //runningAway = false;
        //isMoving = false;
        yield return new WaitForSeconds(2f);
        //lightningBolt = true;
        spearAttack = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //GetComponent<Rigidbody2D>().isKinematic = false;
        teleportOK = true;
        StopCoroutine(spearAttackTeleport());
        //outOfBoundsAppear();
        //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_grounded = false;    
    }

    void moveAttack()
    {
        moveTimer = 0;
        //yield return new WaitForSeconds(2);
        //inbetweenMove = false;
        Debug.Log("Move number is: " + moveChoice);
        moveChoice = Random.Range(0, 2);
       
        tell = tell * -1;
        //yield return new WaitForSeconds(2);
        //inbetweenMove = true;
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
