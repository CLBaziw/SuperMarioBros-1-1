using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D rBody;
    private float upSpeed = 6f;

    private ScoreCounter scoreTracker;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        scoreTracker = FindObjectOfType<ScoreCounter>();

        rBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(scoreTracker);

        scoreTracker.ScoreChecker("coin");
        Destroy(gameObject);
    }
}
