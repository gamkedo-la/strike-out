using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introBall : MonoBehaviour
{
    public GameObject menu, shatterball,ball;

    public void EndOfAnim()
    {
        menu.SetActive(true);
        shatterball.SetActive(true);
        //ball.SetActive(false);
    }
}
