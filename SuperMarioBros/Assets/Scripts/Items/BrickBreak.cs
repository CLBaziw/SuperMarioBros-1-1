using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    private Rigidbody2D rBody;
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

    //Score Tracker
    private ScoreCounter trackerScore;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        trackerScore = FindObjectOfType<ScoreCounter>();

        startingY = transform.position.y;
        currentY = startingY;

        rBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        trackerScore.ScoreChecker("brick");
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

                Debug.Log(name);
                Destroy(gameObject);
            }

            for (int j = 0; j < 4; j++)
            {
                if (smallBrick[j].transform.position.y < startingY)
                {
                    Destroy(smallBrick[j]);
                }
            }
        }
    }
}
