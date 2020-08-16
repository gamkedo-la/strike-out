using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject buttonTemplate;

    public string Item1, Item2, Item3, Item4, Item5, Item6, Item7;

    public string Desc1, Desc2, Desc3, Desc4, Desc5, Desc6, Desc7;
    public Text Description;

    void Start()
    {
       /* for (int i = 1; i <= 20; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<InventoryItemButton>().SetText("Item #" + i);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
        */
    }

    public void StamUp20()
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<InventoryItemButton>().SetText(Item1);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

    public void StamUpAll20()
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<InventoryItemButton>().SetText(Item2);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

    public void EnUp10()
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<InventoryItemButton>().SetText(Item3);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

    public void EnUpAll10()
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<InventoryItemButton>().SetText(Item4);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

    public void EvasUp3Turns()
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<InventoryItemButton>().SetText(Item5);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

    public void EnemyHealthDown20()
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<InventoryItemButton>().SetText(Item6);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

    public void EnemyHealthDownAll20()
    {
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<InventoryItemButton>().SetText(Item7);
        button.transform.SetParent(buttonTemplate.transform.parent, false);
    }

}
