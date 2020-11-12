using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlavourTextforBattle : MonoBehaviour
{
    public void dothething(string name)
    {
        this.GetComponent<TextMeshProUGUI>().text = name;

    }
    public void undothething()
    {
        
        this.GetComponent<TextMeshProUGUI>().text = "Choose a move!";

    }

    public void Select(int hp)
    {
        if(hp > 0)
            GameObject.FindGameObjectWithTag("Turnmaster").GetComponent<Turns>().Heal(hp);
        else
            GameObject.FindGameObjectWithTag("Turnmaster").GetComponent<Turns>().Attack(hp);
        
        GameObject.FindGameObjectWithTag("Turnmaster").GetComponent<Turns>().turn++;
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

}
