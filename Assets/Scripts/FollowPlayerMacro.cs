using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerMacro : MonoBehaviour
{
    public Vector3 offsetFromPlayer;
    public Transform player;
    NavMeshAgent nav;
    public GameObject PlayerModel;
    Animator anim;

    private void Start()
    {
        anim = PlayerModel.GetComponent<Animator>();
        transform.position = player.position + offsetFromPlayer;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > 4)
        {
            anim.SetBool("isRunning", true);
        }
        if (distance <= 4)
        {
            anim.SetBool("isRunning", false);
        }
        if (player == null)
        { return; }
        else
        { nav.SetDestination(player.position); }
    }


}
