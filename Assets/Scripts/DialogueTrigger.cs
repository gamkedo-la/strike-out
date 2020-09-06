using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject Exclam;
    bool isInZone;
    public Dialogue dialogue;

    public bool Boss1, Boss2;
    public static bool GoToMiniBossFight, GoToMajorBossFight;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void Update()
    {
        if (!Boss1 && !Boss2 && isInZone && Input.GetKey(KeyCode.Space))
        {
            TriggerDialogue();
        }

        if (Boss1 && isInZone)
        {
            TriggerDialogue();
            Boss1 = false;
            GoToMiniBossFight = true;
        }

        if (Boss2 && isInZone)
        {
            TriggerDialogue();
            Boss2 = false;
            GoToMajorBossFight = true;
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
