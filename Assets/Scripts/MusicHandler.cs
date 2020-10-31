using UnityEngine;
using UnityEngine.Audio;

public class MusicHandler : MonoBehaviour
{
    public AudioSourceController controller;
    public AudioData[] StartMusic;
    public AudioData[] LoopMusic;
    public AudioData OutMusic;

    [SerializeField]
    private bool playIntro = false;

    public int bpm;
    public int lengthOfStartBars;

    private double nextStartTime;

    [Header("Snapshot Info")]
    public AudioMixer mixer;
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot transition;
    public AudioMixerSnapshot thisScene;
    public bool useSnapshot;


    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<AudioSourceController>();

        if (StartMusic.Length > 0)
            playIntro = true;
    }

    void Start()
    {
        if (useSnapshot)
        { thisScene.TransitionTo(1f); }
        else
        { normal.TransitionTo(2f); }

        nextStartTime = AudioSettings.dspTime + 1f;
        //Debug.LogWarning(nextStartTime);

        if (playIntro)
        {
            //Debug.LogWarning("playIntro");
            foreach (AudioData data in StartMusic)
            {
                //controller.SetSourceOutput(data);
                controller.PlayScheduled(data, nextStartTime);
            }

            nextStartTime += 60.0f / bpm * (lengthOfStartBars * 4);
            //Debug.LogWarning(nextStartTime);
        }

        foreach (AudioData data in LoopMusic)
        {
            //controller.SetSourceOutput(data);
            controller.PlayScheduled(data, nextStartTime);
            //Debug.LogWarning("scheduling loop");
        }
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        nextStartTime = AudioSettings.dspTime + 1f;
        transition.TransitionTo(1f);
        //Debug.LogWarning(nextStartTime);
    }
}
