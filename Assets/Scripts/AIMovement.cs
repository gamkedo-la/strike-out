using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool isWandering = false, isRotatingLeft = false, isRotatingRight = false, isWalking = false;

    Ray enemyRay;
    public Color rayColor;
    RaycastHit rayHit;
    bool follow;

    private NavMeshAgent agent;
    GameObject starter;


    public GameObject[] Vendors;
    public Animator Vendor;

    private void Start()
    {
        int RandInt = Random.Range(0, 3);
        Vendors[RandInt].SetActive(true);
        Vendor = Vendors[RandInt].GetComponent<Animator>();
        starter = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        Vendor.SetBool("isStill", true);
        Vendor.SetBool("isWalk", false);
        Vendor.SetBool("isRun", false);
        // draw a 5-unit white line from the origin for 2.5 seconds
    }

    private void Update()
    {
        float dist = Vector3.Distance(this.transform.position, starter.transform.position);
        //raycasting
        enemyRay = new Ray(transform.position, transform.forward * 10);
        Debug.DrawRay(transform.position, transform.forward * 10, rayColor);

        if (Physics.Raycast(transform.position, transform.forward, 10))
        {
            follow = true;
        }

        if (follow)
        {
            if (dist <= 30)
            {
                Vendor.SetBool("isStill", false);
                Vendor.SetBool("isWalk", false);
                Vendor.SetBool("isRun", true);
                if (starter != null)
                    agent.SetDestination(starter.transform.position);
                moveSpeed = moveSpeed * 3;
            }
            else
                follow = false;
        }

        //following
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }

        if (isRotatingRight)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);    
        }

        if (isRotatingLeft)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }

        if (isWalking)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        Vendor.SetBool("isStill", false);
        Vendor.SetBool("isWalk", true);
        Vendor.SetBool("isRun", false);
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            Vendor.SetBool("isStill", true);
            Vendor.SetBool("isWalk", false);
            Vendor.SetBool("isRun", false);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            Vendor.SetBool("isStill", true);
            Vendor.SetBool("isWalk", false);
            Vendor.SetBool("isRun", false);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
}
