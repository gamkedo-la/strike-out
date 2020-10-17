using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    float occurance;
    float occuranceMax = 1f;

    float duration = 9f;

    private void Update()
    {
        duration -= Time.deltaTime;

        occurance += Time.deltaTime;
        if (duration >= 0)
        {
            if (occurance >= occuranceMax)
            {
                StartCoroutine(Shake(.25f, 1f));
                occurance = 0;
            }
        }
    }



    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-2f, 2f) * magnitude;
            float y = Random.Range(-27f, -25f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
