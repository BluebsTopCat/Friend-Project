using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingTrigger : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(delayedscan()); 
    }
    

    public IEnumerator delayedscan()
    {
        yield return new WaitForSeconds(1f);
        AstarPath.active.Scan();
    }
}
