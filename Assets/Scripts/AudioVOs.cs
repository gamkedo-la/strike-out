using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioClipList
{
    [SerializeField] string name;
    public List<AudioClip> Sounds = new List<AudioClip>();
}

public class AudioVOs : MonoBehaviour
{
    public AudioData pitchVO;
    public AudioData enemyAtkVO;
    public AudioData playerTakeDmgVO;
    public AudioData EnemyTakeDmgVO;
    public List<AudioClipList> VOSounds = new List<AudioClipList>();


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
