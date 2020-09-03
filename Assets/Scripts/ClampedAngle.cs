using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedAngle : MonoBehaviour
{
    public float minAngle, maxAngle;
    public float currentAngle;
    public GameObject baseball, Holder;

    AudioClip Crack;

    private void Start()
    {

        currentAngle = Random.Range(minAngle, maxAngle);
    }

    public void ChooseAngle()
    {
        currentAngle = Random.Range(minAngle, maxAngle);
        Holder.gameObject.transform.rotation = Quaternion.Euler(0, currentAngle,0 );

        Instantiate(baseball, Holder.transform.position, Holder.transform.rotation);

        StartCoroutine(Sound());
    }

    IEnumerator Sound()
    {
        AudioSource crack = GetComponent<AudioSource>();
        yield return new WaitForSeconds(.1f);
        crack.Play();
        yield return new WaitForSeconds(.25f);
        crack.Play();
        yield return new WaitForSeconds(.4f);
        crack.Play();
    }
}
