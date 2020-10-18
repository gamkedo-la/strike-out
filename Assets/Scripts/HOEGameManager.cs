using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOEGameManager : MonoBehaviour
{
    public static bool redToggle, greenToggle;

    public static bool plaqueArea, umpArea, displayArea, cornfieldArea;

    public GameObject plaqueRoom, umpireRoom, displayRoom, cornfieldRoom;

    private void Start()
    {
        plaqueArea = true;
    }

   /* private void Update()
    {
        if (HOEGameManager.plaqueArea)
        {
            plaqueRoom.SetActive(true);
        }

        if (HOEGameManager.umpArea)
        {
            umpireRoom.SetActive(true);
        }

        if (HOEGameManager.displayArea)
        {
            displayRoom.SetActive(true);
        }

        if (HOEGameManager.cornfieldArea)
        {
            cornfieldRoom.SetActive(true);
        }

        if (!HOEGameManager.plaqueArea)
        {
            plaqueRoom.SetActive(false);
        }

        if (!HOEGameManager.umpArea)
        {
            umpireRoom.SetActive(false);
        }

        if (!HOEGameManager.displayArea)
        {
            displayRoom.SetActive(false);
        }

        if (!HOEGameManager.cornfieldArea)
        {
            cornfieldRoom.SetActive(false);
        }
    }
    */
}
