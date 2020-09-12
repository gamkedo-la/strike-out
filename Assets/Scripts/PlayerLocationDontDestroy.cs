using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocationDontDestroy : MonoBehaviour
{
    public static float playerX;
    public static float playerY;
    public static float playerZ;
    public static bool isStarting = true;

    private void Awake()
    {
        if (!isStarting)
        {
            this.gameObject.transform.position = new Vector3(playerX, playerY, playerZ);
        }
        else
        {
            playerX = 43.9f;
            playerY = -.749f;
            playerZ = -59.18f;
            isStarting = false;
        }
    }
}
