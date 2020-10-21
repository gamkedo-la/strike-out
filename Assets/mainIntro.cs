using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainIntro : MonoBehaviour
{
    public GameObject cam;
    public void AnimTurnOff()
    {
        cam.GetComponent<Animator>().enabled = false;
    }
   
}
