using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncerConvo : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        TVTurnOn.HOEUnlocked = true;
    }
}
