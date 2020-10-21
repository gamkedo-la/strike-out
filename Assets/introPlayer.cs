using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introPlayer : MonoBehaviour
{
    public GameObject gloveBall, handball, animatedBall;

    public void ToggleGloveHand()
    {
        gloveBall.SetActive(false);
        handball.SetActive(true);
    }

    public void ToggleHandBall()
    {
        handball.SetActive(false);
        animatedBall.SetActive(true);
    }
}
