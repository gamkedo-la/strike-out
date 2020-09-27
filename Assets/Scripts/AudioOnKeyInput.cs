using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnKeyInput : MonoBehaviour
{
    public AudioSourceController Source;
    public AudioData LRTrigger;
    public AudioData spaceTrigger;

    private void Start()
    {
        Source.SetSourceOutput(LRTrigger);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            Source.PlayRandom(LRTrigger);

        if (Input.GetKeyDown(KeyCode.Space))
            Source.PlayRandom(spaceTrigger);
    }
}
