using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAudio : MonoBehaviour
{
    public AudioSource audio1, audio2, audio3, audio4, audio5, audio6, audio7, audio8, audio9, audio10;

    private void Start()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1.5f);
        audio1.Play();
        yield return new WaitForSeconds(1.5f);
        audio2.Play();
        yield return new WaitForSeconds(1.5f);
        audio3.Play();
        yield return new WaitForSeconds(1.5f);
        audio4.Play();
        yield return new WaitForSeconds(1.5f);
        audio5.Play();
        yield return new WaitForSeconds(1.5f);
        audio6.Play();
        yield return new WaitForSeconds(1.5f);
        audio7.Play();
        yield return new WaitForSeconds(1.5f);
        audio8.Play();
        yield return new WaitForSeconds(1.5f);
        audio9.Play();
        yield return new WaitForSeconds(1.5f);
        audio10.Play();
    }
}
