using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSourceController controller;
    public AudioData[] StartMusic;
    public AudioData[] LoopMusic;
    public AudioData OutMusic;

    private bool playIntro = false;

    public int bpm;
    public int lengthOfStartBars;
    private double nextStartTime;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<AudioSourceController>();

        if (StartMusic.Length > 0)
            playIntro = true;
    }

    void Start()
    {
        nextStartTime = AudioSettings.dspTime + 0.1;

        if (playIntro)
        {
            Debug.LogWarning("playIntro");
            foreach (AudioData data in StartMusic)
            {
                //controller.SetSourceOutput(data);
                controller.PlayScheduled(data, nextStartTime);
            }

            nextStartTime += 60.0f / bpm * (lengthOfStartBars * 4);
        }

        foreach (AudioData data in LoopMusic)
        {
            //controller.SetSourceOutput(data);
            controller.PlayScheduled(data, nextStartTime);
            Debug.LogWarning("scheduling loop");
        }
    }

    void Update()
    {

    }
}
