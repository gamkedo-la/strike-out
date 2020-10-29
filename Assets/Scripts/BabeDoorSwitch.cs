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
                AudioButtonAction.ButtonCall("LeverOn");
                var doorSound = GetComponent<AudioEventGeneric>();
                doorSound.controller.PlayRandom(doorSound.sound);
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
