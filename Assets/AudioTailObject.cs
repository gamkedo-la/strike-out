using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTailObject : MonoBehaviour
{
    public AudioSourceController controller;
    public AudioData sound;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);   
    }

    public void PlaySoundWithTail()
    {
        controller.PlayRandom(sound);
        Destroy(this.gameObject, sound.Clip.length);
    }
}
