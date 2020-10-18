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

    public bool Boss3, Boss4;
    public static bool Umpire, Babe;

    public GameObject theBabe;
    public GameObject mainCam, cutsceneCam;


    public bool isTraining ,isManager;
    public GameObject dialogue1, dialogue2;
    public AudioEventGeneric dialogueBoxAudio;
    private void Start()
    {
        if (isTraining)
        {
            dialogue1.SetActive(true);
            dialogue2.SetActive(false);
            StartCoroutine(Waiting());
        }

        if (Boss3)
        {
            if (HOEGameManager.UmpireDefeated)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        if(dialogueBoxAudio != null)
            dialogueBoxAudio.PlayEvent();
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

        if (Boss3 && isInZone)
        {
            TriggerDialogue();
            Boss3 = false;
            Umpire = true;
        }

        if (Boss4 && isInZone)
        {
            TriggerDialogue();
            Boss4 = false;
            Babe = true;
            theBabe.SetActive(true);
            mainCam.SetActive(false);
            cutsceneCam.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isManager)
            {
                dialogue1.SetActive(false);
                dialogue2.SetActive(true);
                isInZone = true;
                Exclam.SetActive(true);
            }
            else
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

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(.25f);
        TriggerDialogue();
    }
}
