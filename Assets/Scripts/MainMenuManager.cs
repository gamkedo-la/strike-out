using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menu, credits;
    public Transform creditsSpot, main;
    public GameObject cameraMain;
    public GameObject shatterBall;

    public void PlayGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void Credits()
    {
        cameraMain.transform.position = creditsSpot.transform.position;
        cameraMain.transform.rotation = creditsSpot.transform.rotation;
        shatterBall.SetActive(false);
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Back()
    {
        cameraMain.transform.position = main.transform.position;
        cameraMain.transform.rotation = main.transform.rotation;
        shatterBall.SetActive(true);
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void Save()
    {

    }

    public void Load()
    {
        
    }

    public void Quit()
    {

    }
}
