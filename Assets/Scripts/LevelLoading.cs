using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoading : MonoBehaviour
{
    public bool turnOn, turnOff;
    public bool plaques, umpire, display, cornfield;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (turnOn)
            {
                if (plaques)
                {
                    HOEGameManager.plaqueArea = true;
                }
                else if (umpire)
                {
                    HOEGameManager.umpArea = true;
                }
                else if(display)
                {
                    HOEGameManager.displayArea = true;
                }
                else if (cornfield)
                {
                    HOEGameManager.cornfieldArea = true;
                }
            }

            if (turnOff)
            {
                if (plaques)
                {
                    HOEGameManager.plaqueArea = false;
                }
                else if (umpire)
                {
                    HOEGameManager.umpArea = false;
                }
                else if(display)
                {
                    HOEGameManager.displayArea = false;
                }
                else if (cornfield)
                {
                    HOEGameManager.cornfieldArea = false;
                }
            }
        }
        Destroy(this.gameObject);
    }

   
}
