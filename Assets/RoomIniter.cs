using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class RoomIniter : MonoBehaviour
{
    public bool cleared;

    public bool active;
    public List<GameObject> enemies;
    public GameObject[] walls;
    private int alivecount = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && (!cleared && !active))
        {
            StartCoroutine(Encounter());
        }

    }

    private int checknumber()
    {
        alivecount = 0;
        for(var i=0;i<enemies.Count;i++)
        {
            if(enemies[i] != null)
            {
                alivecount++;
            }
        }

        return alivecount;
    }


    IEnumerator Encounter()
    {
       
            Debug.Log("Start Room!");
            active = true;
            
            foreach (GameObject g in walls)
            {
                g.SetActive(true);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                
                Vector3 spawnpos = new Vector3(
                    UnityEngine.Random.Range(this.GetComponent<BoxCollider2D>().bounds.min.x,
                        this.GetComponent<BoxCollider2D>().bounds.max.x),
                    UnityEngine.Random.Range(this.GetComponent<BoxCollider2D>().bounds.min.y,
                        this.GetComponent<BoxCollider2D>().bounds.max.y), 0f);;
                

                GameObject enemy = Instantiate(enemies[i]);
                enemy.transform.position = spawnpos;
                enemies[i] = enemy;
            }
            
            yield return new WaitUntil( () => checknumber() == 0);
            
            active = false;
            cleared = true;
            foreach (GameObject g in walls)
            {
                g.SetActive(false);
            }
    }
}
