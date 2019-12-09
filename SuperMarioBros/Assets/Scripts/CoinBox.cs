using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    public GameObject prefabCoin;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Triggered()
    {
        Vector2 coinSpawn = gameObject.transform.position + new Vector3(0, 1.2f, 0);

        {
            GameObject coin = Instantiate(prefabCoin, coinSpawn, prefabCoin.transform.rotation);

            //Change box to already hit box

            animator.SetBool("Triggered", true);
        }
    }
}
