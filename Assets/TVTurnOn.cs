using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVTurnOn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Training.LevelSelectTV = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Training.LevelSelectTV = false;
        }
    }
}
