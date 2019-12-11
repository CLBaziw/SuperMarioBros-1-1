using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTracker : MonoBehaviour
{
    //Player Controller
    private PlayerController playerC;
    private Animator animator;
    private GameObject player;
    private AudioSource audioSource;
    private AudioClip clipSel;
    public AudioClip grow;
    public AudioClip shrink;
    
    //Tracker variables
    public bool isBig, isFlower, isStar;

    //Prefabs
    private GameObject prefab;
    public GameObject babyMario;
    public GameObject superMario;

    //Layer Indexes 
    const int Mario = 0;
    const int Star = 1;
    const int Flower = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerC = FindObjectOfType<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransformPlayer()
    {
        clipSel = grow;

        if (isStar)
        {
            IEnumerator starman = Starman();

            StartCoroutine(starman);

        }
        else if (isFlower && isBig)
        {
            //Enable shift click
            animator.SetLayerWeight(Flower, 1);
        }
        else 
        {
            animator.SetLayerWeight(Mario, 1);

            if (isBig)
            {
                //Make big
                prefab = superMario;

                BigSmall();
;            }
            else if (!isBig)
            {
                //Make small
                prefab = babyMario;
                clipSel = shrink;

                BigSmall();
            }
        }

        audioSource.clip = clipSel;
        audioSource.Play();
    }

    IEnumerator Starman()
    {
        Debug.Log("Start starman");
        //Transform into StarMan
        animator.SetLayerWeight(Star, 1);

        //Increase speed
        playerC.horizForce *= 1.5f;

        //Wait for 10 seconds to pass
        yield return new WaitForSeconds(10f);

        Debug.Log("end starman");

        //Decrease speed
        playerC.horizForce /= 1.5f;
        
        //Transform back
        isStar = false;
        animator.SetLayerWeight(Star, 0);
        TransformPlayer();
    }

    void BigSmall()
    {
        Vector3 spawnPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f);

        Destroy(player);

        player = Instantiate(prefab, spawnPos, prefab.transform.rotation);
        animator = player.GetComponent<Animator>();
    }
}
