using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private PowerUpTracker trackerPU;

    private AudioSource audioSource;
    public AudioClip death;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        trackerPU = FindObjectOfType<PowerUpTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathChecker()
    {
        if (trackerPU.isBig)
        {
            trackerPU.isBig = false;

            trackerPU.TransformPlayer();
        }
        else if (!trackerPU.isBig)
        {
            Debug.Log("Mario is small");

            audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            audioSource.clip = death;
            StartCoroutine("KillPlayer");
        }
    }

    IEnumerator KillPlayer()
    {
        Debug.Log("Kill player 1");

        audioSource.Play();

        FreezeEnemies();

        PlayerDeathAnimation();

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);

        SceneManager.LoadSceneAsync(0);
    }

    void FreezeEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void PlayerDeathAnimation()
    {
        Animator animator = GetComponent<Animator>();
        Collider2D collider2D = GetComponent<Collider2D>();
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();

        GameObject.Find("GameController").GetComponent<AudioSource>().enabled = false;

        animator.SetBool("Death", true);
        Destroy(collider2D);

        gameObject.GetComponent<PlayerController>().enabled = false;

        rBody.velocity = new Vector2(0, 12);
    }
}
