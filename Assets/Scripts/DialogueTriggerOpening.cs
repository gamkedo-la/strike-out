using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerOpening : MonoBehaviour
{
    public Dialogue dialogue;
    public AudioEventGeneric dialogueBoxAudio;

    private void Start()
    {
        StartCoroutine(Waiting());
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagerOpening>().StartDialogue(dialogue);

        if (dialogueBoxAudio != null)
            dialogueBoxAudio.PlayEvent();
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(.25f);
        TriggerDialogue();
    }
}
