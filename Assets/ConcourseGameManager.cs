using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcourseGameManager : MonoBehaviour
{
    public static bool McGeeKilled, AnnouncerKilled;
    public static bool McGeeHasAlreadyBeenKilled, AnnouncerHasAlreadyBeenKilled;

    public GameObject Fred, AnnouncerBoss; 

    public GameObject elevatorBlock, barrierOnElevator1, barrierOnElevator2, barrierOnElevator3, barrierOnElevator4, ElevatorCam, mainCam;

    private void Start()
    {
        if (!McGeeHasAlreadyBeenKilled)
        {
            if (McGeeKilled)
            {
                Fred.SetActive(false);
                StartCoroutine(McGeeKilledWaiting());
            }
        }
    }

    IEnumerator McGeeKilledWaiting()
    {
        ElevatorCam.SetActive(true);
        mainCam.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        barrierOnElevator1.GetComponent<Rigidbody>().isKinematic = false;
        barrierOnElevator1.GetComponent<Rigidbody>().useGravity = true;
        barrierOnElevator2.GetComponent<Rigidbody>().isKinematic = false;
        barrierOnElevator2.GetComponent<Rigidbody>().useGravity = true;
        barrierOnElevator3.GetComponent<Rigidbody>().isKinematic = false;
        barrierOnElevator3.GetComponent<Rigidbody>().useGravity = true;
        barrierOnElevator4.GetComponent<Rigidbody>().isKinematic = false;
        barrierOnElevator4.GetComponent<Rigidbody>().useGravity = true;

        yield return new WaitForSeconds(3f);
        Destroy(elevatorBlock);
        Destroy(barrierOnElevator1);
        Destroy(barrierOnElevator2);
        Destroy(barrierOnElevator3);
        Destroy(barrierOnElevator4);

        yield return new WaitForSeconds(.5f);
        ElevatorCam.SetActive(false);
        mainCam.SetActive(true);
        McGeeHasAlreadyBeenKilled = true;
    }
}
