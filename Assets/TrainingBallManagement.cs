using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingBallManagement : MonoBehaviour
{
    public GameObject baseball;
    public Transform ballSpawn;
    GameObject ball;
    Rigidbody rb;
    bool isMoving;
    public bool Mid, Set;

    private void Start()
    {
    }
    public void CreateBall()
    {
        ball = Instantiate(baseball, ballSpawn.transform.localPosition, ballSpawn.transform.rotation) as GameObject;
        ball.transform.SetParent(ballSpawn, false);
        rb = ball.GetComponent<Rigidbody>();
    }

    public void ReleaseBall()
    {
        ballSpawn.transform.DetachChildren();
        ball.transform.parent = null;
        ball.transform.rotation = Quaternion.Euler(0, -180, 90);
        isMoving = true;
        //baseball.transform.forward * 15 * Time.deltaTime;
    }

    private void Update()
    {
        if (isMoving)
        {
            rb.velocity = transform.forward * 500 * Time.deltaTime;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        if (Mid)
        {
            yield return new WaitForSeconds(1f);
            rb.useGravity = true;
            isMoving = false;

        }
        if (Set)
        {
            yield return new WaitForSeconds(.2f);
            rb.useGravity = true;
            rb.velocity = transform.forward * 300 * Time.deltaTime;
            yield return new WaitForSeconds(1.1f);
            isMoving = false;
        }
        rb.velocity = transform.right *-5 * Time.deltaTime;

        yield return new WaitForSeconds(.2f);
         ball.GetComponent<SphereCollider>().enabled = true;
    }
}
