using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool on;
    // Start is called before the first frame update
    void Update()
    {
        if (on)
            this.gameObject.transform.localScale = new Vector3(7.5f,7.5f,7.5f);
        else
            this.gameObject.transform.localScale = new Vector3(-7.5f,7.5f,7.5f);
    }

}
