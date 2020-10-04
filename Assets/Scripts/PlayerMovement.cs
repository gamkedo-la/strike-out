using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float basespeed = 10f;
    public float sprintSpeed = 15f;
    float currentSpeed;

    public float rotateSpeed = 10f;

    bool interactActiveManager;
    bool interactActiveEnemy;

    public static bool canMove;
    public GameObject ShatterBall;

    public GameObject playerModel;
    Animator anim;
    private void Start()
    {
        anim = playerModel.GetComponent<Animator>();
        canMove = true;
    }

    private void LateUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = sprintSpeed;
            }
            else
                currentSpeed = basespeed;
           

            float translation = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
            float rotation = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);

            if (interactActiveManager && Input.GetKeyDown(KeyCode.Space))
            {
                print("Load Dialogue");
            }
            if (interactActiveEnemy && Input.GetKeyDown(KeyCode.Space))
            {
                PlayerLocationDontDestroy.playerX = transform.position.x;
                PlayerLocationDontDestroy.playerY = transform.position.y;
                PlayerLocationDontDestroy.playerZ = transform.position.z;
                //waiting for the shatter effect 
                ShatterBall.SetActive(true);
                StartCoroutine(Waiting());
            }

            if (Input.GetAxis("Vertical") >= 0.03 || Input.GetAxis("Vertical") <= -.03f)
            {
                anim.SetBool("isRunning", true);
            }
            if (Input.GetAxis("Vertical") <= 0.03 && Input.GetAxis("Vertical") >= -.03f)
            {
                anim.SetBool("isRunning", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Manager")
        {
            interactActiveManager = true; 
        }

        if (other.tag == "Enemy")
        {
            interactActiveEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Manager")
        {
            interactActiveManager = false;
        }

        if (other.tag == "Enemy")
        {
            interactActiveEnemy = false;
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }
}
