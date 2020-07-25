using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject Exclam;
    bool isInZone;
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void Update()
    {
        if (isInZone && Input.GetKey(KeyCode.Space))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInZone = true;
            Exclam.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInZone = false;
            Exclam.SetActive(false);
        }
    }
}
