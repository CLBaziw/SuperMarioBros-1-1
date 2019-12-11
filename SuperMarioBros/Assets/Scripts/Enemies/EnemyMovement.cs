using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Movement variables
    private float moveSpeed = 0f; //Start idle
    const float goomba = -1.82f;
    
    //Controllers
    public Rigidbody2D rBody;
    public Transform minY;
    public Animator animator;
    public AudioSource audioStomp;
    private ScoreCounter trackerScore;
    private PlayerDeath playerDeath; 

    //Check for Head Hit
    public LayerMask whatIsPlayer;
    public Transform headCheck;
    private Vector2 size = new Vector2(0.6f, 0.01f);
    private bool headHit = false;

    private void Start()
    {
        trackerScore = FindObjectOfType<ScoreCounter>();
        playerDeath = FindObjectOfType<PlayerDeath>();
    }

    private void Update()
    {
        headHit = Physics2D.OverlapBox(headCheck.position, size, 0f, whatIsPlayer); //Check if enemy has been hit on the head
    }

    private void FixedUpdate()
    {
        //If enemy goes below floor, destroy it
        if (transform.position.y < minY.position.y)
        {
            Destroy(gameObject);
        }

        if (headHit)
        {
            //Goombra dies
            animator.SetBool("Death", true); //Change animation to death animation
            
            audioStomp.Play(); //Play stomp noise
            Destroy(gameObject, 0.3f); //Wait __f seconds to destroy enemy

            trackerScore.ScoreChecker("enemy");//Add score
        }
        else
        {
            rBody.velocity = new Vector2(moveSpeed, rBody.velocity.y); //Make enemy walk
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;

        if (collisionTag != "Block" && collisionTag != "Ground" && collisionTag != "Player") //Enemy turns around when hitting collidable object that is not player or ground
        {
            moveSpeed *= -1;
        }
        else if (collisionTag == "Player" && !headHit) //If player hits enemy but not from above, kill player
        {
            //Player dies
            playerDeath.Death();
        }
    }

    //Player hits trigger which starts Goomba's movement
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D boxCollider = GetComponent<Collider2D>();

        if (collision.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            
            moveSpeed = goomba;
        }
    }
}
