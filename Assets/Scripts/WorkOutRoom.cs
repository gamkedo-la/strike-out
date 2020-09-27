using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkOutRoom : MonoBehaviour
{
    public GameObject S, M, Se, C, I;
    public Slider SF, SS, SC, SCh, SA;
    public Slider MF, MS, MC, MCh, MA;
    public Slider SeF, SeS, SeC, SeCh, SeA;
    public Slider CF, CS, CC, CCh, CA;

    public Text Money;

    bool StartB, MiddleB, SetUpB, CloserB;

    public int costPerIncrease;
    public void Starter()
    {
        StartB = true;
        MiddleB = false;
        SetUpB = false;
        CloserB = false;

        I.SetActive(true);
        S.SetActive(true);
        M.SetActive(false);
        Se.SetActive(false);
        C.SetActive(false);
    }

    public void Middle()
    {
        StartB = false;
        MiddleB = true;
        SetUpB = false;
        CloserB = false;

        I.SetActive(true);
        S.SetActive(false);
        M.SetActive(true);
        Se.SetActive(false);
        C.SetActive(false);
    }

    public void SetUp()
    {
        StartB = false;
        MiddleB = false;
        SetUpB = true;
        CloserB = false;

        I.SetActive(true);
        S.SetActive(false);
        M.SetActive(false);
        Se.SetActive(true);
        C.SetActive(false);
    }

    public void Closer()
    {
        StartB = false;
        MiddleB = false;
        SetUpB = false;
        CloserB = true;

        I.SetActive(true);
        S.SetActive(false);
        M.SetActive(false);
        Se.SetActive(false);
        C.SetActive(true);
    }

    private void Update()
    {
        Money.text = GameManager.Money.ToString();
        if (StartB)
        {
            SF.value = GameManager.StarterFast;
            SS.value = GameManager.StarterSlid;
            SC.value = GameManager.StarterCurve;
            SCh.value = GameManager.StarterChange;
            SA.value = GameManager.StarterAgil;
        }

        if (MiddleB)
        {
            MF.value = GameManager.MiddleFast;
            MS.value = GameManager.MiddleSlid;
            MC.value = GameManager.MiddleCurve;
            MCh.value = GameManager.MiddleChange;
            MA.value = GameManager.MiddleAgil;
        }

        if (SetUpB)
        {
            SeF.value = GameManager.SetUpFast;
            SeS.value = GameManager.SetUpSlid;
            SeC.value = GameManager.SetUpCurve;
            SeCh.value = GameManager.SetUpChange;
            SeA.value = GameManager.SetUpAgil;
        }

        if (CloserB)
        {
            CF.value = GameManager.CloserFast;
            CS.value = GameManager.CloserSlid;
            CC.value = GameManager.CloserCurve;
            CCh.value = GameManager.CloserChange;
            CA.value = GameManager.CloserAgil;
        }
    }

    public void FastUp()
    {
        if (GameManager.Money >= costPerIncrease)
        {
            if (StartB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.StarterFast += 1;
            }
            if (MiddleB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.MiddleFast += 1;
            }
            if (SetUpB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.SetUpFast += 1;
            }
            if (CloserB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.CloserFast += 1;
            }
        }
    }

    public void SlidUp()
    {
        if (GameManager.Money >= costPerIncrease)
        {
            if (StartB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.StarterSlid += 1;
            }
            if (MiddleB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.MiddleSlid += 1;
            }
            if (SetUpB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.SetUpSlid += 1;
            }
            if (CloserB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.CloserSlid += 1;
            }
        }
    }
    public void CurveUp()
    {
        if (GameManager.Money >= costPerIncrease)
        {
            if (StartB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.StarterCurve += 1;
            }
            if (MiddleB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.MiddleCurve += 1;
            }
            if (SetUpB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.SetUpCurve += 1;
            }
            if (CloserB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.CloserCurve += 1;
            }
        }
    }
    public void ChangeUp()
    {
        if (GameManager.Money >= costPerIncrease)
        {
            if (StartB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.StarterChange += 1;
            }
            if (MiddleB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.MiddleChange += 1;
            }
            if (SetUpB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.SetUpChange += 1;
            }
            if (CloserB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.CloserChange += 1;
            }
        }
    }
    public void AgilUp()
    {
        if (GameManager.Money >= costPerIncrease)
        {
            if (StartB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.StarterAgil += 1;
            }
            if (MiddleB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.MiddleAgil += 1;
            }
            if (SetUpB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.SetUpAgil += 1;
            }
            if (CloserB)
            {
                GameManager.Money -= costPerIncrease;
                GameManager.CloserAgil += 1;
            }
        }
    }

}
