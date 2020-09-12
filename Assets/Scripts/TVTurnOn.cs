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

    private void Start()
    {
        cam.SetActive(false);
        levelSelect = 0;
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

                    SceneManager.LoadScene("Concourse");
                    Training.LevelSelectTV = false;
                }

                if (levelSelect == 1)
                {
                    SceneManager.LoadScene("Clubhouse");
                    Training.LevelSelectTV = false;
                }

                if (levelSelect == 2)
                {
                    SceneManager.LoadScene("Field");
                    Training.LevelSelectTV = false;
                }
            }
        }
        else
        {
            SelectionOfLevel.SetActive(false);
        }
    }
}
