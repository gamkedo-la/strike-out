using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{
    public AudioData sound;
    public AudioSourceController controller;
    public bool PlayOnce;
    public bool TriggerReset;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && sound != null)
        {
            if (TriggerReset)
            {
               PlayOnce = false;
            }
        }
    }
}
