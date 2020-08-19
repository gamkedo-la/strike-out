using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{
    public AudioData sound;
    public AudioSource source;
    public bool PlayOnce;

    private void OnTriggerEnter(Collider other)
    {
        //if (sound != null)
        //{
        //    if (!PlayOnce)
        //        sound.Play(t);

        //    PlayOnce = true;
        //}
        //else
        //    Debug.LogWarning("AudioOnTrigger Does Not Have Sound Assigned");

        source.clip = sound.Sounds[0];
        source.Play();
    }
}
