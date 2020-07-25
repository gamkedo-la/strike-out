using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerOfPlayer : MonoBehaviour
{
    public GameObject Exclam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Exclam.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Exclam.SetActive(false);
        }
    }
}
