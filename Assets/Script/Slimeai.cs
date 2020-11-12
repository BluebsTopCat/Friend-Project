using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slimeai : MonoBehaviour
{
    private GameObject player;

    public float speed;
    public int hp = 10;
    public int maxhp = 10;
    public Slider healthbar;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        this.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;     
        healthbar.maxValue = maxhp;
        healthbar.value = hp;
        
        if(hp <= 0)
            Destroy(this.gameObject);
    }

    public void hurt(int damg)
    {
        Debug.Log("RegisteredHit!");
        this.hp -= damg;
    }
}
