using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBaseball : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnCollisionEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            rb.useGravity = true;
        }
    }
}
