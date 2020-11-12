using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damage = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Slimeai>())
        {
            other.gameObject.GetComponent<Slimeai>().hurt(damage);
        }
    }
}
