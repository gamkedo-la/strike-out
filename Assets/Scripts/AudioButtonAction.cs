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

            default:
                return null;

        }
    }

    //public void PlayAction()
    //{
    //    Source.PlayRandom(buttonActionType(buttonAction));
    //}


}
