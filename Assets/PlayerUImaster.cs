using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUImaster : MonoBehaviour
{
    public Slider hp;

    public Slider mana;

    public Image ammo;

    public Sprite[] ammoimages;

    public void Updateplayer(int _hp, int maxhp, Class playerclass, int _mana, int maxmana, int _ammo)
    {
        hp.maxValue = maxhp;
        hp.value = _hp;

        mana.maxValue = maxmana;
        mana.value = _mana;

        ammo.sprite = ammoimages[_ammo];

        switch (playerclass)
        {
            case Class.Mage:
            case Class.Healer:
                ammo.gameObject.SetActive(false);
                mana.gameObject.SetActive(true);
                break;
            case Class.Ranged:
                mana.gameObject.SetActive(false);
                ammo.gameObject.SetActive(true);
                break;
            case Class.Tank:
                mana.gameObject.SetActive(false);
                ammo.gameObject.SetActive(false);
                break;
        }
        }
    
}
