using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{
    public Text itemText;

    public void SetText(string textString)
    {
        itemText.text = textString;
    }

    public void OnClick()
    {

    }
}
