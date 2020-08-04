using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;
    float RotSpeed = 80;
    float distance;

    private void LateUpdate()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance >= 15)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,-180,0), RotSpeed * Time.deltaTime);
        }

        if (distance >= 5 && distance <= 15)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), RotSpeed * Time.deltaTime);
        }
    }
}
