using System.Collections;
using UnityEngine;

public class AudioVOs : MonoBehaviour
{
    public AudioData pitchVO;
    public AudioData enemyAtkVO;
    public AudioData playerTakeDmgVO;
    public AudioData EnemyTakeDmgVO;

    public AudioSourceController controller;

    private void Start()
    {
        if (controller == null)
            controller = gameObject.GetComponent<AudioSourceController>();
    }

    public IEnumerator PlayAfterDelay(float time, AudioData sound)
    {
        yield return new WaitForSeconds(time);

        controller.PlayRandom(sound);
        Debug.LogWarning("Calling VO");
        yield return null;
    }
}
