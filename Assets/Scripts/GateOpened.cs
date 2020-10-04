using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpened : MonoBehaviour
{
    public Animator gate;
    // Start is called before the first frame update
    void Start()
    {
        if (KeyConcourse.gateHasBeenOpened)
        {
            gate.SetBool("isOpen", true);
        }
    }
}
