using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnStart : AudioEventGeneric
{
    //public AudioData Sound;

    void Start()
    {
        if (sound != null)
        {
            controller.Play(sound);
        }
        else
            Debug.Log("No sound attached");
    }
}
