using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float bottomFloory, topFloory;
    bool isTop;
    public GameObject door;
    public Vector3 doorOpen, doorClosed;
    bool isMoving;

    //Testing
    public GameObject barrier;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isMoving = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(barrier);
        }

        if (isMoving)
        {
            door.transform.position = doorClosed;

            if (isTop)
            {
                transform.Translate(Vector3.forward * -10 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            }
        }

        if (gameObject.transform.position.y >= topFloory)
        {
            isTop = true;
            isMoving = false;
            door.transform.position = doorOpen;
        }

        if (gameObject.transform.position.y <= bottomFloory)
        {
            isTop = false;
            isMoving = false;
            door.transform.position = doorOpen;
        }

        if (!isMoving)
        {
        }
    }
}
