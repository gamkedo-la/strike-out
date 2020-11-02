using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainIntro : MonoBehaviour
{
    public GameObject cam;
    public GameObject skip;
    public GameObject music;
    public void AnimTurnOff()
    {
        cam.GetComponent<Animator>().enabled = false;
        skip.SetActive(false);
        music.SetActive(true);
    }
   
}
