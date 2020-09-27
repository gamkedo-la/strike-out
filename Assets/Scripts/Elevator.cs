using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elevator : MonoBehaviour
{
    public GameObject barrier, door;
    public GameObject elevator;
    public GameObject player, mapCam;
    public GameObject mainCam, elevatorCam;

    public Transform topPos, bottomPos;

    public float pathPerc = 0.0f;

    bool elevatorRising = false;
    bool elevatorSinking = false;
    private void FixedUpdate()
    {
       /* if (Input.GetKeyDown(KeyCode.E))
        {
            barrier.SetActive(false);
        }
        */
        if (elevatorRising)
        {
            pathPerc += Time.deltaTime * .3f;
            if (pathPerc > 1.0f)
            {
                pathPerc = 1.0f;
                ElevatorEnd();
            }
            elevator.transform.position = Vector3.Lerp(bottomPos.position, topPos.position, pathPerc);

            //elevator.transform.position += elevator.transform.forward * Time.deltaTime * 4f;
        }

        if (elevatorSinking)
        {
            pathPerc -= Time.deltaTime * .3f;
            if (pathPerc < 0.0f)
            {
                pathPerc = 0.0f;
                ElevatorEnd();
            }
            elevator.transform.position = Vector3.Lerp(bottomPos.position, topPos.position, pathPerc);

            //elevator.transform.position += elevator.transform.forward * Time.deltaTime * 4f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (pathPerc < 0.5f)
            {
                elevatorRising = true;
            }
            else
            {
                elevatorSinking = true;
            }
           
            door.SetActive(true);
            elevatorCam.SetActive(true);
            mainCam.SetActive(false);
            mapCam.SetActive(false);
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.SetParent(elevator.transform);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            elevatorCam.SetActive(false);
            mainCam.SetActive(true);
            mapCam.SetActive(true);
        }
    }

    private void ElevatorEnd()
    {
        elevatorRising = false;
        elevatorSinking = false;

        door.SetActive(false);
        player.GetComponent<NavMeshAgent>().enabled = true;
        player.transform.SetParent(null);
    }
}
