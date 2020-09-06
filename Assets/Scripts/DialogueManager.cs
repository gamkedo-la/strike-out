﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public Animator anim;

    public Text nameText;
    public Text dialogueText;

    public string Boss1Level, Boss2Level;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
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
        if (DialogueTrigger.GoToMiniBossFight)
        {
            anim.SetBool("isOpen", false);
            PlayerMovement.canMove = true;
            DialogueTrigger.GoToMiniBossFight = false;
            SceneManager.LoadScene(Boss1Level.ToString());
        }

        if (DialogueTrigger.GoToMajorBossFight)
        {
            anim.SetBool("isOpen", false);
            PlayerMovement.canMove = true;
            DialogueTrigger.GoToMajorBossFight = false;
            SceneManager.LoadScene(Boss2Level.ToString());
        }
        else
        {
            anim.SetBool("isOpen", false);
            PlayerMovement.canMove = true;
        }
    }
}
