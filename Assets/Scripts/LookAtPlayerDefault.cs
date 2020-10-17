using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerDefault : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 50 * Time.deltaTime);
    }
}
