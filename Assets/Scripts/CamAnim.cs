using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnim : MonoBehaviour
{
    public Animator cam;
    public string boolToSet;
    private void Start()
    {
        cam.SetBool(boolToSet, true);
    }
}
