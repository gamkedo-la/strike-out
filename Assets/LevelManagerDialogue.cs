using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerDialogue : MonoBehaviour
{
    private Queue<string> sentences;

    public Animator anim;

    public Text nameText;
    public Text dialogueText;
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueForTraining dialogue)
    {
        PlayerMovement.canMove = false;
        anim.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        PlayerMovement.canMove = true;

        if (TVTurnOn.levelSelect == 0)
        {
            SceneManager.LoadScene("Concourse");
        }

        if (TVTurnOn.levelSelect == 1)
        {
            SceneManager.LoadScene("Clubhouse");
        }

        if (TVTurnOn.levelSelect == 2)
        {
            SceneManager.LoadScene("HallofFame");
        }
    }
}
