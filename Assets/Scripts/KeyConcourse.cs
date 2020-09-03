using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyConcourse : MonoBehaviour
{
    public GameObject mainCam, cutSceneCam;
    public Animator gate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mainCam.SetActive(false);
            cutSceneCam.SetActive(true);
            gate.SetBool("isOpen", true);
            StartCoroutine(Waiting());
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(10);
        mainCam.SetActive(true);
        cutSceneCam.SetActive(false);
        Destroy(this.gameObject);
    }
}
