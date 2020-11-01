using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HOEGameManager : MonoBehaviour
{
    public static bool redToggle, greenToggle;

    public static bool plaqueArea, umpArea, displayArea, cornfieldArea;

    public GameObject plaqueRoom, umpireRoom, displayRoom, cornfieldRoom;

    public GameObject player;
    public Transform afterUmpire;

    public static bool UmpireDefeated;
    public static bool UmpireAlreadyKilled;

    Slider SM, MM, SeM, CM, SE, ME, SeE, CE;

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
        if (UmpireDefeated)
        {
            if (!UmpireAlreadyKilled)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = afterUmpire.transform.position;
                UmpireAlreadyKilled = true;
                player.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
        else
        {
            player.transform.position = new Vector3(PlayerLocationDontDestroy.playerX, PlayerLocationDontDestroy.playerY + 3, PlayerLocationDontDestroy.playerZ);
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
    }

}
