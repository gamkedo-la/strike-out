using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConcourseGameManager : MonoBehaviour
{
    public static bool McGeeKilled, AnnouncerKilled;
    public static bool McGeeHasAlreadyBeenKilled, AnnouncerHasAlreadyBeenKilled;

    public GameObject Fred, AnnouncerBoss; 

    public GameObject elevatorBlock, barrierOnElevator1, barrierOnElevator2, barrierOnElevator3, barrierOnElevator4, ElevatorCam, mainCam;

    public static bool elevatorUnlocked;

    public Dialogue dialogue;

    public Transform playerStartAfterMcGee;
    public GameObject Player;

    public GameObject AnnouncerConvo, AnnouncerCam;

    private void Start()
    {
        if (elevatorUnlocked)
        {
            Destroy(elevatorBlock);
            Destroy(barrierOnElevator1);
            Destroy(barrierOnElevator2);
            Destroy(barrierOnElevator3);
            Destroy(barrierOnElevator4);
        }

        Player.GetComponent<NavMeshAgent>().enabled = false;
        Player.transform.position = new Vector3(PlayerLocationDontDestroy.playerX, PlayerLocationDontDestroy.playerY, PlayerLocationDontDestroy.playerZ);
        print("I transported the player to: " + Player.transform.position);
        Player.GetComponent<NavMeshAgent>().enabled = true;

        if (!McGeeHasAlreadyBeenKilled)
        {
            if (McGeeKilled)
            {
                elevatorUnlocked = true;
                Fred.SetActive(false);
                StartCoroutine(McGeeKilledWaiting());
            }
        }

        if (!AnnouncerHasAlreadyBeenKilled)
        {
            if (AnnouncerKilled)
            {
                AnnouncerBoss.SetActive(false);
                AnnouncerCam.SetActive(true);
                mainCam.SetActive(false);
                StartCoroutine(AnnouncerKilledWait());
            }
        }
    }

    IEnumerator McGeeKilledWaiting()
    {
        Player.transform.position = playerStartAfterMcGee.transform.position;
        yield return new WaitForSeconds(1.5f);
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

        yield return new WaitForSeconds(1f);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    IEnumerator AnnouncerKilledWait()
    {
        yield return new WaitForSeconds(7.5f);
        AnnouncerCam.SetActive(false);
        mainCam.SetActive(true);
        AnnouncerConvo.SetActive(true);
    }
}
