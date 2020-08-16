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

            if (GameManager.StarterMorale > GameManager.StarterMoraleMax)
            {
                GameManager.StarterMorale = GameManager.StarterMoraleMax;
            }
            print("Drink Consumed");
            
            Destroy(InventoryItem);
        }

        if (itemText.text == "Grandma's Cookies")
        {
            GameManager.StarterMorale += 20;
            GameManager.MidRelivMorale += 20;
            GameManager.SetUpMorale += 20;
            GameManager.CloserMorale += 20;

            if (GameManager.StarterMorale > GameManager.StarterMoraleMax)
            {
                GameManager.StarterMorale = GameManager.StarterMoraleMax;
            }
            if (GameManager.MidRelivMorale > GameManager.MidRelivMoraleMax)
            {
                GameManager.MidRelivMorale = GameManager.MidRelivMoraleMax;
            }
            if (GameManager.SetUpMorale > GameManager.SetUpMoraleMax)
            {
                GameManager.SetUpMorale = GameManager.SetUpMoraleMax;
            }
            if (GameManager.CloserMorale > GameManager.CloserMoraleMax)
            {
                GameManager.CloserMorale = GameManager.CloserMoraleMax;
            }
            print("Cookies Eaten");
            Destroy(InventoryItem);
        }

        if (itemText.text == "Granola Bar")
        {
            //Choose Between characters
            GameManager.StarterEnergy += 10;

            if (GameManager.StarterEnergy > GameManager.StarterEnergyMax)
            {
                GameManager.StarterEnergy = GameManager.StarterEnergyMax;
            }
            Destroy(InventoryItem);
        }

        if (itemText.text == "Sunflower Seeds")
        {
            GameManager.StarterEnergy += 10;
            GameManager.MidRelivEnergy += 10;
            GameManager.SetUpEnergy += 10;
            GameManager.CloserEnergy += 10;

            if (GameManager.StarterEnergy > GameManager.StarterEnergyMax)
            {
                GameManager.StarterEnergy = GameManager.StarterEnergyMax;
            }

            if (GameManager.MidRelivEnergy > GameManager.MidRelievEnergyMax)
            {
                GameManager.MidRelivEnergy = GameManager.MidRelievEnergyMax;
            }

            if (GameManager.SetUpEnergy > GameManager.SetUpEnergyMax)
            {
                GameManager.SetUpEnergy = GameManager.SetUpEnergyMax;
            }

            if (GameManager.CloserEnergy > GameManager.CloserEnergyMax)
            {
                GameManager.CloserEnergy = GameManager.CloserEnergyMax;
            }
            Destroy(InventoryItem);
        }

        if (itemText.text == "Film Review")
        {
            //Evasion
            Destroy(InventoryItem);
        }

        if (itemText.text == "Scouting Report")
        {
            try
            {
                //Choose an enemy to lower health
                MultipleSystem.GetComponent<BattleSystemMultiple>().ScoutingReportItem();
                Destroy(InventoryItem);
            }
            catch
            {
                print("Only used in battle");
                return;
            }
        }

        if (itemText.text == "Defensive Shift")
        {
            //Lower all enemy health
            MultipleSystem.GetComponent<BattleSystemMultiple>().DefensiveShiftItem();
            //Figure Out Whose Turn It Goes To
            Destroy(InventoryItem);

            try
            {
                
            }
            catch
            {
                print("Only used in battle");
                return;
            }
        }
        try
        {
            print("Here");
            MultipleSystem.GetComponent<BattleSystemMultiple>().AdvanceTurn();
        }
        catch
        {
            return;
        }
    }
}
