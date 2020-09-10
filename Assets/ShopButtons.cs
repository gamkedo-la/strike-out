using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public GameObject scoutingMenu, minorMenu, majorMenu, itemMenu;

    public void Items()
    {
        scoutingMenu.SetActive(false);
        itemMenu.SetActive(true);
    }

    public void ScoutingReport()
    {
        scoutingMenu.SetActive(true);
        minorMenu.SetActive(true);
        majorMenu.SetActive(true);
        itemMenu.SetActive(false);
    }

    public void Minor()
    {
        minorMenu.SetActive(true);
        majorMenu.SetActive(false);
    }

    public void Major()
    {
        minorMenu.SetActive(false);
        majorMenu.SetActive(true);
    }
}
