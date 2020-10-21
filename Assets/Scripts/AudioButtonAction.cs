using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AudioButtonAction : MonoBehaviour
{
    public static Action<string> ButtonCall;
    public AudioButtonHandler data;
    public AudioSourceController Source;
    //public string buttonAction;

    void Start()
    {
        ButtonCall += ButtonListener;
    }

    public void ButtonListener(string callback)
    {
        //buttonAction = callback;
        Source.PlayRandom(buttonActionType(callback));
    }

    AudioData buttonActionType(string action)
    {
        switch(action)
        {
            case "Hover":
                return data.ButtonHover;

            case "Click":
                return data.ButtonClick;
 
            case "Error":
                return data.ButtonError;

            case "Text":
                return data.TextButton;

            case "Dialogue":
                return data.TextDialogue;

            case "LeverOn":
                return data.LeverOn;

            case "LeverOff":
                return data.LeverOff;

            case "GateOpen":
                return data.GateOpen;

            case "GateClose":
                return data.GateClose;

            default:
                return null;

        }
    }

    //public void PlayAction()
    //{
    //    Source.PlayRandom(buttonActionType(buttonAction));
    //}

    private void OnDestroy()
    {
        ButtonCall -= ButtonListener;
    }
}
