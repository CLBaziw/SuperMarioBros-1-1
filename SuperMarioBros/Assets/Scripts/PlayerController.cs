using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Horiztontal Movement
    public float moveSpeed = 5f;
    public float horiz;

    //Jumping movement
    private bool isJumping;
    
    //Controllers
    private Animator animator;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horiz = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        //Layer indexes
        const int WALK = 1;
        const int JUMP = 2;

        if (Input.GetAxis("Jump") > 0 && !isJumping)
        {
            rBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse); //Impulse = add force right away
            isJumping = true;

            animator.SetLayerWeight(JUMP, 1);
        }

        if (horiz != 0) //Player is walking
        {
            animator.SetFloat("Horizontal", horiz);

            if (!isJumping)
            {
                animator.SetLayerWeight(WALK, 1);
            }
        }
        else if (!isJumping)  //Player is idle
        {
            animator.SetLayerWeight(WALK, 0);
            animator.SetLayerWeight(JUMP, 0);
        }



        /*
            --------------------------------------------------
            if player is jumping = Input.GetAxis("Jump") > 0 && isGrounded
                make player jump
                isGrounded = false
                set layer JUMP
            else
                isGrounded = true

            if player is moving = horiz != 0
                set horizontal value
                if isGrounded = true
                    set layer WALK
            else
                set layer IDLE
        */

        rBody.velocity = new Vector2(horiz * 7.5f, rBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D floor)
    {
        if (floor.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
    }
}
