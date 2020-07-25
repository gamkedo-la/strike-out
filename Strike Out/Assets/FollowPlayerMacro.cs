using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerMacro : MonoBehaviour
{
    public Vector3 offsetFromPlayer;
    public Transform player;
    NavMeshAgent nav;

    private void Start()
    {
        transform.position = player.position + offsetFromPlayer;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player == null)
        { return; }
        else
            nav.SetDestination(player.position);
    }
}
