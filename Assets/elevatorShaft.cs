using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorShaft : MonoBehaviour
{
    public GameObject movePlatform;

    private void OnTriggerStay()
    {
        movePlatform.transform.position += movePlatform.transform.up * Time.deltaTime * 1.5f;
    }
}
