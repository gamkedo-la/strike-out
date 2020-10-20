using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{
    public Text itemText;
    public GameObject InventoryItem;

    GameObject MultipleSystem;
    GameObject GameManagerObject;

    public void SetText(string textString)
    {
        itemText.text = textString;
    }

    private void Start()
    {
        GameManagerObject = GameObject.Find("GameManager");
    }

    private void Update()
    {
        MultipleSystem = GameObject.FindGameObjectWithTag("BattleSystem");
    }

    public void OnClick()
    {
        if (itemText.text == "Sports Drink")
        {
            //Choose Between characters
            if (MultipleSystem.GetComponent<BattleSystemMultiple>() == null)
            {
                //Figure Out How To Determine Which Player Gets a Health Up
            }

            if (MultipleSystem.GetComponent<BattleSystemMultiple>() != null)
            {
                MultipleSystem.GetComponent<BattleSystemMultiple>().SportsDrink();
            }
            print("Drink Consumed");
            
            Destroy(InventoryItem);
        }

        if (itemText.text == "Grandma's Cookies")
        {
            if (MultipleSystem.GetComponent<BattleSystemMultiple>() != null)
            {
                GameManagerObject.GetComponent<GameManager>().HealthUpAll(20);
                print("Cookies Eaten");
                Destroy(InventoryItem);
            }

            if (MultipleSystem.GetComponent<BattleSystemMultiple>() != null)
            {
                GameManagerObject.GetComponent<GameManager>().HealthUpAll(20);
                MultipleSystem.GetComponent<BattleSystemMultiple>().AdvanceTurn();
                print("Cookies Eaten");
                Destroy(InventoryItem);
            }

        }

        if (itemText.text == "Granola Bar")
        {
            //Choose Between characters
            if (MultipleSystem.GetComponent<BattleSystemMultiple>() == null)
            {
                //Figure Out How To Determine Which Player Gets an Energy Up
            }

            if (MultipleSystem.GetComponent<BattleSystemMultiple>() != null)
            {
                MultipleSystem.GetComponent<BattleSystemMultiple>().GranolaBar();
            }
            print("Drink Consumed");

            Destroy(InventoryItem);
        }

        if (itemText.text == "Sunflower Seeds")
        {
            if (MultipleSystem.GetComponent<BattleSystemMultiple>() == null)
            {
                GameManagerObject.GetComponent<GameManager>().EnergyUpAll(15);
            }

            if (MultipleSystem.GetComponent<BattleSystemMultiple>() != null)
            {
                GameManagerObject.GetComponent<GameManager>().EnergyUpAll(15);
                MultipleSystem.GetComponent<BattleSystemMultiple>().AdvanceTurn();
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
            if (MultipleSystem.GetComponent<BattleSystemMultiple>() == null)
            {
                Debug.Log("Only used in battle");
                return;
            }

            if (MultipleSystem.GetComponent<BattleSystemMultiple>() != null)
            {
                MultipleSystem.GetComponent<BattleSystemMultiple>().ScoutingReportItem();
            }

            Destroy(InventoryItem);
        }

        if (itemText.text == "Defensive Shift")
        {
            if (MultipleSystem.GetComponent<BattleSystemMultiple>() == null)
            {
                Debug.Log("Only used in battle");
                return;
            }

            if (MultipleSystem.GetComponent<BattleSystemMultiple>() != null)
            {
                MultipleSystem.GetComponent<BattleSystemMultiple>().DefensiveShiftItem();
            }
            //Figure Out Whose Turn It Goes To
            Destroy(InventoryItem);
        }
    }
}
