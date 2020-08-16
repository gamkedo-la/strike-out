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

    public bool StaminaUp20;
    public bool StaminaUp20All;
    public bool EnergyUp10;
    public bool EnergyUp10All;
    public bool EvasionUpFor3Turns;
    public bool EnemyHealthDown20;
    public bool AllEnemyHealthDown20;

    GameObject InventoryManage;

    private void Start()
    {
        Display = GameObject.Find("ItemTextDisplay");
        TextDisplay = Display.GetComponent<Text>();

        InventoryManage = GameObject.Find("Inventory");
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
            if (StaminaUp20)
            {
                InventoryManage.GetComponent<InventoryManager>().StamUp20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (StaminaUp20All)
            {
                InventoryManage.GetComponent<InventoryManager>().StamUpAll20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EnergyUp10)
            {
                InventoryManage.GetComponent<InventoryManager>().EnUp10();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EnergyUp10All)
            {
                InventoryManage.GetComponent<InventoryManager>().EnUpAll10();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EvasionUpFor3Turns)
            {
                InventoryManage.GetComponent<InventoryManager>().EvasUp3Turns();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EnemyHealthDown20)
            {
                InventoryManage.GetComponent<InventoryManager>().EnemyHealthDown20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (AllEnemyHealthDown20)
            {
                InventoryManage.GetComponent<InventoryManager>().EnemyHealthDownAll20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2.5f);
        TextDisplay.text = "";
    }
}
