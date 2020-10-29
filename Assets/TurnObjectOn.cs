using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObjectOn : MonoBehaviour
{
    public GameObject toTurnOn;
    // Start is called before the first frame update
    void Start()
    {
        toTurnOn.SetActive(true);
    }
}
