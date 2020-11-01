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
                InventoryMenu.transform.localPosition = new Vector3(233, -900, -5000);
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

        PlayerPrefs.SetInt("StarterFast", GameManager.StarterFast);
        PlayerPrefs.SetInt("StarterSlid", GameManager.StarterSlid);
        PlayerPrefs.SetInt("StarterCurve", GameManager.StarterCurve);
        PlayerPrefs.SetInt("StarterChange", GameManager.StarterChange);
        PlayerPrefs.SetInt("StarterAgil", GameManager.StarterAgil);

        PlayerPrefs.SetInt("MiddleFast", GameManager.MiddleFast);
        PlayerPrefs.SetInt("MiddleSlid", GameManager.MiddleSlid);
        PlayerPrefs.SetInt("MiddleCurve", GameManager.MiddleCurve);
        PlayerPrefs.SetInt("MiddleChange", GameManager.MiddleChange);
        PlayerPrefs.SetInt("MiddleAgil", GameManager.MiddleAgil);

        PlayerPrefs.SetInt("SetUpFast", GameManager.SetUpFast);
        PlayerPrefs.SetInt("SetUpSlid", GameManager.SetUpSlid);
        PlayerPrefs.SetInt("SetUpCurve", GameManager.SetUpCurve);
        PlayerPrefs.SetInt("SetUpChange", GameManager.SetUpChange);
        PlayerPrefs.SetInt("SetUpAgil", GameManager.SetUpAgil);

        PlayerPrefs.SetInt("CloserFast", GameManager.CloserFast);
        PlayerPrefs.SetInt("CloserSlid", GameManager.CloserSlid);
        PlayerPrefs.SetInt("CloserCurve", GameManager.CloserCurve);
        PlayerPrefs.SetInt("CloserChange", GameManager.CloserChange);
        PlayerPrefs.SetInt("CloserAgil", GameManager.CloserAgil);

        PlayerPrefs.SetInt("StarterLevel", GameManager.StarterLevel);
        PlayerPrefs.SetInt("MRLevel", GameManager.MRLevel);
        PlayerPrefs.SetInt("SetUpLevel", GameManager.SetUpLevel);
        PlayerPrefs.SetInt("CloserLevel", GameManager.CloserLevel);

        PlayerPrefs.SetFloat("StarterMorale", GameManager.StarterMorale);
        PlayerPrefs.SetFloat("StarterEnergy", GameManager.StarterEnergy);
        PlayerPrefs.SetInt("StarterMoraleMax", GameManager.StarterMoraleMax);
        PlayerPrefs.SetInt("StarterEnergyMax", GameManager.StarterEnergyMax);

        PlayerPrefs.SetFloat("MidRelivMorale", GameManager.MidRelivMorale);
        PlayerPrefs.SetFloat("MidRelivEnergy", GameManager.MidRelivEnergy);
        PlayerPrefs.SetInt("MidRelivMoraleMax", GameManager.MidRelivMoraleMax);
        PlayerPrefs.SetInt("MidRelievEnergyMax", GameManager.MidRelievEnergyMax);

        PlayerPrefs.SetFloat("SetUpMorale", GameManager.SetUpMorale);
        PlayerPrefs.SetFloat("SetUpEnergy", GameManager.SetUpEnergy);
        PlayerPrefs.SetInt("SetUpMoraleMax", GameManager.SetUpMoraleMax);
        PlayerPrefs.SetInt("SetUpEnergyMax", GameManager.SetUpEnergyMax);

        PlayerPrefs.SetFloat("CloserMorale", GameManager.CloserMorale);
        PlayerPrefs.SetFloat("CloserEnergy", GameManager.CloserEnergy);
        PlayerPrefs.SetInt("CloserMoraleMax", GameManager.CloserMoraleMax);
        PlayerPrefs.SetInt("CloserEnergyMax", GameManager.CloserEnergyMax);

        PlayerPrefs.SetInt("Minor1Value", GameManager.m1v);
        PlayerPrefs.SetInt("Minor2Value", GameManager.m2v);
        PlayerPrefs.SetInt("Minor3Value", GameManager.m3v);
        PlayerPrefs.SetInt("Minor4Value", GameManager.m4v);
        PlayerPrefs.SetInt("Minor5Value", GameManager.m5v);
        PlayerPrefs.SetInt("Minor6Value", GameManager.m6v);
        PlayerPrefs.SetInt("Minor7Value", GameManager.m7v);
        PlayerPrefs.SetInt("Minor8Value", GameManager.m8v);

        PlayerPrefs.SetInt("Major1Value", GameManager.M1v);
        PlayerPrefs.SetInt("Major2Value", GameManager.M2v);
        PlayerPrefs.SetInt("Major3Value", GameManager.M3v);
        PlayerPrefs.SetInt("Major4Value", GameManager.M4v);
        PlayerPrefs.SetInt("Major5Value", GameManager.M5v);
        PlayerPrefs.SetInt("Major6Value", GameManager.M6v);
        PlayerPrefs.SetInt("Major7Value", GameManager.M7v);
        PlayerPrefs.SetInt("Major8Value", GameManager.M8v);

        PlayerPrefs.SetFloat("MidRelivMorale", GameManager.StarterExp);
        PlayerPrefs.SetFloat("MidRelivEnergy", GameManager.MRExp);
        PlayerPrefs.SetFloat("MidRelivMoraleMax", GameManager.SetUpExp);
        PlayerPrefs.SetFloat("MidRelievEnergyMax", GameManager.CloserExp);

        PlayerPrefs.SetFloat("SetUpMorale", GameManager.StarterTargetExp);
        PlayerPrefs.SetFloat("SetUpEnergy", GameManager.MRTargetExp);
        PlayerPrefs.SetFloat("SetUpMoraleMax", GameManager.SetupTargetExp);
        PlayerPrefs.SetFloat("SetUpEnergyMax", GameManager.CloserTargetExp);

        PlayerPrefs.SetInt("HOEUnlockedValue", TVTurnOn.HOEUnlockedValue);

        PlayerPrefs.Save();

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
