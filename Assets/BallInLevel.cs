using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInLevel : MonoBehaviour
{
    public Rigidbody rb;
    public int minLifeTime, maxLifeTime;
    float currentLifeLeft;

    private void Start()
    {
        currentLifeLeft = Random.Range(minLifeTime, maxLifeTime);
        this.transform.SetParent(GameObject.Find("Holder").transform);
    }

    private void Update()
    {
        currentLifeLeft -= Time.deltaTime;
        if (currentLifeLeft <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ActivateRB()
    {
        rb.useGravity = true;
       // transform.parent = null;
    }
}
