using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class GunProperties
{
    public string name;
    public Sprite sprite;
    public int damage;
    public int bullets;
    public float spread;
    public float speed;
    public int maxammo;
    public bool auto;
    public GunProperties(string Name, Sprite Sprite, int Damage, int Bullets, float Spread, float Speed, int ammo, bool automatic)
    {
        name = Name;
        sprite = Sprite;
        damage = Damage;
        bullets = Bullets;
        spread = Spread;
        speed = Speed;
        maxammo = ammo;
        auto = automatic;
    }
}
