using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{
    public Text moneyUi;

    private void Update()
    {
        moneyUi.text = "$" + GameManager.Money.ToString();
    }
}
