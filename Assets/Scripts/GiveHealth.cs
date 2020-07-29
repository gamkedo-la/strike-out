using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.StarterMorale <= 0)
        {
            GameManager.StarterMorale = 1;
        }
        if (GameManager.MidRelivMorale <= 0)
        {
            GameManager.MidRelivMorale = 1;
        }
        if (GameManager.SetUpMorale <= 0)
        {
            GameManager.SetUpMorale = 1;
        }
        if (GameManager.CloserMorale <= 0)
        {
            GameManager.CloserMorale = 1;
        }
    }

}
