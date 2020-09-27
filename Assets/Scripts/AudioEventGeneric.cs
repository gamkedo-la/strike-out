using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventGeneric : MonoBehaviour
{
    public AudioData sound;
    public AudioSourceController controller;

    void Start()
    {
        controller.SetSourceOutput(sound);
    }

    public void PlayEvent()
    {
        controller.PlayRandom(sound);
    }
}
