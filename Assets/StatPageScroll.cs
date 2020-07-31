using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPageScroll : MonoBehaviour
{
    public GameObject[] Stats;
    int currentValue;

    public GameObject MenuToggle;
    bool menuActive;
    // Start is called before the first frame update
    void Start()
    {
        currentValue = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuActive = !menuActive;
        }

        if (menuActive)
        {
            MenuToggle.SetActive(true);
        }

        if(!menuActive)
        {
            MenuToggle.SetActive(false);
        }

    }

    public void IncreasePage()
    {
        currentValue++;

        if (currentValue > Stats.Length -1)
        {
            currentValue = Stats.Length - 1;
            Stats[currentValue].SetActive(false);
            currentValue = 0;
            Stats[currentValue].SetActive(true);
        }

        Stats[currentValue - 1].SetActive(false);
        Stats[currentValue].SetActive(true);
    }

    public void DecreasePage()
    {
        currentValue--;

        if (currentValue < 0)
        {
            currentValue = 0;
            Stats[currentValue].SetActive(false);
            currentValue = Stats.Length -1;
            Stats[currentValue].SetActive(true);
        }

        Stats[currentValue + 1].SetActive(false);
        Stats[currentValue].SetActive(true);
    }
}
