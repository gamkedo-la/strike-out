using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingZone : MonoBehaviour
{
    public bool isOnTimer;
    public bool isOnTrigger;

    float timeInLevel;
    public float toggleOnTimer, toggleOffTimer;

    public GameObject toTurnOn, toTurnOff;

    private void Update()
    {
        timeInLevel += Time.deltaTime;

        if(timeInLevel >= toggleOnTimer)
        {
            toTurnOn.SetActive(true);
        }

        if (timeInLevel >= toggleOffTimer)
        {
            toTurnOff.SetActive(false);
        }
    }
}
