using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public string ItemName;
    public string ItemDescription;

    public int MoraleToIncrease;
    public int EnergyToIncrease;

    GameObject Display;
    Text TextDisplay;

    public bool StaminaUp20;
    public bool StaminaUp20All;
    public bool EnergyUp10;
    public bool EnergyUp10All;
    public bool EvasionUpFor3Turns;
    public bool EnemyHealthDown20;
    public bool AllEnemyHealthDown20;
    public string boolName;

    GameObject InventoryManage;

    public bool i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15, i16, i17, i18, i19, i20, i21, i22, i23, i24, i25;

    private void Start()
    {
        Display = GameObject.Find("ItemTextDisplay");
        TextDisplay = Display.GetComponent<Text>();

        InventoryManage = GameObject.Find("Inventory");

        if (i1 && GameManager.i1)
        {
            this.gameObject.SetActive(false);
        }
        if (i2 && GameManager.i2)
        {
            this.gameObject.SetActive(false);
        }
        if (i3 && GameManager.i13)
        {
            this.gameObject.SetActive(false);
        }
        if (i4 && GameManager.i4)
        {
            this.gameObject.SetActive(false);
        }
        if (i5 && GameManager.i5)
        {
            this.gameObject.SetActive(false);
        }
        if (i6 && GameManager.i6)
        {
            this.gameObject.SetActive(false);
        }
        if (i7 && GameManager.i7)
        {
            this.gameObject.SetActive(false);
        }
        if (i8 && GameManager.i8)
        {
            this.gameObject.SetActive(false);
        }
        if (i9 && GameManager.i9)
        {
            this.gameObject.SetActive(false);
        }
        if (i10 && GameManager.i10)
        {
            this.gameObject.SetActive(false);
        }
        if (i11 && GameManager.i11)
        {
            this.gameObject.SetActive(false);
        }
        if (i12 && GameManager.i12)
        {
            this.gameObject.SetActive(false);
        }
        if (i13 && GameManager.i13)
        {
            this.gameObject.SetActive(false);
        }
        if (i14 && GameManager.i14)
        {
            this.gameObject.SetActive(false);
        }
        if (i15 && GameManager.i15)
        {
            this.gameObject.SetActive(false);
        }
        if (i16 && GameManager.i16)
        {
            this.gameObject.SetActive(false);
        }
        if (i17 && GameManager.i17)
        {
            this.gameObject.SetActive(false);
        }
        if (i18 && GameManager.i18)
        {
            this.gameObject.SetActive(false);
        }
        if (i19 && GameManager.i19)
        {
            this.gameObject.SetActive(false);
        }
        if (i20 && GameManager.i20)
        {
            this.gameObject.SetActive(false);
        }
        if (i21 && GameManager.i21)
        {
            this.gameObject.SetActive(false);
        }
        if (i22 && GameManager.i22)
        {
            this.gameObject.SetActive(false);
        }
        if (i23 && GameManager.i23)
        {
            this.gameObject.SetActive(false);
        }
        if (i24 && GameManager.i24)
        {
            this.gameObject.SetActive(false);
        }
        if (i25 && GameManager.i25)
        {
            this.gameObject.SetActive(false);
        }
    }

    public InventoryItem(string thisName, string thisDescription)
    {
        ItemName = thisName;
        ItemDescription = thisDescription;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            if (StaminaUp20)
            {
                InventoryManage.GetComponent<InventoryManager>().StamUp20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (StaminaUp20All)
            {
                InventoryManage.GetComponent<InventoryManager>().StamUpAll20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EnergyUp10)
            {
                InventoryManage.GetComponent<InventoryManager>().EnUp10();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EnergyUp10All)
            {
                InventoryManage.GetComponent<InventoryManager>().EnUpAll10();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EvasionUpFor3Turns)
            {
                InventoryManage.GetComponent<InventoryManager>().EvasUp3Turns();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (EnemyHealthDown20)
            {
                InventoryManage.GetComponent<InventoryManager>().EnemyHealthDown20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (AllEnemyHealthDown20)
            {
                InventoryManage.GetComponent<InventoryManager>().EnemyHealthDownAll20();
                TextDisplay.text = ItemName.ToString();
                StartCoroutine(Waiting());
            }

            if (i1)
            {
                GameManager.i1 = true;
            }
            if (i2)
            {
                GameManager.i2 = true;
            }
            if (i3)
            {
                GameManager.i3 = true;
            }
            if (i4)
            {
                GameManager.i4 = true;
            }
            if (i5)
            {
                GameManager.i5 = true;
            }
            if (i6)
            {
                GameManager.i6 = true;
            }
            if (i7)
            {
                GameManager.i7 = true;
            }
            if (i8)
            {
                GameManager.i8 = true;
            }
            if (i9)
            {
                GameManager.i9 = true;
            }
            if (i10)
            {
                GameManager.i10 = true;
            }
            if (i11)
            {
                GameManager.i11 = true;
            }
            if (i12)
            {
                GameManager.i12 = true;
            }
            if (i13)
            {
                GameManager.i13 = true;
            }
            if (i14)
            {
                GameManager.i14 = true;
            }
            if (i15)
            {
                GameManager.i15 = true;
            }
            if (i16)
            {
                GameManager.i16 = true;
            }
            if (i17)
            {
                GameManager.i17 = true;
            }
            if (i18)
            {
                GameManager.i18 = true;
            }
            if (i19)
            {
                GameManager.i19 = true;
            }
            if (i20)
            {
                GameManager.i20 = true;
            }
            if (i21)
            {
                GameManager.i21 = true;
            }
            if (i22)
            {
                GameManager.i22 = true;
            }
            if (i23)
            {
                GameManager.i23 = true;
            }
            if (i24)
            {
                GameManager.i24 = true;
            }
            if (i25)
            {
                GameManager.i25 = true;
            }
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1.5f);
        TextDisplay.text = "";
        Destroy(this.gameObject);
    }
}
