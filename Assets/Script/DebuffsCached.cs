using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebuffsCached : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infobox;
    public string ailname;
    public string ailinfo;
    public TextMeshProUGUI name;
    public TextMeshProUGUI info;
    // Update is called once per frame
    void Update()
    {
        name.text = ailname;
        info.text = ailinfo;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse is over GameObject.");
        infobox.SetActive(true);
        infobox.transform.SetAsFirstSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infobox.SetActive(false);
        infobox.transform.SetParent(this.transform, true);
    }
}
