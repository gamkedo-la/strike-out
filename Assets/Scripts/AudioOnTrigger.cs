using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{
    public AudioData sound;
    public AudioSourceController controller;
    public bool PlayOnce;

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
