using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    public GameObject prefab;
    private Animator animator;
    private GameController gController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D bottomCollider = GetComponent<Collider2D>();

        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == "Brick")
            {
                //if (gController.isBig) //if Mario is big
                //{
                //    //Brick break animation
                //    Destroy(gameObject);
                //}
            }
            else if (gameObject.name == "MultiCoinBox")
            {
                //Let Mario hit bottom for 10 coins within 5 seconds (or something)
            }
            else
            {
                //Turn off collider on bottom of box
                bottomCollider.enabled = false;

                //Create object (coin, mushroom, flower, star, etc)
                Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 1.2f, 0);
                Instantiate(prefab, spawnPos, prefab.transform.rotation);

                //Change box animation
                animator.SetBool("Triggered", true);
            }            
        }
    }
}