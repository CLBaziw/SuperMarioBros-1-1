using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public PlayerController pCont;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            pCont.isGrounded = true;
            Debug.Log(pCont.isGrounded);
        }
    }
}
