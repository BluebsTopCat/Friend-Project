using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStack : MonoBehaviour
{
    public int money;
    private SpriteRenderer cashmoney;
    public Sprite[] cashmoneyimages;
    // Update is called once per frame

    private void Start()
    {
        cashmoney = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (money != 0)
            cashmoney.sprite = cashmoneyimages[money - 1];
    }
}
