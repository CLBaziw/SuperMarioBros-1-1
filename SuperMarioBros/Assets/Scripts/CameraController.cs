using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float maxX; //Furthest player has moved to the right
    private Transform transCamera; //Camera transform
    private Transform transPlayer; // Player transform

    // Start is called before the first frame update
    void Start()
    {
        transCamera = GetComponent<Transform>();
        transPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        maxX = transCamera.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        float playerX = transPlayer.position.x;

        if (playerX > maxX)
        {
            maxX = transPlayer.position.x;
            transCamera.position = new Vector3(maxX, transCamera.position.y, transCamera.position.z);
        } 
    }
}
