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
    public Collider2D bottomCollider;

    private PowerUpTracker trackerPU;

    private void Start()
    {
        animator = GetComponent<Animator>();
        trackerPU = FindObjectOfType<PowerUpTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnumerator coroutine = WaitToBreak();

        if (collision.gameObject.CompareTag("Player"))
        {
            if (name == "MultiCoinBox")
            {
                //Let Mario hit bottom for 10 coins within 5 seconds (or something)
            }
            else
            {
                if (name.Contains("Brick"))
                {
                    if (trackerPU.isBig)
                    {
                        prefab = brickPrefab;

                        InstantiateObj();

                        Debug.Log("about to wait");

                        StartCoroutine(coroutine);
                    }
                }
                else
                {
                    if (name.Contains("Coin"))
                    {
                        prefab = coinPrefab;
                    }
                    else if (name.Contains("Star"))
                    {
                        prefab = starPrefab;
                    }
                    else
                    {
                        CheckPowerUp();
                    }

                    Debug.Log(name);
                    InstantiateObj();
                }
            }
        }
    }

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

        //Change box animation
        animator.SetBool("Triggered", true);

        //Turn off collider on bottom of box
        bottomCollider.enabled = false;

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
        Debug.Log("Wait");

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}