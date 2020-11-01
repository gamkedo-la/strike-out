using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    Slider SM, MM, SeM, CM, SE, ME, SeE, CE;

    public Text Objective;


    private void Awake()
    {
        SM = GameObject.Find("StarterMorale").GetComponent<Slider>();
        MM = GameObject.Find("MiddleRelivMorale").GetComponent<Slider>();
        SeM = GameObject.Find("SetUpMorale").GetComponent<Slider>();
        CM = GameObject.Find("CloserMorale").GetComponent<Slider>();

        SE = GameObject.Find("StarterEnergy").GetComponent<Slider>();
        ME = GameObject.Find("MiddleRelivEnergy").GetComponent<Slider>();
        SeE = GameObject.Find("SetUpEnergy").GetComponent<Slider>();
        CE = GameObject.Find("CloserEnergy").GetComponent<Slider>();

    }
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
        //print("I transported the player to: " + Player.transform.position);
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

    private void Update()
    {
        SM.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
        MM.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
        SeM.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
        CM.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);

        SE.value = (GameManager.StarterEnergy / GameManager.StarterEnergyMax);
        ME.value = (GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax);
        SeE.value = (GameManager.SetUpEnergy / GameManager.SetUpEnergyMax);
        CE.value = (GameManager.CloserEnergy / GameManager.CloserEnergyMax);

        if (!KeyConcourse.gateHasBeenOpened)
        {
            Objective.text = "Find Keys to Open the Gate".ToString();
        }

        if (KeyConcourse.gateHasBeenOpened && !McGeeHasAlreadyBeenKilled)
        {
            Objective.text = "Take down Power McGee".ToString();
        }

        if (McGeeHasAlreadyBeenKilled && elevatorUnlocked)
        {
            Objective.text = "Go upstairs to take down the Announcer".ToString();
        }

        else if (McGeeHasAlreadyBeenKilled && elevatorUnlocked && AnnouncerHasAlreadyBeenKilled)
        {
            Objective.text = "Move on to the next Level".ToString();
        }
    }

    IEnumerator McGeeKilledWaiting()
    {
        Player.GetComponent<NavMeshAgent>().enabled = false;
        Player.transform.position = playerStartAfterMcGee.transform.position;
        Player.GetComponent<NavMeshAgent>().enabled = true;
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
