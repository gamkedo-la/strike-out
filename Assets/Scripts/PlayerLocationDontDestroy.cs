using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            this.gameObject.transform.position = new Vector3(playerX, playerY + 3, playerZ);
        }
        else if (SceneManager.GetActiveScene().name == "Concourse" && isStarting)
        {
            playerX = 43.9f;
            playerY = -.749f;
            playerZ = -59.18f;
            isStarting = false;
        }
        else if (SceneManager.GetActiveScene().name == "ClubHouse" && isStarting)
        {
            playerX = 4.69f;
            playerY = 1.5f;
            playerZ = -9.75f;
            isStarting = false;
        }
    }
}
