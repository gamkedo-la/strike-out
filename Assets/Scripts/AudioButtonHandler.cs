using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioButtonType { Hover, Click, Error, Text, Dialogue }

[CreateAssetMenu()]
public class AudioButtonHandler : ScriptableObject
{
    public AudioData ButtonHover;
    public AudioData ButtonClick;
    public AudioData ButtonError;
    public AudioData TextButton;
    public AudioData TextDialogue;
    public AudioData TextBoxAppear;
    public AudioData LeverOn;
    public AudioData LeverOff;
    public AudioData GateOpen;
    public AudioData GateClose;
}
