using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class AffectorTable{
    
    public string name;
    public string info;
    public string affects;
    public Sprite image;
    public AffectorTable(string name, string info, string affects, Sprite icon){
        this.name = name;
        this.info = info;
        this.affects = affects;
        this.image = icon;
    }
}
