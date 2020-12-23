using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class Slimeai : MonoBehaviour
{
    public int chanceofmoneydrop;
    public int minmoney;
    public int maxmoney;
    private GameObject[] player;
    public float speed;
    public int hp = 10;
    public int maxhp = 10;
    public Slider healthbar;
    public GameObject dropcash;
    public AIDestinationSetter ai;

    public AIPath path;
    public bool useai = true;
    public Renderer thisenemy;
    // Update is called once per frame
    void Update()
    {
        if (useai)
        {
            if (thisenemy.isVisible)
                path.canMove = true;
            else
                path.canMove = false;

            player = GameObject.FindGameObjectsWithTag("Player");
            if (player != null)
                ai.target = GetClosestPlayer(player).transform;
        }

        healthbar.maxValue = maxhp;
        healthbar.value = hp;

        if (hp <= 0)
        {
            int rand = UnityEngine.Random.Range(0, 10);
            if (rand < chanceofmoneydrop)
            {
                GameObject cash = Instantiate(dropcash);
                cash.transform.position = this.transform.position;
                dropcash.GetComponent<MoneyStack>().money = UnityEngine.Random.Range(minmoney, maxmoney);
            }
            Destroy(this.gameObject);
        }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<PlayerController>().Hurt(1);
    }
}
