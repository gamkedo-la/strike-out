using System;
using UnityEngine;

public enum AnimEvent { Windup, Swing, Oof, Dizzy, Miss };
public class AudioOnAnim : MonoBehaviour
{
    public static Action<AnimEvent> audioTrigger;

    public AudioData sound;
    public AudioData dmg;
    public AudioData prep;
    public AudioData downed;
    public AudioSourceController controller;
    public AudioVOs VO;
    public bool suppressVO;

    void Start()
    {

        if (controller == null)
            controller = gameObject.AddComponent<AudioSourceController>();

        controller.SetSourceOutput(sound);

        VO = FindObjectOfType<AudioVOs>();
    }

    public void PitchSound()
    {
        controller.PlayRandom(sound);
        // Debug.Log("Pitch Sound!");
        //if (VO != null && !suppressVO)
        //    VO.StartCoroutine(VO.PlayAfterDelay(1f, VO.pitchVO));
    }

    public void TakeDmg()
    {
        controller.PlayRandom(dmg);
        //Debug.Log("Dmg Sound!");
        //if (VO != null)
        //    VO.StartCoroutine(VO.PlayAfterDelay(1, VO.playerTakeDmgVO));
    }

    public void Downed()
    {
        controller.PlayRandom(downed);

        if (VO != null)
            VO.PlayAfterDelay(1, VO.playerTakeDmgVO);
    }

    public void Prep()
    {
        controller.PlayRandom(prep);
    }
}
