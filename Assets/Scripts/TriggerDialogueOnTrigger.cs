using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogueOnTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject TurnOnButtons;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerDialogue();
            StartCoroutine(Waiting());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerDialogue();
            TurnOnButtons.SetActive(true);
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
        TurnOnButtons.SetActive(true);
    }

    IEnumerator ColliderOff()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    public void ReturnToTraining()
    {
        SceneManager.LoadScene("TrainingArea");
    }

    public void StayInLevel()
    {
        TurnOnButtons.SetActive(false);
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(ColliderOff());
    }
}
