using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public GameObject scoutingMenu, minorMenu, majorMenu;

    public void Items()
    {
        scoutingMenu.SetActive(false);
    }

    public void ScoutingReport()
    {
        scoutingMenu.SetActive(true);
        minorMenu.SetActive(true);
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
