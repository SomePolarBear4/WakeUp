using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    public float Bouncyness;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag== "Player")
        {
           // player.GetComponent<PlayerMovement.jump()>();
        }
    }
}
