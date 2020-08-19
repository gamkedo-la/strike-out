using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{
    public AudioData sound;
    public AudioSourceController controller;
    public bool PlayOnce;

    void Start()
    {
        controller.SetSourceOutput(sound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && sound != null)
        {
            if (!PlayOnce)
            {
                controller.PlayRandom(sound);

                PlayOnce = true;
            }
        }
    }
}
