using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    private GameObject prefab;
    public GameObject coinPrefab;
    public GameObject brickPrefab;
    public GameObject mushroomPrefab;
    public GameObject flowerPrefab;
    public GameObject starPrefab;

    public Animator animator;
    private Rigidbody2D rBody;
    public Collider2D bottomCollider;
    private PowerUpTracker trackerPU;

    private Vector2 startPos;
    private bool shake;
    private bool isFalling = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        trackerPU = FindObjectOfType<PowerUpTracker>();

        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        double difHeight = 0.5f;

        if (shake)
        {
            double difPos = transform.position.y - startPos.y;
            double difPosRounded = Math.Round(difPos, 1);

            Debug.Log(difPos);

            if (difPos < difHeight && !isFalling)
            {
                Debug.Log("Rise");
                rBody.bodyType = RigidbodyType2D.Dynamic;
                rBody.velocity = new Vector2(0, 10f);

            }
            else if (difPosRounded >= difHeight)
            {
                rBody.velocity *= -2f;
                isFalling = true;
                Debug.Log("Fall");    
            }

            if (difPos < 0)
            {
                shake = false;

                Debug.Log("Stop falling");
                rBody.bodyType = RigidbodyType2D.Static;
                transform.position = startPos;

                if (name.Contains("Brick"))
                {
                    if (trackerPU.isBig) //If player is big destroy brick
                    {
                        IEnumerator waitBreak = WaitToBreak();

                        prefab = brickPrefab;
                        InstantiateObj();
                        StartCoroutine(waitBreak);
                    }
                }
                else
                {
                    InstantiateObj();
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shake = true;

            if (name == "MultiCoinBox")
            {
                //Let Mario hit bottom for 10 coins within 5 seconds (or something)
            }
            else
            {
                //Change box animation
                animator.SetBool("Triggered", true);

                //Turn off collider on bottom of box
                bottomCollider.enabled = false;

                if (name.Contains("Coin")) //Box should give out coins
                {
                    prefab = coinPrefab;
                }
                else if (name.Contains("Star")) //Brick should give star man powerup
                {
                    prefab = starPrefab;
                }
                else
                {
                    CheckPowerUp(); //If player is small, give mushroom. If player is big, give flower.
                }

                Debug.Log(name);                
            }
        }
    }

    //If player is small, give mushroom. If player is big, give flower.
    void CheckPowerUp()
    {
        if (trackerPU.isBig)
        {
            prefab = flowerPrefab;
        }
        else
        {
            prefab = mushroomPrefab;
        }
    }

    void InstantiateObj()
    {
        Vector3 spawnPos; 

        //Create object (coin, mushroom, flower, star, etc)
        if (prefab == coinPrefab)
        {
            spawnPos = transform.position + new Vector3(0, 1.2f, 0);
        }
        else
        {
            spawnPos = transform.position + new Vector3(0, 1f, 0);
        }
       
        Instantiate(prefab, spawnPos, prefab.transform.rotation);
    }

    IEnumerator WaitToBreak()
    {
        yield return new WaitForSeconds(0.12f);

        Destroy(gameObject);
    }
}