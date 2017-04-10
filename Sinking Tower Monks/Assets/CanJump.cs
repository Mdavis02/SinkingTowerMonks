using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class CanJump : MonoBehaviour
    {

        public GameObject player;
        public GameObject startPlat;
        bool started = false;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //if (started == true)
            //{
            //    //Destroy(startPlat);
            //}
            //transform.position = player.transform.position;
            //GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().m_Grounded = false;
        }

        //void OnTriggerEnter2D(Collider2D other)
        //{
        //    Debug.Log("Is this working?");
        //    //Debug.Log("This is colliding: " + other.gameObject.transform.name);
        //    if (other.gameObject.tag == "Platform")
        //    {
        //        //Debug.Log("Almost there");
        //        GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().m_Grounded = true;

        //    }
        //}

        void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("This is colliding: " + other.gameObject.transform.name);
            if (other.gameObject.tag == "Platform" || other.gameObject.tag == "SafePlat")
            {
                //Debug.Log("Almost there");
                GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().m_Grounded = true;
               
            }
            if (other.gameObject.tag == "StartPlatform")
            {
                //Debug.Log("Almost there");
                GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().m_Grounded = true;
              
            }
            if (other.gameObject.tag == "IcePlat")
            { }
        }

        //void OnTriggerStay2D(Collider2D other)
        //{
        //    Debug.Log("This is colliding: " + other.gameObject.transform.name);
        //    if (other.gameObject.tag == "Platform" || other.gameObject.tag == "StartPlatform")
        //    {
        //        //Debug.Log("Almost there");
        //        GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().m_Grounded = true;
                
        //    }
        //}

        void OnTriggerExit2D(Collider2D other)
        {
            //Debug.Log("Please?");
            if (other.gameObject.tag == "Platform")
            {
                GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().m_Grounded = false;
                
            }
            if (other.gameObject.tag == "StartPlatform")
            {
                GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().m_Grounded = false;
                
                Destroy(startPlat);
            }
        }
    }
}
