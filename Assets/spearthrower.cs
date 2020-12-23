using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearthrower : MonoBehaviour
{
    public Vector3 start;
    public float starttime;
    public Vector3 end;
    public float speed;
    private float size;
    private float angleDeg;
    // Start is called before the first frame update
  
    void Start()
    {
        start = this.transform.position;
        end = GameObject.FindGameObjectWithTag("Player").transform.position;
        starttime = Time.time;
        float AngleRad = Mathf.Atan2(end.y - start.y, end.x - start.x);
        angleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, angleDeg + 224);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(start, end, (Time.time - starttime) * speed);
        if (this.transform.position == end)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" )
        {
            other.GetComponent<PlayerController>().Hurt(2);
        }
    }
}
