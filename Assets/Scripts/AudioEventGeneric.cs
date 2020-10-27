using UnityEngine;

public class AudioEventGeneric : MonoBehaviour
{
    public AudioData sound;
    public AudioSourceController controller;

    void Start()
    {
        if (controller == null)
            controller = gameObject.AddComponent<AudioSourceController>();

        controller.SetSourceOutput(sound);
    }

    public void PlayEvent()
    {
        controller.PlayRandom(sound);
    }
}
