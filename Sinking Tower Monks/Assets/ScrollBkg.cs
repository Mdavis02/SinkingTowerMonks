using UnityEngine;
using System.Collections;


public class ScrollBkg : MonoBehaviour
{
    public Material firstPhase;
    public Material secondPhase;
    public Material thirdPhase;
    public GameObject player;
    public float speed = .5f;
    Vector3 moveDown;
    Vector2 offset;
    bool moveOk = false;
    bool secMove = false;
    public bool scroll = true;
    float it = .02f;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(progressTrack());
        moveDown = new Vector3(0, transform.position.y - 11.77f, transform.position.z);
        //Debug.Log("Current position of quads is: " + transform.position.y); 
        player = GameObject.Find("CharacterRobotBoy");
    }

    // Update is called once per frame
    void Update()
    {

        //else
        //{
        //    scroll = false;
        //}
        if (player != null)
        {
            progressTrack();
            if (scroll == true)
            {
                offset = new Vector2(0, it * speed);
                GetComponent<Renderer>().material.mainTextureOffset = offset;
                it = it + .02f;
            }

            if (moveOk == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveDown, 1 * Time.deltaTime);
            }
            //Debug.Log("offset = " + offset);
            if (transform.position.y == moveDown.y)
            {
                scroll = true;
                offset[1] = offset[1] + .1f;
            }
        }
    }

    void progressTrack()
    {
        if (offset[1] > 2 && offset[1] < 2.1)
        {
            //Debug.Log("MovingDown");
            moveOk = true;
            scroll = false;
        }
        else
        {
            scroll = true;
        }
        if (offset[1] > 4 && offset[1] < 4.1)
        {
            if (secMove == false)
            {
                moveDown = new Vector3(0, transform.position.y - 11.77f, 0);
                secMove = true;
            }
            //Debug.Log("MovingDown");
            moveOk = true;
            scroll = false;
        }
        //yield return new WaitForSeconds(12.5f);

        //if (offset[1] > 4 && offset[1] <= 6)
        //{   scroll = false;
        //    moveDown = new Vector3(0, transform.position.y - 11.77f, 0);

        //}
        //else
        //{
        //    scroll = true;
        //}

        //GetComponent<Renderer>().material = thirdPhase;
    }
}