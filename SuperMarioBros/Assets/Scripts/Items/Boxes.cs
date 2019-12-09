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
            if (name == "MultiCoinBox")
            {
                //Let Mario hit bottom for 10 coins within 5 seconds (or something)
            }
            else
            {              
                if (tag == "Brick")
                {
                    //if (gController.isBig)
                    //{
                        instantiateObj();

                        Debug.Log("about to wait");

                    StartCoroutine("WaitToBreak");

                        Destroy(gameObject);
                    //}
                        
                }
                else
                {
                    instantiateObj();
                }
            }            
        }

        void instantiateObj()
        {
            //Turn off collider on bottom of box
            bottomCollider.enabled = false;

            //Create object (coin, mushroom, flower, star, etc)
            Vector3 spawnPos = transform.position + new Vector3(0, 1.2f, 0);
            Instantiate(prefab, spawnPos, prefab.transform.rotation);

            //Change box animation
            animator.SetBool("Triggered", true);
        }

        IEnumerator WaitToBreak()
        {
            Debug.Log("Wait");

            yield return new WaitForSeconds(200);
        }
    }
}