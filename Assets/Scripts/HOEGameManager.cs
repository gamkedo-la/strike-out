using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOEGameManager : MonoBehaviour
{
    public static bool redToggle, greenToggle;

    public static bool plaqueArea, umpArea, displayArea, cornfieldArea;

    public GameObject plaqueRoom, umpireRoom, displayRoom, cornfieldRoom;

    public static bool UmpireDefeated;

    public GameObject player;

    private void Start()
    {
        if (UmpireDefeated)
        {
            player.transform.position = new Vector3(PlayerLocationDontDestroy.playerX, PlayerLocationDontDestroy.playerY, PlayerLocationDontDestroy.playerZ);
        }
    }

}
