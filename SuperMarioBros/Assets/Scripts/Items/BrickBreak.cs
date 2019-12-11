using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    private Rigidbody2D rBody;
    private AudioSource audioSource;
    private float upSpeed = 6f;

    private float startingY;
    private float currentY;
    const float distanceUp = 0.1f;

    //Variables for small bricks
    public GameObject downwardsPrefab;
    public GameObject upwardsPrefab;
    private GameObject activePrefab;
    private GameObject[] smallBrick = new GameObject[4];
    private float[] upForce = new float[] { -0.5f, -0.25f, 0.25f, 0.5f };
    private bool smallBricksSpawned;

    //Score Tracker
    private ScoreCounter trackerScore;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        trackerScore = FindObjectOfType<ScoreCounter>();
        audioSource = GetComponent<AudioSource>();

        startingY = transform.position.y;
        currentY = startingY;

        smallBricksSpawned = false;

        rBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        trackerScore.ScoreChecker("brick");

        audioSource.Play();
    }

    private void FixedUpdate()
    {
        currentY = transform.position.y;

        if (currentY > startingY + distanceUp)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    activePrefab = upwardsPrefab;
                }
                else
                {
                    activePrefab = downwardsPrefab;
                }

                smallBrick[i] = Instantiate(activePrefab, transform.position, activePrefab.transform.rotation);
                Rigidbody2D rbSmallBrick = smallBrick[i].GetComponent<Rigidbody2D>();
                rbSmallBrick.AddForce(transform.position * upForce[i], ForceMode2D.Impulse);

                Destroy(gameObject);

                Destroy(smallBrick[i], 1f);
            }
        }
    }

    
}
