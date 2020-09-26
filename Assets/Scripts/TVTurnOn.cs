using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVTurnOn : MonoBehaviour
{
    bool isActive;
    public static int levelSelect;


    public GameObject[] Levels;
    public GameObject SelectionOfLevel;
    public GameObject cam;

    public DialogueForTraining dialogue1, dialogue2, dialogue3;

    public static bool HOEUnlocked;
    public Material locked, unlocked;
    public GameObject HallOfEliteIcon;

    private void Start()
    {
        cam.SetActive(false);
        levelSelect = 0;

        if (HOEUnlocked)
        {
            HallOfEliteIcon.GetComponent<Renderer>().material = unlocked;
        }
        else
        {
            HallOfEliteIcon.GetComponent<Renderer>().material = locked;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Training.LevelSelectTV = true;
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Training.LevelSelectTV = false;
            isActive = false;
        }
    }

    private void Update()
    {
        if (isActive)
        {
            SelectionOfLevel.SetActive(true);

            SelectionOfLevel.transform.position = Levels[levelSelect].transform.position;

            if (Input.GetKeyDown(KeyCode.A))
            {
                print(levelSelect);
                if (levelSelect >= 0)
                {
                    levelSelect--;
                }
                if (levelSelect < 0)
                {
                    levelSelect = Levels.Length - 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.D) && levelSelect < Levels.Length)
            {
                if (levelSelect < Levels.Length)
                {
                    levelSelect++;
                }
                if (levelSelect >= Levels.Length)
                {
                    levelSelect = 0;
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (levelSelect == 0)
                {
                    FindObjectOfType<LevelManagerDialogue>().StartDialogue(dialogue1);
                    //SceneManager.LoadScene("Concourse");
                    //Training.LevelSelectTV = false;
                }

                if (levelSelect == 1 && HOEUnlocked)
                {
                    FindObjectOfType<LevelManagerDialogue>().StartDialogue(dialogue3);
                    //SceneManager.LoadScene("Concourse");
                    //Training.LevelSelectTV = false;
                }
            }
        }
        else
        {
            SelectionOfLevel.SetActive(false);
        }
    }
}
