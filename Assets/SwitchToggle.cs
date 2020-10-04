using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToggle : MonoBehaviour
{
    public bool isRed, isGreen;
    public GameObject Text;

    bool inZone;

    private void Update()
    {
        if (inZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isRed)
                {
                    HOEGameManager.redToggle = !HOEGameManager.redToggle;
                }

                if (isGreen)
                {
                    HOEGameManager.greenToggle = !HOEGameManager.greenToggle;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(false);
            inZone = false;
        }
    }
}
