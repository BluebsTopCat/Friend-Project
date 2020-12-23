using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Vector3 destination;
    public float speed;

    public GameObject blast;
    public int damage = 1;
    // Update is called once per frame
    void Update()
    {
        float AngleRad = Mathf.Atan2(destination.y - this.transform.position.y, destination.x - this.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        this.transform.Translate(new Vector3(speed*Time.deltaTime, 0f, 0f));
        
     if (Vector3.Distance(destination, this.transform.position) < 10)
     {
         GameObject shot = Instantiate(blast);
         shot.transform.position = this.transform.position;
         Destroy(this.gameObject);
     }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Slimeai>())
        {
            GameObject shot = Instantiate(blast);
            shot.transform.position = this.transform.position;
            other.gameObject.GetComponent<Slimeai>().hurt(damage);
            Destroy(this.gameObject);
        }
    }
}
