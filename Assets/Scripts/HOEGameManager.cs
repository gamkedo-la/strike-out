using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOEGameManager : MonoBehaviour
{
    public static bool redToggle, greenToggle;

    public static bool plaqueArea, umpArea, displayArea, cornfieldArea;

    public GameObject plaqueRoom, umpireRoom, displayRoom, cornfieldRoom;

    public GameObject player;
    public Transform afterUmpire;

    public static bool UmpireDefeated;
    public static bool UmpireAlreadyKilled;

    private void Start()
    {
        if (UmpireDefeated)
        {
            if (!UmpireAlreadyKilled)
            {
                player.transform.position = afterUmpire.transform.position;
                UmpireAlreadyKilled = true;
            }
        }
        else
        {
            player.transform.position = new Vector3(PlayerLocationDontDestroy.playerX, PlayerLocationDontDestroy.playerY + 3, PlayerLocationDontDestroy.playerZ);
        }
    }

}
