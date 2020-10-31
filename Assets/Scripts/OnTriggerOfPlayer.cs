using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerOfPlayer : MonoBehaviour
{
    public GameObject Exclam;

    public float startTime = 1f;
    float Timer = 1f;
    bool hasEnteredZone;

    public bool Concourse;
    public bool HoE;

    GameObject screenBreak;
    GameObject player;

    private void Start()
    {
        screenBreak = GameObject.Find("EnemyScreenBreakHolder");
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (hasEnteredZone)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer <= 0 && !PlayerMovement.hasAttackedEnemy)
        {
            GameManager.enemyAttackedPlayer = true;
            screenBreak.GetComponent<TurnObjectOn>().enabled = true;
            //Turn On ShatterBall (red?)
            StartCoroutine(Waiting());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Exclam.SetActive(true);
            hasEnteredZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Exclam.SetActive(false);
            hasEnteredZone = false;
            Timer = startTime;
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(2.5f);
        if (HoE)
        {
            PlayerLocationDontDestroy.playerX = player.transform.position.x;
            PlayerLocationDontDestroy.playerY = player.transform.position.y;
            PlayerLocationDontDestroy.playerZ = player.transform.position.z;

            print("Saving location as: " + PlayerLocationDontDestroy.playerX + "," + PlayerLocationDontDestroy.playerY + "," + PlayerLocationDontDestroy.playerZ);

            SceneManager.LoadScene("HoEBattle");
        }
        if (Concourse)
        {
            PlayerLocationDontDestroy.playerX = player.transform.position.x;
            PlayerLocationDontDestroy.playerY = player.transform.position.y;
            PlayerLocationDontDestroy.playerZ = player.transform.position.z;

            print("Saving location as: " + PlayerLocationDontDestroy.playerX + "," + PlayerLocationDontDestroy.playerY + "," + PlayerLocationDontDestroy.playerZ);

            SceneManager.LoadScene("SampleScene");
        }
    }
}
