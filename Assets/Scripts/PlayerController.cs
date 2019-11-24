using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public variables
    public float moveSpeed = 5f;
    public Vector2 movement;
    //public Animator animator;

    //Private variables
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Grabbing movement
        movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");



        if (movement != Vector2.zero)
        {
            //Call animator for movement
            //animator.SetFloat("Horizontal", movement.x);
            //animator.SetFloat("Vertical", movement.y);
            //animator.SetLayerWeight(1, 1);
        }
        else
        {
            //animator.SetLayerWeight(1, 0);
        }

        rBody.MovePosition(rBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
