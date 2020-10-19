using System.Collections;
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
    public string UmpireLevel, BabeLevel;

    //manager - training
    int randint;
    public GameObject[] trivia, advice;
    public GameObject dialogue1, dialogue2;
    public AudioData dialogueAudio;
    public AudioSourceController audioSource;

    public bool isWin;

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

        if (DialogueTrigger.Babe)
        {

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

        if(dialogueAudio != null)
            audioSource.PlayRandom(dialogueAudio);
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

        else if (DialogueTrigger.GoToMajorBossFight)
        {
            anim.SetBool("isOpen", false);
            PlayerMovement.canMove = true;
            DialogueTrigger.GoToMajorBossFight = false;
            SceneManager.LoadScene(Boss2Level.ToString());
        }

        else if (DialogueTrigger.Umpire)
        {
            anim.SetBool("isOpen", false);
            PlayerMovement.canMove = true;
            DialogueTrigger.Umpire = false;
            SceneManager.LoadScene(UmpireLevel.ToString());
        }

        else if (DialogueTrigger.Babe)
        {
            anim.SetBool("isOpen", false);
            PlayerMovement.canMove = true;
            DialogueTrigger.Babe = false;
            SceneManager.LoadScene(BabeLevel.ToString());
        }

        else if (isWin)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            anim.SetBool("isOpen", false);
            PlayerMovement.canMove = true;
        }
    }

    public void Advice()
    {
        anim.SetBool("isOpen", false);
        randint = Random.Range(0, advice.Length);
        advice[randint].SetActive(true);
        dialogue1.SetActive(true);
        dialogue2.SetActive(false);
    }

    public void Trivia()
    {
        anim.SetBool("isOpen", false);
        randint = Random.Range(0, trivia.Length);
        trivia[randint].SetActive(true);
        dialogue1.SetActive(true);
        dialogue2.SetActive(false);
    }

}
