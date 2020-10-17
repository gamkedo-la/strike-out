using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabeLifeTimeAnimation : MonoBehaviour
{
    public Animator theBabeWalk;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Waiting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(9);
        theBabeWalk.SetBool("toLeadUp", true);
    }
}
