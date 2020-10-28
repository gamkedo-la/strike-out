using UnityEngine;

public class AudioEventGeneric : MonoBehaviour
{
    public AudioData sound;
    public AudioSourceController controller;
    public bool playOnce;
    private bool hasPlayed;

    void Start()
    {
        if (controller == null)
            controller = gameObject.AddComponent<AudioSourceController>();

        controller.SetSourceOutput(sound);
    }

    public void PlayEvent()
    {
        if (!hasPlayed)
            controller.PlayRandom(sound);

        if (playOnce)
            hasPlayed = true;
    }
}
