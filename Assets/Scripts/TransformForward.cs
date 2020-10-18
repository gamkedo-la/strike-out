using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformForward : MonoBehaviour
{
    bool isMoving = true;
    float countdown = 9.75f;
    Vector3 scaleChange = new Vector3(.005f, .005f, .005f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            isMoving = false;
        }
        if (isMoving)
        {
            transform.Translate(new Vector3(.1125f, 0, 0));
            transform.localScale += scaleChange;
        }
    }
}
