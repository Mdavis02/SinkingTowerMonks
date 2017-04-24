using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        public GameObject tempPlat;
        public GameObject enemy;
        public GameObject chance;
        GameObject animChild;
        Vector2 dir;
        public bool inRange = false;
        bool started = false;
        private float force;
        private float initForce;
        public int comboTime = 0;
        public float playerForce;
        public bool comboChance0 = true;
        public bool comboChance1 = false;
        public bool comboChance2 = false;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Animator c_Anim;
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private void Awake()
        {
            // Setting up references.
            animChild = GameObject.Find("PlayerAirProjectile_0");
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            chance = GameObject.FindWithTag("Chance");
            chance.SetActive(false);
            m_Anim = GetComponent<Animator>();
            c_Anim = animChild.GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            playerForce = 50f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && inRange == true)
            {
                c_Anim.SetBool("isAttacking", true);
                StartCoroutine(attackAnimWait());
                if (enemy.gameObject.tag == "Enemy")
                {
                    dir = (enemy.transform.position - transform.position).normalized;
                    if (comboChance0 == true)
                    {
                        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        c_Anim.SetInteger("attackNum", 0);
                        force = initForce;
                        enemy.GetComponent<Rigidbody2D>().AddForce(dir * force);
                        comboTime = 0;
                        comboChance1 = true;
                        comboChance0 = false;
                        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    }
                    else if (comboChance1 == true && comboTime > 30 && comboTime < 60)
                    {
                        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        c_Anim.SetInteger("attackNum", 1);
                        force = force * 2;
                        enemy.GetComponent<Rigidbody2D>().AddForce(dir * force);
                        comboChance1 = false;
                        comboChance2 = true;
                        comboTime = 0;
                        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    }
                    else if (comboChance2 == true && comboTime > 30 && comboTime < 60)
                    {
                        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        c_Anim.SetInteger("attackNum", 2);
                        force = force * 2;
                        enemy.GetComponent<Rigidbody2D>().AddForce(dir * force);
                        comboTime = 0;
                        comboChance2 = false;
                        comboChance0 = true;
                        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    }
                    Debug.Log("force = " + force);
                    //enemy = GameObject.FindWithTag("Enemy");

                    Debug.Log("Button has been pressed");
                    
                }
                else if (enemy.gameObject.tag == "Boss1" && enemy.GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeAll)
                {
                    dir = (enemy.transform.position - transform.position).normalized;
                    if (comboChance0 == true)
                    {
                        c_Anim.SetInteger("attackNum", 0);
                        force = initForce;
                        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                        //enemy.GetComponent<Rigidbody2D>().AddForce(dir * force);
                        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                        comboTime = 0;
                        comboChance1 = true;
                        comboChance0 = false;
                        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    }
                    else if (comboChance1 == true && comboTime > 20 && comboTime < 40)
                    {
                        c_Anim.SetInteger("attackNum", 1);
                        force = force * 2;
                        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                        enemy.GetComponent<Rigidbody2D>().AddForce(dir * force);
                        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                        comboChance1 = false;
                        comboChance2 = true;
                        comboTime = 0;
                    }
                    else if (comboChance2 == true && comboTime > 20 && comboTime < 40)
                    {
                        c_Anim.SetInteger("attackNum", 2);
                        force = force * 2;
                        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        enemy.GetComponent<Rigidbody2D>().AddForce(transform.up * (force * -1));
                        comboTime = 0;
                        comboChance2 = false;
                        comboChance0 = true;
                        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                        //GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
                        //GetComponent<Rigidbody2D>().AddForce(dir * 50);
                    }
                    Debug.Log("force = " + force);
                    //enemy = GameObject.FindWithTag("Enemy");

                    Debug.Log("Button has been pressed");

                }
            }
        }
        private void FixedUpdate()
        {
            if (comboTime <= 20 || comboTime >= 40)
            {
                chance.SetActive(false);
            }
            if (comboChance1 == true || comboChance2 == true)
            {
                if (comboTime >= 0 && comboTime < 60)
                {
                    if (comboTime > 20 && comboTime < 40)
                    {
                        chance.SetActive(true);
                    }
                    
                    comboTime++;
                }
                
                else
                {

                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    //GetComponent<Rigidbody2D>().AddForce(transform.up * 200);
                    //GetComponent<Rigidbody2D>().AddForce(dir * 50);
                    comboTime = 0;
                    comboChance0 = true;
                    comboChance1 = false;
                    comboChance2 = false;
                }
            }
            //m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            //Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            //for (int i = 0; i < colliders.Length; i++)
            //{
            //    //if (colliders[i].gameObject != gameObject)
            //    //    m_Grounded = true;
            //}
            //m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            
        }


        public void Move(float move, bool crouch, bool jump)
        {
            int k = 1;
            // If crouching, check to see if the character can stand up
            if (k == 0)
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump /*&& m_Anim.GetBool("Ground")*/)
            {
                if(started == false)
                {
                    //Destroy(tempPlat);
                    started = true;
                }
                // Add a vertical force to the player.
                //m_Grounded = false;
                //m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag == "Enemy")
            {
                initForce = 200f;
                enemy = other.gameObject;
                inRange = true;
                //Debug.Log("Enemy in range");
            }
            else if(other.gameObject.tag == "Boss1")
            {
                initForce = 300f;
                enemy = other.gameObject;
                inRange = true;
            }
           
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            //if (other.gameObject.tag == "Platform")
            //{
            //    Debug.Log("Standing on platform");
            //    m_Grounded = true;
            //}
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss1")
            {
                enemy = null;
                inRange = false;
                //Debug.Log("Enemy left range");
            }
            //if (other.gameObject.tag == "Platform")
            //{
            //    m_Grounded = false;
            //}
        }

        IEnumerator attackAnimWait()
        {
            yield return new WaitForSeconds(.5f);
            c_Anim.SetBool("isAttacking", false);
            StopCoroutine(attackAnimWait());
        }

        //private void OnCollisionStay2D(Collision2D other)
        //{
        //    Debug.Log("Something is colliding: " + other.gameObject.tag);
        //    if (other.gameObject.tag == "Platform")
        //    {
        //        Debug.Log("Standing on platform");
        //        m_Grounded = true;
        //    }
        //}

        //private void OnCollisionExit2D(Collision2D other)
        //{
        //    if (other.gameObject.tag == "Platform")
        //    {
        //        m_Grounded = false;
        //    }
        //}
    }
}
