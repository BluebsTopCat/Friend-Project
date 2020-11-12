using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool On;


    private void Update()
    {
        if (On)
            this.transform.eulerAngles = new Vector3(-60, 0, 0);
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>())
            On = true;
        else
        {
            On = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>())
            On = false;

    }
}
