using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class CurrentDebuffs{
 
    public GameObject gameObject;
    public int id;
    public CurrentDebuffs(GameObject icon, int id)
    {
        this.gameObject = icon;
        this.id = id;
    }
}
