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
    const float jumpSpeed = 18f;
    
    //Controllers
    private Animator animator;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transBoundary = GameObject.Find("MinXMaxY").GetComponent<Transform>();
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
            rBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse); //Impulse = add force right away
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

        rBody.velocity = new Vector2(horiz * moveSpeed, rBody.velocity.y);

        vecBoundary = transBoundary.position;

        rBody.position = new Vector2(Mathf.Clamp(rBody.position.x, vecBoundary.x, float.MaxValue ), Mathf.Clamp(rBody.position.y, -3, vecBoundary.y));
    }

    private void OnCollisionEnter2D(Collision2D floor)
    {
        if (floor.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
    }
}
