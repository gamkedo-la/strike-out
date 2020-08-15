using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject buttonTemplate;

    void Start()
    {
        for (int i = 1; i <= 20; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<InventoryItemButton>().SetText("Item #" + i);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

}
