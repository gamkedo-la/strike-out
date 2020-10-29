﻿using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        screenBreak = GameObject.Find("EnemyScreenBreakHolder");
    }

    private void Update()
    {
        if (hasEnteredZone)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer <= 0)
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
            SceneManager.LoadScene("HoEBattle");
        }
        if (Concourse)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
