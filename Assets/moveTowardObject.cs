using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTowardObject : MonoBehaviour
{
    Vector3 startDestination;
    Vector3 Destination;

    public GameObject shatterScreen;

    public float timeToReachTarget = .2f;
    float t;

    private void Start()
    {
        StartCoroutine(Waiting());
    }

    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startDestination, Destination, t);
    }
    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startDestination = transform.position;
        timeToReachTarget = time;
        Destination = destination;
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(.1f);
        startDestination = GameObject.FindGameObjectWithTag("Player").transform.position;
        Destination = GameObject.FindGameObjectWithTag("Screen").transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Screen")
        {
            shatterScreen.SetActive(true);
        }
    }
}
