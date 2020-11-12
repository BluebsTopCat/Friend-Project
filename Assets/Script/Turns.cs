using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turns : MonoBehaviour
{
    public List<string> TurnPlayers = new List<string>();
    public int turn;
    public Text turndisp1;
    public Text turndisp2;
    public Text turndisp3;
    private GameObject[] fighters;
    private void Start()
    {
       fighters = GameObject.FindGameObjectsWithTag("Combatant");
        foreach (GameObject g in fighters)
        {
            TurnPlayers.Add(g.name);
        }
    }

    private void Update()
    {
        fighters[turn].GetComponent<PlayerMaster>().YourTurn();
        turndisp1.text = "Now: " + TurnPlayers[turn];
        turndisp2.text = "Next: " + TurnPlayers[(turn + 1)%TurnPlayers.Count];
        turndisp3.text = "Next: " + TurnPlayers[(turn + 2)%TurnPlayers.Count];
    }

    void OnInteract()
    {
        if(fighters[turn].GetComponent<PlayerMaster>().isai)
        {
        turn++;
        if (turn > TurnPlayers.Count -1)
            turn = 0;
        }
    }

    public void Attack(int hp)
    {
        fighters[(turn + 1)%TurnPlayers.Count].GetComponent<PlayerMaster>().hp += hp;
    }

    public void Heal(int hp)
    {
        fighters[turn].GetComponent<PlayerMaster>().hp += hp;
    }
}
