using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCameraAnimation : MonoBehaviour
{
    public void StopCamera()
    {
        this.gameObject.SetActive(false);
    }
}
