using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Player1Controller p1; 
    public Player2Controller p2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P1") || collision.CompareTag("P2"))
        {
            p1.spawnPosition = transform.position;
            p2.spawnPosition = transform.position;
        }

    }

}
