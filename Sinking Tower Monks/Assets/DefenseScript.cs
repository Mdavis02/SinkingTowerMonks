using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class DefenseScript : MonoBehaviour
    {
        //public GameObject player;
        //public bool playerOn;

        // Use this for initialization
        void Start()
        {
            //playerOn = false;
            //player = GameObject.Find("CharacterRobotBoy");
            StartCoroutine(timer());
        }

        // Update is called once per frame
        void Update()
        {
            GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().playerForce = 25f;
        }

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        playerOn = true;
        //        StartCoroutine(timer());
        //    }
        //}

        //private void OnTriggerExit2D(Collider2D other)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        playerOn = false;
        //        gameObject.SetActive(false);
        //    }
        //}

        IEnumerator timer()
        {
            yield return new WaitForSeconds(15f);
            GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>().playerForce = 50f;
            gameObject.SetActive(false);
            StopCoroutine(timer());
        }
    }
}

