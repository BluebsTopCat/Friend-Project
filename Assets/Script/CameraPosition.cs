using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public int width;

    public int height;

    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerpos = player.transform.position;
        Vector3 camPos = new Vector3(Mathf.Round(playerpos.x/width)*width,Mathf.Round(playerpos.y/height)*height, this.transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, camPos, ref velocity, smoothTime);
    }
}
