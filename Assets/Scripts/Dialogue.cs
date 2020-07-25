using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue
{
    public string name;

    //Text area defines the size of the box in the inspector when typing
    [TextArea(3, 10)]
    public string[] sentences;
}
