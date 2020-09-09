using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject mainCam, shopCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shopMenu.SetActive(true);
            shopCam.SetActive(true);
            mainCam.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shopMenu.SetActive(false);
            shopCam.SetActive(false);
            mainCam.SetActive(true);
        }
    }
}
