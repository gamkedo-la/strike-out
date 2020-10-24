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
    public void Load()
    {
        PlayerPrefs.GetFloat("money", GameManager.Money);

        PlayerPrefs.GetFloat("StarterFast");
        PlayerPrefs.GetFloat("StarterSlid");
        PlayerPrefs.GetFloat("StarterCurve");
        PlayerPrefs.GetFloat("StarterChange");
        PlayerPrefs.GetFloat("StarterAgil");

        PlayerPrefs.GetFloat("MiddleFast");
        PlayerPrefs.GetFloat("MiddleSlid");
        PlayerPrefs.GetFloat("MiddleCurve");
        PlayerPrefs.GetFloat("MiddleChange");
        PlayerPrefs.GetFloat("MiddleAgil");

        PlayerPrefs.GetFloat("SetUpFast");
        PlayerPrefs.GetFloat("SetUpSlid");
        PlayerPrefs.GetFloat("SetUpCurve");
        PlayerPrefs.GetFloat("SetUpChange");
        PlayerPrefs.GetFloat("SetUpAgil");

        PlayerPrefs.GetFloat("CloserFast");
        PlayerPrefs.GetFloat("CloserSlid");
        PlayerPrefs.GetFloat("CloserCurve");
        PlayerPrefs.GetFloat("CloserChange");
        PlayerPrefs.GetFloat("CloserAgil");

        PlayerPrefs.GetFloat("StarterLevel");
        PlayerPrefs.GetFloat("MRLevel");
        PlayerPrefs.GetFloat("SetUpLevel");
        PlayerPrefs.GetFloat("CloserLevel");

        PlayerPrefs.GetFloat("StarterMorale");
        PlayerPrefs.GetFloat("StarterEnergy");
        PlayerPrefs.GetFloat("StarterMoraleMax");
        PlayerPrefs.GetFloat("StarterEnergyMax");

        PlayerPrefs.GetFloat("MidRelivMorale");
        PlayerPrefs.GetFloat("MidRelivEnergy");
        PlayerPrefs.GetFloat("MidRelivMoraleMax");
        PlayerPrefs.GetFloat("MidRelievEnergyMax");

        PlayerPrefs.GetFloat("SetUpMorale");
        PlayerPrefs.GetFloat("SetUpEnergy");
        PlayerPrefs.GetFloat("SetUpMoraleMax");
        PlayerPrefs.GetFloat("SetUpEnergyMax");

        PlayerPrefs.GetFloat("CloserMorale");
        PlayerPrefs.GetFloat("CloserEnergy");
        PlayerPrefs.GetFloat("CloserMoraleMax");
        PlayerPrefs.GetFloat("CloserEnergyMax");

        //convert this to the bool once it is loaded
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

        //items are not currently saved
    }

    public void Quit()
    {

    }
}
