using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToggle : MonoBehaviour
{
    public bool isRed, isGreen;
    public GameObject Text;

    bool inZone;

    public Animator switchModel;

    private void Update()
    {
        if (inZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isRed)
                {

                    HOEGameManager.redToggle = !HOEGameManager.redToggle;

                    if (HOEGameManager.redToggle)
                    {
                        switchModel.SetBool("isOpen", true);

                        AudioButtonAction.ButtonCall("LeverOn"); // Switch On Audio

                        print("yes");
                    }

                    if(!HOEGameManager.redToggle)
                    {
                        switchModel.SetBool("isOpen", false);
                        AudioButtonAction.ButtonCall("LeverOff"); // Switch On Audio

                        print("no");
                    }
                }

                if (isGreen)
                {
                    HOEGameManager.greenToggle = !HOEGameManager.greenToggle;

                    if (HOEGameManager.greenToggle)
                    {
                        switchModel.SetBool("isOpen", true);
                        AudioButtonAction.ButtonCall("LeverOn"); // Switch On Audio
                    }

                    if (!HOEGameManager.greenToggle)
                    {
                        switchModel.SetBool("isOpen", false);
                        AudioButtonAction.ButtonCall("LeverOff"); // Switch On Audio
                    }
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
