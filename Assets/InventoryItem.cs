using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryItem : MonoBehaviour
{
    public string ItemName;
    public string ItemDescription;

    public int MoraleToIncrease;
    public int EnergyToIncrease;

    public InventoryItem(string thisName, string thisDescription)
    {
        ItemName = thisName;
        ItemDescription = thisDescription;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("you ran into an item");
        }
    }

}
