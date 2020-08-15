using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public string ItemName;
    public string ItemDescription;

    public int MoraleToIncrease;
    public int EnergyToIncrease;

    GameObject Display;
    Text TextDisplay;

    private void Start()
    {
        Display = GameObject.Find("ItemTextDisplay");
        TextDisplay = Display.GetComponent<Text>();
    }

    public InventoryItem(string thisName, string thisDescription)
    {
        ItemName = thisName;
        ItemDescription = thisDescription;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TextDisplay.text = ItemName.ToString();
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2.5f);
        TextDisplay.text = "";
    }
}
