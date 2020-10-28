using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVTurnOn : MonoBehaviour
{
    bool isActive;

    [SerializeField]
    private AudioOnKeyInput tvSound;

    public static int levelSelect;


    public GameObject[] Levels;
    public GameObject SelectionOfLevel;
    public GameObject cam;

    public DialogueForTraining dialogue1, dialogue2, dialogue3;

    public static bool HOEUnlocked;
    public static int HOEUnlockedValue;
    public Material locked, unlocked;
    public GameObject HallOfEliteIcon;

    public TextMesh TVText;

    private void Start()
    {
        Training.LevelSelectTV = false;
        cam.SetActive(false);
        levelSelect = 0;

        if (HOEUnlocked)
        {
            HallOfEliteIcon.GetComponent<Renderer>().material = unlocked;
            HOEUnlockedValue = 1;
        }
        else
        {
            HallOfEliteIcon.GetComponent<Renderer>().material = locked;
            HOEUnlockedValue = 0;
        }

        TVText.text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Training.LevelSelectTV = true;
            isActive = true;
            tvSound.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Training.LevelSelectTV = false;
            isActive = false;
            tvSound.enabled = false;
        }
    }

    private void Update()
    {
        if (isActive)
        {
            SelectionOfLevel.SetActive(true);

            SelectionOfLevel.transform.position = Levels[levelSelect].transform.position;

            if (levelSelect == 0)
            {
                TVText.text = "Concourse";
            }

            if (levelSelect == 1)
            {
                TVText.text = "Hall of Elite";
            }

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
