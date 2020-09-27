using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public int price;
    GameObject InventoryManage;

    private void Start()
    {
        InventoryManage = GameObject.Find("Inventory");
    }
    public void SportsDrink()
    {
        if (price <= GameManager.Money)
        {
            GameManager.Money -= price;
            InventoryManage.GetComponent<InventoryManager>().StamUp20();
        }
    }

    public void Grandma()
    {
        if (price <= GameManager.Money)
        {
            GameManager.Money -= price;
            InventoryManage.GetComponent<InventoryManager>().StamUpAll20();
        }
    }

    public void Granola()
    {
        if (price <= GameManager.Money)
        {
            GameManager.Money -= price;
            InventoryManage.GetComponent<InventoryManager>().EnUp10();
        }
    }

    public void Sunflower()
    {
        if (price <= GameManager.Money)
        {
            GameManager.Money -= price;
            InventoryManage.GetComponent<InventoryManager>().EnUpAll10();
        }
    }

    public void Scouting()
    {
        if (price <= GameManager.Money)
        {
            GameManager.Money -= price;
            InventoryManage.GetComponent<InventoryManager>().EnemyHealthDown20();
        }
    }
    public void Defensive()
    {
        if (price <= GameManager.Money)
        {
            GameManager.Money -= price;
            InventoryManage.GetComponent<InventoryManager>().EnemyHealthDownAll20();
        }
    }
}
