using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTraining : MonoBehaviour
{
    public GameObject normalPlayer, dizzyPlayer;

    private void Start()
    {
        if (GameManager.isGameOver)
        {
            normalPlayer.SetActive(false);
            dizzyPlayer.SetActive(true);
            this.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(Waiting());
        }

        else
        {
            normalPlayer.SetActive(true);
            dizzyPlayer.SetActive(false);
        }

        GameManager.isGameOver = false;
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(5f);
        normalPlayer.SetActive(true);
        dizzyPlayer.SetActive(false);
        this.GetComponent<PlayerMovement>().enabled = true;
    }
}
