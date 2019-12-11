
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Horizontal movement
    private float horiz;
    const float moveSpeed = 5f;
    private float horizForce = moveSpeed;

    //Controllers
    public Rigidbody2D rBody;
    public Animator animator;
    public GameObject topLeftBoundary;
    private Vector2 vecTLBoundary;
    public GameObject bottomBoundary;
    private float botBoundary;
    public AudioSource audioJump;

    //Jumping movement
    public bool isGrounded;
    public bool isJumping;
    const float upForce = 10f;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    const float fallMultiplier = 5f;

    //Jump timer
    const float jumpTime = 0.3f;
    private float timeCounter = 0;

    //Layer Index
    private const int JUMP = 2;
    private const int WALK = 1;

    private void Start()
    {
        isGrounded = true;
        isJumping = false;
        timeCounter = jumpTime;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        horiz = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            timeCounter = jumpTime;
        }

        Jump();
    }

    private void FixedUpdate()
    {
        vecTLBoundary = topLeftBoundary.transform.position;
        botBoundary = bottomBoundary.transform.position.y;

        //Faster fall time
        if (rBody.velocity.y < -0.01)
        {
            rBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        Movement();

        if (isGrounded)
        {
            animator.SetLayerWeight(JUMP, 0);
            if (horiz != 0)
            {
                animator.SetFloat("Horizontal", horiz);
                animator.SetLayerWeight(WALK, 1);
            }
            else if (horiz == 0)
            {
                animator.SetLayerWeight(WALK, 0);
            }
        }
        else
        {
            animator.SetLayerWeight(JUMP, 1);
        }

        //If player has fallen
        if (transform.position.y < bottomBoundary.transform.position.y)
        {
            //Player dies
            Debug.Log("Player has fallen");
        }
    }

    private void Movement()
    {
        rBody.velocity = new Vector2(horiz * horizForce, rBody.velocity.y);

        rBody.position = new Vector2(Mathf.Clamp(rBody.position.x, vecTLBoundary.x, float.MaxValue), Mathf.Clamp(rBody.position.y, -3, vecTLBoundary.y));
    }

    private void Jump()
    {
        //if space bar is pressed
        if (Input.GetButtonDown("Jump"))
        {
            //if Mario is on the ground
            if (isGrounded)
            {
                //Play jump SFX
                audioJump.Play();

                ApplyVerticalVelocity();
                isJumping = true;
            }
        }

        //if space bar is held
        if (Input.GetButton("Jump") && isJumping)
        {
            //Keep jumping
            if (timeCounter > 0)
            {
                ApplyVerticalVelocity();
                timeCounter -= Time.deltaTime;
            }
        }

        //when space bar is lifted
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            timeCounter = 0;
        }
    }

    private void ApplyVerticalVelocity()
    {
        rBody.velocity = Vector2.up * upForce;
    }
}



