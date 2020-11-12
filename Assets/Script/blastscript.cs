using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastscript : MonoBehaviour
{
    public ParticleSystem item;

    private void Update()
    {
        if(item.isPlaying != true)
            Destroy(this.gameObject);
    }
}
