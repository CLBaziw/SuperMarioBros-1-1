using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 fireballV;
    private PlayerController playerC;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerC = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        fireballV = new Vector2(5, 5);

        if (rb.velocity.y < fireballV.y)
        {
            rb.velocity = fireballV * playerC.horiz;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;

        if (collisionTag == "Ground")
        {
            rb.velocity = new Vector2(fireballV.x, -fireballV.y);
        }
        else if (collisionTag == "Enemy")
        {
            //Destroy enemy
            Debug.Log("Destory enemy");
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
