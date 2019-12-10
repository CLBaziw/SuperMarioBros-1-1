using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private PlayerController playerC;

    private void Start()
    {
        playerC = FindObjectOfType<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Kill enemy");
        }
        else
        {
            Debug.Log("Player hit ground");
            playerC.isGrounded = true;
            Debug.Log(playerC.isGrounded);
        }
    }
}
