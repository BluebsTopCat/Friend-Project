using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonspears : MonoBehaviour
{
    public GameObject spear;
    private Vector3 end;
    public float speed;
    void Update()
    {
        if (Time.time % 2 < 0.002)
        {
            GameObject spears = Instantiate(spear);
            spears.transform.position = this.transform.position;
        }

        end = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 direction = transform.position - end;
        direction.Normalize();

        this.transform.Translate(direction * speed);

    }
}
