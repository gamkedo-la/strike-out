﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public GameObject Batter;

    public bool m1, m2, m3, m4, m5, m6, m7, m8;
    public bool M1, M2, M3, M4, M5, M6, M7, M8;
    public bool e1, e2, e3, e4, e5, e6, e7, e8;
    public void Enter()
    {
        Batter.SetActive(true);
    }

    public void Exit()
    {
        Batter.SetActive(false);
    }

    public void Purchase()
    {
        if(m1)
        { GameManager.m1 = true; }
        if (m2)
        { GameManager.m2 = true; }
        if (m3)
        { GameManager.m3 = true; }
        if (m4)
        { GameManager.m4 = true; }
        if (m5)
        { GameManager.m5 = true; }
        if (m6)
        { GameManager.m6 = true; }
        if (m7)
        { GameManager.m7 = true; }
        if (m8)
        { GameManager.m8 = true; }

        if (M1)
        { GameManager.M1 = true; }
        if (M2)
        { GameManager.M2 = true; }
        if (M3)
        { GameManager.M3 = true; }
        if (M4)
        { GameManager.M4 = true; }
        if (M5)
        { GameManager.M5 = true; }
        if (M6)
        { GameManager.M6 = true; }
        if (M7)
        { GameManager.M7 = true; }
        if (M8)
        { GameManager.M8 = true; }

        if (e1)
        { GameManager.e1 = true; }
        if (e2)
        { GameManager.e2 = true; }
        if (e3)
        { GameManager.e3 = true; }
        if (e4)
        { GameManager.e4 = true; }
        if (e5)
        { GameManager.e5 = true; }
        if (e6)
        { GameManager.e6 = true; }
        if (e7)
        { GameManager.e7 = true; }
        if (e8)
        { GameManager.e8 = true; }  
    }
}
