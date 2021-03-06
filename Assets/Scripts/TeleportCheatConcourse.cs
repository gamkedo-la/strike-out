﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportCheatConcourse : MonoBehaviour
{
    public GameObject player;
    public Vector3 keys, main, elevatortop, elevatordown, announcer, umpire, babe, fred;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = fred;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetKey(KeyCode.K))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = keys;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetKey(KeyCode.M))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = main;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetKey(KeyCode.V))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = elevatortop;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetKey(KeyCode.C))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = elevatordown;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = announcer;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetKey(KeyCode.U))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = umpire;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        if (Input.GetKey(KeyCode.B))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = babe;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
