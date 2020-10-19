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

    private void OnDestroy()
    {
        Destroy(this, sound.Clip.length);
    }
}
