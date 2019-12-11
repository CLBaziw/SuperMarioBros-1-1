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
    private PowerUpTracker trackerPU;

    private Vector2 startPos;
    public bool shake;
    public GameObject boxTriggered;
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

            if (difPos < difHeight && !isFalling)
            {
                rBody.bodyType = RigidbodyType2D.Dynamic;
                rBody.velocity = new Vector2(0, 10f);

            }
            else if (difPos >= difHeight)
            {
                rBody.velocity *= -2f;
                isFalling = true;
            }

            if (difPos < 0 && shake)
            {
                shake = false;

                rBody.bodyType = RigidbodyType2D.Static;
                transform.position = startPos;

                Debug.Log("Called InstantiateObj");
                InstantiateObj();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shake = true;
            boxTriggered = gameObject;
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
        bool objectUsed = false;

        Debug.Log(name);

        if (name == "MultiCoinBox")
        {
            objectUsed = false;
            
            //Let Mario hit bottom for 10 coins within 5 seconds (or something)
        }
        else if (name.Contains("Brick"))
        {
            if (trackerPU.isBig) //If player is big destroy brick
            {
                Debug.Log("Player is big");
                
                prefab = brickPrefab;
                Destroy(gameObject, 0.12f);

                objectUsed = true;
            }
            else
            {
                Debug.Log("Player is small");
                prefab = null;

                objectUsed = false;
            }
        }
        else
        {
            objectUsed = false;

            if (name.Contains("Coin")) //Box should give out coins
            {
                prefab = coinPrefab;
            }
            else if (name.Contains("Star")) //Brick should give star man powerup
            {
                prefab = starPrefab;
            }
            else if (name.Contains("Power"))
            {
                CheckPowerUp(); //If player is small, give mushroom. If player is big, give flower.
            }
        }

        //Create object (coin, mushroom, flower, star, etc)
        if (prefab == coinPrefab)
        {
            spawnPos = transform.position + new Vector3(0, 1.2f, 0);
        }
        else
        {
            spawnPos = transform.position + new Vector3(0, 1f, 0);
        }

        if (prefab != null)
        {
            Debug.Log("Instantiating object");
            Instantiate(prefab, spawnPos, prefab.transform.rotation);

            //Change box animation
            animator.SetBool("Triggered", true);
        }

        Debug.Log(gameObject.name);
        Debug.Log(GetComponent<EdgeCollider2D>().isTrigger);
        
        if (objectUsed)
        {
            Destroy(GetComponent<EdgeCollider2D>()) ;
        }
    }
}