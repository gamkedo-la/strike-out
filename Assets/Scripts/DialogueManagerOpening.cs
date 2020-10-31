using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManagerOpening : MonoBehaviour
{
    private Queue<string> sentences;


    public Animator anim;

    public Text nameText;
    public Text dialogueText;

    public GameObject camera;
    public Transform[] camLocations;
    int camTransformLocations;
    public GameObject[] nextRoundofText;

    //manager - training
    int randint;
    public AudioData dialogueAudio;
    public AudioSourceController audioSource;

    public Material set1, set2, set3, set4;

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

    private void Update()
    {
        if (camTransformLocations == 0)
        {
            RenderSettings.skybox = set1;
        }

        if (camTransformLocations == 1)
        {
            RenderSettings.skybox = set2;
        }

        if (camTransformLocations == 2)
        {
            RenderSettings.skybox = set3;
        }

        if (camTransformLocations == 3)
        {
            RenderSettings.skybox = set4;
        }
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

        if (dialogueAudio != null)
            audioSource.PlayRandom(dialogueAudio);
    }

    void EndDialogue()
    {
        camTransformLocations++;
        if (camTransformLocations > nextRoundofText.Length -1)
        {
            SceneManager.LoadScene("TrainingArea");
        }

        else {

            camera.transform.position = camLocations[camTransformLocations].transform.position;
            camera.transform.rotation = camLocations[camTransformLocations].transform.rotation;
            nextRoundofText[camTransformLocations - 1].SetActive(false);
            nextRoundofText[camTransformLocations].SetActive(true);
            anim.SetBool("isOpen", false);
        }
    }
}
