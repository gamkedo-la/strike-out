using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingLookAt : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = .2f;
    // Start is called before the first frame update
   
    void FixedUpdate()
    {
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }
}
