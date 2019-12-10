﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    public GameObject prefab;
    public Animator animator;
    public Collider2D bottomCollider;

    private ScoreCounter tracker;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tracker = FindObjectOfType<ScoreCounter> ();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnumerator coroutine = WaitToBreak();

        if (collision.gameObject.CompareTag("Player") && CompareTag("Block"))
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
                    InstantiateObj();

                    Debug.Log("about to wait");

                    StartCoroutine(coroutine);

                    Destroy(gameObject);
                    //}

                }
                else
                {
                    InstantiateObj();
                }
            }
        }
    }

    void InstantiateObj()
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

        yield return new WaitForSeconds(200f);
    }
}