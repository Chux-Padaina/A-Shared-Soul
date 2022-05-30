using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;
    public float movespeed;
    public int target;

    public GameManager gm;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
    }
    private void Update()
    {
        if (gm.isDaytime)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, 
                             waypoints[target].position, movespeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        if(transform.position == waypoints[target].position)
        {
            if(target == waypoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target++;
            }
        }
    }
}
