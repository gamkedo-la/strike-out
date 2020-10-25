using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isInventory;
    bool isPaused;
    public GameObject pauseMenu;
    public GameObject InventoryMenu, StatsMenu;

    private void Start()
    {
        isPaused = false;
    }
    private void Update()
    {
        if (!BattleSystemMultiple.inBattle)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !isPaused)
            {
                isInventory = !isInventory;
            }

            if (isInventory)
            {
                InventoryMenu.transform.localPosition = new Vector3(233, 0, 0);
            }

            if (!isInventory)
            {
                InventoryMenu.transform.localPosition = new Vector3(233, -900, 0);
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isInventory = false;
                isPaused = !isPaused;
                StatsMenu.SetActive(false);
                // InventoryMenu.transform.localPosition = new Vector3(0,-400,0);
            }

            if (isPaused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }

            if (!isPaused)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        StatsMenu.SetActive(false);
        isInventory = false;
        Time.timeScale = 1f;
    }

    public void PlayerStats()
    {
        StatsMenu.SetActive(true);
        isInventory = false;
    }

    public void Inventory()
    {
        isInventory = true;
        StatsMenu.SetActive(false);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("money", GameManager.Money);

        PlayerPrefs.SetFloat("StarterFast", GameManager.StarterFast);
        PlayerPrefs.SetFloat("StarterSlid", GameManager.StarterSlid);
        PlayerPrefs.SetFloat("StarterCurve", GameManager.StarterCurve);
        PlayerPrefs.SetFloat("StarterChange", GameManager.StarterChange);
        PlayerPrefs.SetFloat("StarterAgil", GameManager.StarterAgil);

        PlayerPrefs.SetFloat("MiddleFast", GameManager.MiddleFast);
        PlayerPrefs.SetFloat("MiddleSlid", GameManager.MiddleSlid);
        PlayerPrefs.SetFloat("MiddleCurve", GameManager.MiddleCurve);
        PlayerPrefs.SetFloat("MiddleChange", GameManager.MiddleChange);
        PlayerPrefs.SetFloat("MiddleAgil", GameManager.MiddleAgil);

        PlayerPrefs.SetFloat("SetUpFast", GameManager.SetUpFast);
        PlayerPrefs.SetFloat("SetUpSlid", GameManager.SetUpSlid);
        PlayerPrefs.SetFloat("SetUpCurve", GameManager.SetUpCurve);
        PlayerPrefs.SetFloat("SetUpChange", GameManager.SetUpChange);
        PlayerPrefs.SetFloat("SetUpAgil", GameManager.SetUpAgil);

        PlayerPrefs.SetFloat("CloserFast", GameManager.CloserFast);
        PlayerPrefs.SetFloat("CloserSlid", GameManager.CloserSlid);
        PlayerPrefs.SetFloat("CloserCurve", GameManager.CloserCurve);
        PlayerPrefs.SetFloat("CloserChange", GameManager.CloserChange);
        PlayerPrefs.SetFloat("CloserAgil", GameManager.CloserAgil);

        PlayerPrefs.SetFloat("StarterLevel", GameManager.StarterLevel);
        PlayerPrefs.SetFloat("MRLevel", GameManager.MRLevel);
        PlayerPrefs.SetFloat("SetUpLevel", GameManager.SetUpLevel);
        PlayerPrefs.SetFloat("CloserLevel", GameManager.CloserLevel);

        PlayerPrefs.SetFloat("StarterMorale", GameManager.StarterMorale);
        PlayerPrefs.SetFloat("StarterEnergy", GameManager.StarterEnergy);
        PlayerPrefs.SetFloat("StarterMoraleMax", GameManager.StarterMoraleMax);
        PlayerPrefs.SetFloat("StarterEnergyMax", GameManager.StarterEnergyMax);

        PlayerPrefs.SetFloat("MidRelivMorale", GameManager.MidRelivMorale);
        PlayerPrefs.SetFloat("MidRelivEnergy", GameManager.MidRelivEnergy);
        PlayerPrefs.SetFloat("MidRelivMoraleMax", GameManager.MidRelivMoraleMax);
        PlayerPrefs.SetFloat("MidRelievEnergyMax", GameManager.MidRelievEnergyMax);

        PlayerPrefs.SetFloat("SetUpMorale", GameManager.SetUpMorale);
        PlayerPrefs.SetFloat("SetUpEnergy", GameManager.SetUpEnergy);
        PlayerPrefs.SetFloat("SetUpMoraleMax", GameManager.SetUpMoraleMax);
        PlayerPrefs.SetFloat("SetUpEnergyMax", GameManager.SetUpEnergyMax);

        PlayerPrefs.SetFloat("CloserMorale", GameManager.CloserMorale);
        PlayerPrefs.SetFloat("CloserEnergy", GameManager.CloserEnergy);
        PlayerPrefs.SetFloat("CloserMoraleMax", GameManager.CloserMoraleMax);
        PlayerPrefs.SetFloat("CloserEnergyMax", GameManager.CloserEnergyMax);

        PlayerPrefs.SetFloat("Minor1Value", GameManager.m1v);
        PlayerPrefs.SetFloat("Minor2Value", GameManager.m2v);
        PlayerPrefs.SetFloat("Minor3Value", GameManager.m3v);
        PlayerPrefs.SetFloat("Minor4Value", GameManager.m4v);
        PlayerPrefs.SetFloat("Minor5Value", GameManager.m5v);
        PlayerPrefs.SetFloat("Minor6Value", GameManager.m6v);
        PlayerPrefs.SetFloat("Minor7Value", GameManager.m7v);
        PlayerPrefs.SetFloat("Minor8Value", GameManager.m8v);

        PlayerPrefs.SetFloat("Major1Value", GameManager.M1v);
        PlayerPrefs.SetFloat("Major2Value", GameManager.M2v);
        PlayerPrefs.SetFloat("Major3Value", GameManager.M3v);
        PlayerPrefs.SetFloat("Major4Value", GameManager.M4v);
        PlayerPrefs.SetFloat("Major5Value", GameManager.M5v);
        PlayerPrefs.SetFloat("Major6Value", GameManager.M6v);
        PlayerPrefs.SetFloat("Major7Value", GameManager.M7v);
        PlayerPrefs.SetFloat("Major8Value", GameManager.M8v);

        PlayerPrefs.SetFloat("MidRelivMorale", GameManager.StarterExp);
        PlayerPrefs.SetFloat("MidRelivEnergy", GameManager.MRExp);
        PlayerPrefs.SetFloat("MidRelivMoraleMax", GameManager.SetUpExp);
        PlayerPrefs.SetFloat("MidRelievEnergyMax", GameManager.CloserExp);

        PlayerPrefs.SetFloat("SetUpMorale", GameManager.StarterTargetExp);
        PlayerPrefs.SetFloat("SetUpEnergy", GameManager.MRTargetExp);
        PlayerPrefs.SetFloat("SetUpMoraleMax", GameManager.SetupTargetExp);
        PlayerPrefs.SetFloat("SetUpEnergyMax", GameManager.CloserTargetExp);

        PlayerPrefs.SetInt("HOEUnlockedValue", TVTurnOn.HOEUnlockedValue);

        //items are not currently saved
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        isInventory = false;
        isPaused = !isPaused;
        StatsMenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
