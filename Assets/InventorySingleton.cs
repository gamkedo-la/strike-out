﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySingleton : MonoBehaviour
{
    private static InventorySingleton _instance;
    public static InventorySingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("CanvasInventory");
                go.AddComponent<InventorySingleton>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }
}
