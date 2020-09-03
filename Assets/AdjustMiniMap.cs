using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMiniMap : MonoBehaviour
{
    public int yHeightBase, yHeightIncrease;
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            this.transform.localPosition = new Vector3(0, yHeightIncrease, 171);
        }
        else
            this.transform.localPosition = new Vector3(0, yHeightBase, 0);
    }
}
