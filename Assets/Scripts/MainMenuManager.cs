using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menu, credits;
    public Transform creditsSpot, main;
    public GameObject cameraMain;
    public GameObject shatterBall;

    public Text loadText;

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
    public void Load()
    {
        PlayerPrefs.GetFloat("money", GameManager.Money);

        PlayerPrefs.GetInt("StarterFast");
        PlayerPrefs.GetInt("StarterSlid");
        PlayerPrefs.GetInt("StarterCurve");
        PlayerPrefs.GetInt("StarterChange");
        PlayerPrefs.GetInt("StarterAgil");

        PlayerPrefs.GetInt("MiddleFast");
        PlayerPrefs.GetInt("MiddleSlid");
        PlayerPrefs.GetInt("MiddleCurve");
        PlayerPrefs.GetInt("MiddleChange");
        PlayerPrefs.GetInt("MiddleAgil");

        PlayerPrefs.GetInt("SetUpFast");
        PlayerPrefs.GetInt("SetUpSlid");
        PlayerPrefs.GetInt("SetUpCurve");
        PlayerPrefs.GetInt("SetUpChange");
        PlayerPrefs.GetInt("SetUpAgil");

        PlayerPrefs.GetInt("CloserFast");
        PlayerPrefs.GetInt("CloserSlid");
        PlayerPrefs.GetInt("CloserCurve");
        PlayerPrefs.GetInt("CloserChange");
        PlayerPrefs.GetInt("CloserAgil");

        PlayerPrefs.GetInt("StarterLevel");
        PlayerPrefs.GetInt("MRLevel");
        PlayerPrefs.GetInt("SetUpLevel");
        PlayerPrefs.GetInt("CloserLevel");

        PlayerPrefs.GetFloat("StarterMorale");
        PlayerPrefs.GetFloat("StarterEnergy");
        PlayerPrefs.GetInt("StarterMoraleMax");
        PlayerPrefs.GetInt("StarterEnergyMax");

        PlayerPrefs.GetFloat("MidRelivMorale");
        PlayerPrefs.GetFloat("MidRelivEnergy");
        PlayerPrefs.GetInt("MidRelivMoraleMax");
        PlayerPrefs.GetInt("MidRelievEnergyMax");

        PlayerPrefs.GetFloat("SetUpMorale");
        PlayerPrefs.GetFloat("SetUpEnergy");
        PlayerPrefs.GetInt("SetUpMoraleMax");
        PlayerPrefs.GetInt("SetUpEnergyMax");

        PlayerPrefs.GetFloat("CloserMorale");
        PlayerPrefs.GetFloat("CloserEnergy");
        PlayerPrefs.GetInt("CloserMoraleMax");
        PlayerPrefs.GetInt("CloserEnergyMax");

        PlayerPrefs.GetFloat("Minor1Value");
        PlayerPrefs.GetFloat("Minor2Value");
        PlayerPrefs.GetFloat("Minor3Value");
        PlayerPrefs.GetFloat("Minor4Value");
        PlayerPrefs.GetFloat("Minor5Value");
        PlayerPrefs.GetFloat("Minor6Value");
        PlayerPrefs.GetFloat("Minor7Value");
        PlayerPrefs.GetFloat("Minor8Value");

        PlayerPrefs.GetFloat("Major1Value");
        PlayerPrefs.GetFloat("Major2Value");
        PlayerPrefs.GetFloat("Major3Value");
        PlayerPrefs.GetFloat("Major4Value");
        PlayerPrefs.GetFloat("Major5Value");
        PlayerPrefs.GetFloat("Major6Value");
        PlayerPrefs.GetFloat("Major7Value");
        PlayerPrefs.GetFloat("Major8Value");

        PlayerPrefs.GetFloat("MidRelivMorale");
        PlayerPrefs.GetFloat("MidRelivEnergy");
        PlayerPrefs.GetFloat("MidRelivMoraleMax");
        PlayerPrefs.GetFloat("MidRelievEnergyMax");

        PlayerPrefs.GetFloat("SetUpMorale");
        PlayerPrefs.GetFloat("SetUpEnergy");
        PlayerPrefs.GetFloat("SetUpMoraleMax");
        PlayerPrefs.GetFloat("SetUpEnergyMax");

        PlayerPrefs.GetInt("HOEUnlockedValue");

        if (GameManager.m1v == 1)
        {
            GameManager.m1 = true;
        }

        if (GameManager.m2v == 1)
        {
            GameManager.m2 = true;
        }

        if (GameManager.m3v == 1)
        {
            GameManager.m3 = true;
        }

        if (GameManager.m4v == 1)
        {
            GameManager.m4 = true;
        }

        if (GameManager.m5v == 1)
        {
            GameManager.m5 = true;
        }

        if (GameManager.m6v == 1)
        {
            GameManager.m6 = true;
        }

        if (GameManager.m7v == 1)
        {
            GameManager.m7 = true;
        }

        if (GameManager.m8v == 1)
        {
            GameManager.m8 = true;
        }


        if (GameManager.M1v == 1)
        {
            GameManager.M1 = true;
        }

        if (GameManager.M2v == 1)
        {
            GameManager.M2 = true;
        }

        if (GameManager.M3v == 1)
        {
            GameManager.M3 = true;
        }

        if (GameManager.M4v == 1)
        {
            GameManager.M4 = true;
        }

        if (GameManager.M5v == 1)
        {
            GameManager.M5 = true;
        }

        if (GameManager.M6v == 1)
        {
            GameManager.M6 = true;
        }

        if (GameManager.M7v == 1)
        {
            GameManager.M7 = true;
        }

        if (GameManager.M8v == 1)
        {
            GameManager.M8 = true;
        }

        if (TVTurnOn.HOEUnlockedValue == 1)
        {
            TVTurnOn.HOEUnlocked = true;
        }

        loadText.text = "Loading...".ToString();
        StartCoroutine(Waiting());
        //items are not currently saved
    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("TrainingArea");
    }
}
