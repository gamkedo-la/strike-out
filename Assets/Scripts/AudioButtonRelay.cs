using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButtonRelay : MonoBehaviour
{
    
    public void ButtonRelay(string input)
    {
        AudioButtonAction.ButtonCall(input);
    }
}
