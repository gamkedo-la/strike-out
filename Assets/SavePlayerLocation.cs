using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerLocation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerLocationDontDestroy.isStarting = false;
        }
    }
}
