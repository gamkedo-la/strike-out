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


    public Slider StartF, StartS, StartC, StartCh, StartA;
    public Slider MiddleF, MiddleS, MiddleC, MiddleCh, MiddleA;
    public Slider SetUpF, SetUpS, SetUpC, SetUpCh, SetUpA;
    public Slider CloserF, CloserS, CloserC, CloserCh, CloserA;
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
            MenuActive();
            print(GameManager.StarterFast);

            StartF.value = GameManager.StarterFast;
            StartS.value = GameManager.StarterSlid;
            StartC.value = GameManager.StarterCurve;
            StartCh.value = GameManager.StarterChange;
            StartA.value = GameManager.StarterAgil;

            MiddleF.value = GameManager.MiddleFast;
            MiddleS.value = GameManager.MiddleSlid ;
            MiddleC.value = GameManager.MiddleCurve;
            MiddleCh.value = GameManager.MiddleChange;
            MiddleA.value = GameManager.MiddleAgil;

            SetUpF.value = GameManager.SetUpFast;
            SetUpS.value = GameManager.SetUpSlid;
            SetUpC.value = GameManager.SetUpCurve;
            SetUpCh.value = GameManager.SetUpChange;
            SetUpA.value = GameManager.SetUpAgil;

            CloserF.value = GameManager.CloserFast;
            CloserS.value = GameManager.CloserSlid;
            CloserC.value = GameManager.CloserCurve;
            CloserCh.value = GameManager.CloserChange;
            CloserA.value = GameManager.CloserAgil;
        }

        if(!menuActive)
        {
            MenuToggle.SetActive(false);
        }
    }

    void MenuActive()
    {
        MenuToggle.SetActive(true);


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
