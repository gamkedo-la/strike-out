using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{
    public Text itemText;
    public GameObject InventoryItem;

    GameObject MultipleSystem;

    public void SetText(string textString)
    {
        itemText.text = textString;
    }

    private void Start()
    {
        MultipleSystem = GameObject.FindGameObjectWithTag("BattleSystem");
    }

    public void OnClick()
    {
        if (itemText.text == "Sports Drink")
        {
            //Choose Between characters
            GameManager.StarterMorale += 20;
            print("Drink Consumed");
            Destroy(InventoryItem);
        }

        if (itemText.text == "Grandma's Cookies")
        {
            GameManager.StarterMorale += 20;
            GameManager.MidRelivMorale += 20;
            GameManager.SetUpMorale += 20;
            GameManager.CloserMorale += 20;
            print("Cookies Eaten");
            Destroy(InventoryItem);
        }

        if (itemText.text == "Granola Bar")
        {
            //Choose Between characters
            GameManager.StarterEnergy += 10;
            Destroy(InventoryItem);
        }

        if (itemText.text == "Sunflower Seeds")
        {
            GameManager.StarterEnergy += 10;
            GameManager.MidRelivEnergy += 10;
            GameManager.SetUpEnergy += 10;
            GameManager.CloserEnergy += 10;
            Destroy(InventoryItem);
        }

        if (itemText.text == "Film Review")
        {
            //Evasion
            Destroy(InventoryItem);
        }

        if (itemText.text == "Scouting Report")
        {
            //Choose an enemy to lower health
            MultipleSystem.GetComponent<BattleSystemMultiple>().ScoutingReportItem();
            Destroy(InventoryItem);
        }

        if (itemText.text == "Defensive Shift")
        {
            //Lower all enemy health
            MultipleSystem.GetComponent<BattleSystemMultiple>().DefensiveShiftItem();
            //Figure Out Whose Turn It Goes To
            Destroy(InventoryItem);
        }

    }
}
