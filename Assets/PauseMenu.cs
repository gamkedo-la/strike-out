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
        if (Input.GetKeyDown(KeyCode.Tab) && !isPaused)
        {
            isInventory = !isInventory;
        }
        
        if (isInventory)
        {
            InventoryMenu.transform.localPosition = new Vector3(0, 0, 0);
        }

        if (!isInventory)
        {
            InventoryMenu.transform.localPosition = new Vector3(0, -400, 0);
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

        if(!isPaused)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
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
        //Will come in later
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MainMenu");
    }
}
