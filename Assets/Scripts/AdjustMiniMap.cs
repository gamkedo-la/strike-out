using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMiniMap : MonoBehaviour
{
    public int yHeightBase, yHeightIncrease;
    public GameObject map;
    public float mapStart, mapExpand;
    public Camera miniCam;

    public bool isHoE;
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            if (isHoE)
            {
                this.transform.localPosition = new Vector3(-5, yHeightIncrease, 110);
            }

            else
            {
                this.transform.localPosition = new Vector3(50, yHeightIncrease, 171);
            }
            this.transform.rotation = Quaternion.Euler(90, 0, 0);
            map.transform.localScale = new Vector2(mapExpand, mapExpand);
            miniCam.orthographic = false;
            miniCam.transform.parent = null;
        }
        else
        {
            this.transform.localPosition = new Vector3(0, yHeightBase, 0);
            map.transform.localScale = new Vector2(mapStart, mapStart);
            miniCam.orthographic = true;
            miniCam.transform.parent = GameObject.Find("Player").transform;
        }

    }
}
