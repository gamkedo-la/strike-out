using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elevator2 : MonoBehaviour
{
    public GameObject door;
    public GameObject elevator;
    public GameObject player, mapCam;
    public GameObject mainCam, elevatorCam;

    public GameObject Elevator1, thisElevator;

    bool playerInside;

    private void FixedUpdate()
    {

        if (playerInside)
        {
            door.SetActive(true);
            elevatorCam.SetActive(true);
            mainCam.SetActive(false);
            mapCam.SetActive(false);
            player.GetComponent<NavMeshAgent>().enabled = false;
            elevator.transform.position -= elevator.transform.forward * Time.deltaTime * 4f;
        }

        if (!playerInside)
        {
            door.SetActive(false);
            elevatorCam.SetActive(false);
            mainCam.SetActive(true);
            mapCam.SetActive(true);
            player.GetComponent<NavMeshAgent>().enabled = true;
            Elevator1.SetActive(true);
            thisElevator.SetActive(false);
            //StartCoroutine(Waiting());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInside = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInside = false;
        }

    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(4f);

    }
}
