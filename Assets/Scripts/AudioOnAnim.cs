using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AnimEvent { Windup, Swing, Oof, Dizzy, Miss };
public class AudioOnAnim : MonoBehaviour
{
    public static Action<AnimEvent> audioTrigger;

    public AudioData sound;
    public AudioData dmg;
    public AudioSourceController controller;

    void Start()
    {
        controller.SetSourceOutput(sound);
    }

    public void PitchSound()
    {
        controller.PlayRandom(sound);
       // Debug.Log("Pitch Sound!");
    }

    public void TakeDmg()
    {
        controller.PlayRandom(dmg);
        //Debug.Log("Dmg Sound!");
    }
}
