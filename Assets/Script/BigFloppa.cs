using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BigFloppa : MonoBehaviour
{
    public int id = 0;

    private ItemLib weaponslist;
    // Update is called once per frame
    private void Start()
    {
        weaponslist = GameObject.FindObjectOfType<ItemLib>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = weaponslist.gunarray[id].sprite;
    }

    void Update()
    {
        this.transform.localScale = new Vector3(math.sin(Time.time),1f,1f);
    }
    
    public void Interact(PlayerController player)
    {
        Debug.Log("Interact Succeeded");
        int newid = player.gun;
        player.gun = id;
        id = newid;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = weaponslist.gunarray[id].sprite;
    }
}
