using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{

    public GameManager gm;
    public string objectType;

    private void Update()
    {
        if (gm.isDaytime)
        {
            if (objectType == "day")
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
        else
        {
            if (objectType == "day")
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
