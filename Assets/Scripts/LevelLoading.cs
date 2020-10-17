using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoading : MonoBehaviour
{
    public bool turnOn, turnOff;
    public bool plaques, umpire, display, cornfield;

    public GameObject plaqueRoom, umpireRoom, displayRoom, cornfieldRoom;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (turnOn)
            {
                if (plaques)
                {
                    plaqueRoom.SetActive(true);
                }
                if (umpireRoom)
                {
                    umpireRoom.SetActive(true);
                }
                if (display)
                {
                    displayRoom.SetActive(true);
                }
                if (cornfield)
                {
                    cornfieldRoom.SetActive(true);
                }
            }

            if (turnOff)
            {
                if (plaques)
                {
                    Destroy(plaqueRoom);
                }
                if (umpireRoom)
                {
                    Destroy(umpireRoom);
                }
                if (display)
                {
                    Destroy(displayRoom);
                }
                if (cornfield)
                {
                    Destroy(cornfieldRoom);
                }
            }
        }
        Destroy(this.gameObject);
    }
}
