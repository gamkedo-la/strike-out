using UnityEngine;

public class AudioOnStart : AudioEventGeneric
{
    //public AudioData Sound;

    void Start()
    {
        if (controller == null)
            controller = gameObject.AddComponent<AudioSourceController>();

        if (sound != null)
        {
            controller.PlayRandom(sound);
        }
        else
            Debug.Log("No sound attached");
    }
}
