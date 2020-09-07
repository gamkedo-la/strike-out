using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifetime : MonoBehaviour
{
    public float lifetimeMin, lifetimeMax;
    float life;
    // Start is called before the first frame update
    void Start()
    {
        life = Random.Range(lifetimeMin, lifetimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
