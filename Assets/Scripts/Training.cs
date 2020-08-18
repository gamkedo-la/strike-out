using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Training : MonoBehaviour
{
    public static int levelSelect;

    public GameObject[] Levels;
    public GameObject SelectionOfLevel;

    public GameObject mainCam, TVCam;

    public static bool LevelSelectTV;

    private void Start()
    {
        levelSelect = 0;
    }

    private void Update()
    {
        if (LevelSelectTV)
        {
            TVCam.SetActive(true);
            mainCam.SetActive(false);
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
        }

        if (!LevelSelectTV)
        {
            TVCam.SetActive(false);
            mainCam.SetActive(true);
            SelectionOfLevel.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (levelSelect == 0)
            {
                SceneManager.LoadScene("Concourse");
            }

            if (levelSelect == 1)
            {
                SceneManager.LoadScene("Clubhouse");
            }

            if (levelSelect == 2)
            {
                SceneManager.LoadScene("Field");
            }
        }
    }
}
