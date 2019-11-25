using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //Horizontal Movement
    public Vector2 movement;
    public float moveSpeed = 5f;
    public Animator animator;

    //Jumping
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    public bool grounded;
    public LayerMask whatisGround;
    public bool stoppedJumping;
    public Transform groundCheck;
    public float groundCheckRadius;

    private Rigidbody2D rBody; 

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatisGround);
        
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(Input.GetButton("Jump"));

        //If you presss the space bar down
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                jumpChecker();
                stoppedJumping = false;
            }
        }
        //If you keep holding the space button
        if (Input.GetButton("Jump") && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                jumpChecker();
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        //If you stop holding the space button
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        //Animation for horizontal movement
        if (movement.x != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

        //Change horizontal position
        rBody.MovePosition(rBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void jumpChecker()
    {
        rBody.velocity = new Vector2(rBody.velocity.x, jumpForce);
    }
}
