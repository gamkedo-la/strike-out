using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOEGameManager : MonoBehaviour
{
    public static bool redToggle, greenToggle;
    // Start is called before the first frame update
    void Start()
    {
        redToggle = false;
        greenToggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            greenToggle = !greenToggle;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            redToggle = !redToggle;
        }
    }
}
