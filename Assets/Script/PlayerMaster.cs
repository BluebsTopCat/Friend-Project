using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMaster : MonoBehaviour
{
    public int maxhp;
    public int hp;
    public int maxstamina;
    public int stamina;

    public Slider Hp;
    public Slider Stamina;
    public Slider sheild;
    public Text currenthp;
    public Text currentstamina;

    public TextMeshProUGUI name;

    public string playername;
    
    //This caused problems for me so I commented it out. –Ben
    //public Info statuses;
    
    public bool isai;

    public GameObject controls;
    // Update is called once per frame
    void Update()
    {
        if (hp > maxhp)
        {
            sheild.gameObject.SetActive(true);
            hp = Mathf.Clamp(hp, 0, maxhp * 2);
            sheild.value = (hp - maxhp);
            sheild.maxValue = maxhp;
        }
        else
        {
            sheild.gameObject.SetActive(false);
        }
        
        name.text = playername;
        Stamina.maxValue = maxstamina;
        Stamina.value = stamina;

        Hp.maxValue = maxhp;
        Hp.value = hp;
        
        currenthp.text = "HP: " + hp + "/" + maxhp;
        currentstamina.text = "MP: " + stamina + "/" + maxstamina;
    }

    public void YourTurn()
    {
        if(!isai)
           controls.SetActive(true);
        hp = Mathf.Clamp(hp, 0, maxhp);

    }
}
