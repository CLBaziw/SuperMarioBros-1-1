
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Horizontal movement
    public float horiz;
    const float moveSpeed = 5f;
    public float horizForce = moveSpeed;

    //Controllers
    public Rigidbody2D rBody;
    public Animator animator;
    public AudioSource audioJump;
    
    //Movement Boundaries
    private Vector2 vecTLBoundary;
    private GameObject bottomBoundary;
    private GameObject topLeftBoundary;
    private float botBoundary;

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

    //Fireball
    public GameObject fireball;
    private Vector3 spawnPos;

    //Scripts
    private PlayerDeath playerDeath;
    private PowerUpTracker trackerPU;

    private bool death = false;

    private void Start()
    {
        playerDeath = GetComponent<PlayerDeath>();
        trackerPU = FindObjectOfType<PowerUpTracker>();

        bottomBoundary = GameObject.Find("MinY");
        topLeftBoundary = GameObject.Find("MinXMaxY");

        isGrounded = true;
        isJumping = false;
        timeCounter = jumpTime;

        horiz = 0.1f;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        horiz = Input.GetAxisRaw("Horizontal");

        if (isGrounded)
        {
            timeCounter = jumpTime;
        }

        if (trackerPU.isFlower)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Fireball();
            }
        }

        Jump();
        Movement();
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

        if (isGrounded)
        {
            animator.SetBool("Jump", false);
            if (horiz != 0)
            {
                animator.SetFloat("Horizontal", horiz);
                animator.SetBool("Walking", true);
            }
            else if (horiz == 0)
            {
                animator.SetBool("Walking", false);
            }
        }
        else
        {
            animator.SetBool("Jump", true);
        }

        //If player has fallen
        if (transform.position.y < bottomBoundary.transform.position.y & !death)
        {
            death = true; 

            trackerPU.isBig = false;
            playerDeath.DeathChecker();
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

    private void Fireball()
    {
        if (horiz < 0)
        {
            spawnPos = transform.position + new Vector3(-1f, 0, 0);
        }
        else
        {
            spawnPos = transform.position + new Vector3(1f, 0, 0);
        }

        Instantiate(fireball, spawnPos, fireball.transform.rotation);
    }
}



