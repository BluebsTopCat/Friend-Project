using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Slimeai : MonoBehaviour
{
    private GameObject[] player;
    public float speed;
    public int hp = 10;
    public int maxhp = 10;
    public Slider healthbar;

    public AIDestinationSetter ai;

    public AIPath path;

    public Renderer thisenemy;
    // Update is called once per frame
    void Update()
    {
        
        if (thisenemy.isVisible)
            path.canMove = true;
        else
            path.canMove = false;
        
        player = GameObject.FindGameObjectsWithTag("Player");
        if(player != null)
            ai.target = GetClosestPlayer(player).transform;
        
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

    GameObject GetClosestPlayer(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        return tMin;
    }
}
