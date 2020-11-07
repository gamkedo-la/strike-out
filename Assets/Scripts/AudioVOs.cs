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
    [Header("Default VO")]
    public AudioData defaultData;
    public AudioData pitchVO;
    public AudioData enemyAtkVO;
    public AudioData playerTakeDmgVO;
    public AudioData EnemyTakeDmgVO;

    [Header("Player Attack Sounds")]
    public AudioData FastballVO;
    public AudioData SliderVO;
    public AudioData CurveballVO;
    public AudioData ChangeUpVO;

    [Header("Player Attack Config")]
    public float VODelay;

    [Header("Enemy Attack Sounds")]
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

    public void PlayFastBallVO()
    {
        StartCoroutine(PlayAfterDelay(VODelay, FastballVO));
        //controller.PlayRandom(FastballVO);
    }

    public void PlayCurveballVO()
    {
        StartCoroutine(PlayAfterDelay(VODelay, CurveballVO));
        //controller.PlayRandom(FastballVO);
    }

    public void PlaySliderVO()
    {
        StartCoroutine(PlayAfterDelay(VODelay, SliderVO));
        //controller.PlayRandom(FastballVO);
    }

    public void PlayChangeupVO()
    {
        StartCoroutine(PlayAfterDelay(VODelay, ChangeUpVO));
        //controller.PlayRandom(FastballVO);
    }
}
