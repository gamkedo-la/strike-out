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
            playerX = 311.92f;
            playerY = 171.71f;
            playerZ = -18.9f;
            isStarting = false;
        }
    }
}
