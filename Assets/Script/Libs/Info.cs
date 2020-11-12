using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Info : MonoBehaviour{
    public List<AffectorTable> Debufftable = new List<AffectorTable>();
    public GameObject ailmenttemplate;    
    public List<CurrentDebuffs> Currentailments = new List<CurrentDebuffs>();
    public GameObject parent;
    private void Start()
    {
        placedebuff(0);
        placedebuff(1);
        placedebuff(2);
    }

    public void placedebuff(int id)
    {
        Currentailments.Add(new CurrentDebuffs(null, id));
        refreshdebuffs();
    }

    public void removedebuff(int ID)
    {
        Destroy(Currentailments.Find(Buff => Buff.id == ID).gameObject);
        Currentailments.Remove(Currentailments.Find(Buff => Buff.id == ID));
        refreshdebuffs();
    }

    public void refreshdebuffs()
    {
        foreach (CurrentDebuffs d in Currentailments)
        {
            Destroy(d.gameObject);
        }
        
        foreach (CurrentDebuffs d in Currentailments)
        {
        GameObject illness = Instantiate(ailmenttemplate, this.transform);
        d.gameObject = illness;
        illness.transform.parent = parent.transform;
        illness.transform.position = parent.transform.position;
        illness.GetComponent<Image>().sprite = Debufftable[d.id].image;
        illness.name = Debufftable[d.id].name;
        illness.GetComponent<DebuffsCached>().ailname = Debufftable[d.id].name;
        illness.GetComponent<DebuffsCached>().ailinfo = Debufftable[d.id].info;
        }  
    }


}