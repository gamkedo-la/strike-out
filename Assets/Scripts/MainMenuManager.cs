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

    public Animator mainCamAnim;
    public GameObject skip;

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
        //This is the format, use this for the future items
        GameManager.Money = PlayerPrefs.GetFloat("money", GameManager.Money);

        GameManager.StarterFast = PlayerPrefs.GetInt("StarterFast", GameManager.StarterFast);
        GameManager.StarterSlid = PlayerPrefs.GetInt("StarterSlid", GameManager.StarterSlid);
        GameManager.StarterCurve = PlayerPrefs.GetInt("StarterCurve", GameManager.StarterCurve);
        GameManager.StarterChange = PlayerPrefs.GetInt("StarterChange", GameManager.StarterChange);
        GameManager.StarterAgil = PlayerPrefs.GetInt("StarterAgil", GameManager.StarterAgil);

        GameManager.MiddleFast = PlayerPrefs.GetInt("MiddleFast", GameManager.MiddleFast);
        GameManager.MiddleSlid = PlayerPrefs.GetInt("MiddleSlid", GameManager.MiddleSlid);
        GameManager.MiddleCurve= PlayerPrefs.GetInt("MiddleCurve", GameManager.MiddleCurve);
        GameManager.MiddleChange = PlayerPrefs.GetInt("MiddleChange", GameManager.MiddleChange);
        GameManager.MiddleAgil = PlayerPrefs.GetInt("MiddleAgil", GameManager.MiddleAgil);

        GameManager.SetUpFast = PlayerPrefs.GetInt("SetUpFast", GameManager.SetUpFast);
        GameManager.SetUpSlid = PlayerPrefs.GetInt("SetUpSlid", GameManager.SetUpSlid);
        GameManager.SetUpCurve = PlayerPrefs.GetInt("SetUpCurve", GameManager.SetUpCurve);
        GameManager.SetUpChange = PlayerPrefs.GetInt("SetUpChange", GameManager.SetUpChange);
        GameManager.SetUpAgil = PlayerPrefs.GetInt("SetUpAgil", GameManager.SetUpAgil);

        GameManager.CloserFast = PlayerPrefs.GetInt("CloserFast", GameManager.CloserFast);
        GameManager.CloserSlid = PlayerPrefs.GetInt("CloserSlid", GameManager.CloserSlid);
        GameManager.CloserCurve = PlayerPrefs.GetInt("CloserCurve", GameManager.CloserCurve);
        GameManager.CloserChange = PlayerPrefs.GetInt("CloserChange", GameManager.CloserChange);
        GameManager.CloserAgil = PlayerPrefs.GetInt("CloserAgil", GameManager.CloserAgil);

        GameManager.StarterLevel = PlayerPrefs.GetInt("StarterLevel", GameManager.StarterLevel);
        GameManager.MRLevel = PlayerPrefs.GetInt("MRLevel", GameManager.MRLevel);
        GameManager.SetUpLevel = PlayerPrefs.GetInt("SetUpLevel", GameManager.SetUpLevel);
        GameManager.CloserLevel = PlayerPrefs.GetInt("CloserLevel", GameManager.CloserLevel);

        GameManager.StarterMorale = PlayerPrefs.GetFloat("StarterMorale", GameManager.StarterMorale);
        GameManager.StarterEnergy = PlayerPrefs.GetFloat("StarterEnergy", GameManager.StarterEnergy);
        GameManager.StarterMoraleMax = PlayerPrefs.GetInt("StarterMoraleMax", GameManager.StarterMoraleMax);
        GameManager.StarterEnergyMax = PlayerPrefs.GetInt("StarterEnergyMax", GameManager.StarterEnergyMax);

        GameManager.MidRelivMorale = PlayerPrefs.GetFloat("MidRelivMorale", GameManager.MidRelivMorale);
        GameManager.MidRelivEnergy = PlayerPrefs.GetFloat("MidRelivEnergy", GameManager.MidRelivEnergy);
        GameManager.MidRelivMoraleMax = PlayerPrefs.GetInt("MidRelivMoraleMax", GameManager.MidRelivMoraleMax);
        GameManager.MidRelievEnergyMax = PlayerPrefs.GetInt("MidRelievEnergyMax", GameManager.MidRelievEnergyMax);

        GameManager.SetUpMorale = PlayerPrefs.GetFloat("SetUpMorale", GameManager.SetUpMorale);
        GameManager.SetUpEnergy = PlayerPrefs.GetFloat("SetUpEnergy", GameManager.SetUpEnergy);
        GameManager.SetUpMoraleMax = PlayerPrefs.GetInt("SetUpMoraleMax", GameManager.SetUpMoraleMax);
        GameManager.SetUpEnergyMax = PlayerPrefs.GetInt("SetUpEnergyMax", GameManager.SetUpEnergyMax);

        GameManager.CloserMorale = PlayerPrefs.GetFloat("CloserMorale", GameManager.CloserMorale);
        GameManager.CloserEnergy = PlayerPrefs.GetFloat("CloserEnergy", GameManager.CloserEnergy);
        GameManager.CloserMoraleMax = PlayerPrefs.GetInt("CloserMoraleMax", GameManager.CloserMoraleMax);
        GameManager.CloserEnergyMax = PlayerPrefs.GetInt("CloserEnergyMax", GameManager.CloserEnergyMax);

        GameManager.m1v = PlayerPrefs.GetInt("Minor1Value", GameManager.m1v);
        GameManager.m2v = PlayerPrefs.GetInt("Minor2Value", GameManager.m2v);
        GameManager.m3v = PlayerPrefs.GetInt("Minor3Value", GameManager.m3v);
        GameManager.m4v = PlayerPrefs.GetInt("Minor4Value", GameManager.m4v);
        GameManager.m5v = PlayerPrefs.GetInt("Minor5Value", GameManager.m5v);
        GameManager.m6v = PlayerPrefs.GetInt("Minor6Value", GameManager.m6v);
        GameManager.m7v = PlayerPrefs.GetInt("Minor7Value", GameManager.m7v);
        GameManager.m8v = PlayerPrefs.GetInt("Minor8Value", GameManager.m8v);

        GameManager.M1v = PlayerPrefs.GetInt("Major1Value", GameManager.M1v);
        GameManager.M2v = PlayerPrefs.GetInt("Major2Value", GameManager.M2v);
        GameManager.M3v = PlayerPrefs.GetInt("Major3Value", GameManager.M3v);
        GameManager.M4v = PlayerPrefs.GetInt("Major4Value", GameManager.M4v);
        GameManager.M5v = PlayerPrefs.GetInt("Major5Value", GameManager.M5v);
        GameManager.M6v = PlayerPrefs.GetInt("Major6Value", GameManager.M6v);
        GameManager.M7v = PlayerPrefs.GetInt("Major7Value", GameManager.M7v);
        GameManager.M8v = PlayerPrefs.GetInt("Major8Value", GameManager.M8v);

        GameManager.StarterExp = PlayerPrefs.GetFloat("MidRelivMorale", GameManager.StarterExp);
        GameManager.MRExp = PlayerPrefs.GetFloat("MidRelivEnergy", GameManager.MRExp);
        GameManager.SetUpExp = PlayerPrefs.GetFloat("MidRelivMoraleMax", GameManager.SetUpExp);
        GameManager.CloserExp = PlayerPrefs.GetFloat("MidRelievEnergyMax", GameManager.CloserExp);

        GameManager.StarterTargetExp = PlayerPrefs.GetFloat("SetUpMorale", GameManager.StarterTargetExp);
        GameManager.MRTargetExp = PlayerPrefs.GetFloat("SetUpEnergy", GameManager.MRTargetExp);
        GameManager.SetupTargetExp = PlayerPrefs.GetFloat("SetUpMoraleMax", GameManager.SetupTargetExp);
        GameManager.CloserTargetExp = PlayerPrefs.GetFloat("SetUpEnergyMax", GameManager.CloserTargetExp);

        TVTurnOn.HOEUnlockedValue = PlayerPrefs.GetInt("HOEUnlockedValue", TVTurnOn.HOEUnlockedValue);

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

    public void Skip()
    {
        mainCamAnim.SetBool("hasEnded", true);
        cameraMain.GetComponent<mainIntro>().enabled = true;
        menu.SetActive(true);
        skip.SetActive(false);
        StartCoroutine(Waiting2());
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("TrainingArea");
    }

    IEnumerator Waiting2()
    {
        yield return new WaitForSeconds(.5f);
        cameraMain.GetComponent<Animator>().enabled = false;
    }
}
