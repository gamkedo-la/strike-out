using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabeDoorSwitch : MonoBehaviour
{
    public GameObject Text;

    bool inZone;

    public Animation door;
    public Animator switchToggle;
    public GameObject cornfield;

    private void Update()
    {
        if (inZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switchToggle.SetBool("isOpen", true);
                door.Play("babeDoorRotate");
                cornfield.SetActive(true);
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
