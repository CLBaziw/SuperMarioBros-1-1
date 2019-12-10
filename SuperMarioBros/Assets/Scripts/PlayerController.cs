using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Horiztontal Movement
    private float horiz;
    const float moveSpeed = 7.5f;
    private Transform transBoundary;
    private Vector3 vecBoundary;

    //Jumping movement
    public bool isJumping;
    public bool isGrounded;
    const float jumpSpeed = 6f;
    const float jumpTime = 0.2f;
    private float jumpTimeCounter;
    
    //Controllers
    private Animator animator;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transBoundary = GameObject.Find("MinXMaxY").GetComponent<Transform>();

        jumpTimeCounter = jumpTime;
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        horiz = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            jumpTimeCounter = jumpTime;
        }
    }

    private void FixedUpdate()
    {
        //Layer indexes
        const int WALK = 1;
        const int JUMP = 2;

        if (Input.GetButtonDown("Jump")) //If space was pressed
        {
            Debug.Log(isGrounded);

            if (isGrounded) //If player is on the ground
            {
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
                //rBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                isJumping = true;
                isGrounded = false;

                animator.SetLayerWeight(JUMP, 1);
            }
        }

        if ((Input.GetButton("Jump") && isJumping))
        {
            if (jumpTimeCounter > 0)
            {
                //keep jumping!
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
                //rBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (!Input.GetButtonUp("Jump"))
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
            isJumping = false;
            animator.SetLayerWeight(JUMP, 0);
        }

        if (!isJumping)
        {
            if (horiz != 0)
            {
                animator.SetFloat("Horizontal", horiz);
                animator.SetLayerWeight(WALK, 1);
            }
            else if (horiz == 0)
            { 
                animator.SetLayerWeight(JUMP, 0);
                animator.SetLayerWeight(WALK, 0);
            }
        }
       
        
        

        rBody.velocity = new Vector2(horiz * moveSpeed, rBody.velocity.y);

        vecBoundary = transBoundary.position;

        rBody.position = new Vector2(Mathf.Clamp(rBody.position.x, vecBoundary.x, float.MaxValue ), Mathf.Clamp(rBody.position.y, -3, vecBoundary.y));
    }
}
