using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    public AudioSource source;
    private Vector3 position;

    void Awake()
    {
        if(source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        position = this.transform.position;
    }

    public void SetSourceOutput(AudioData data)
    {
        source.outputAudioMixerGroup = data.GetOutputGroup();
    }

    public void SetSourceProperties(AudioData data)
    {
        source.clip = data.Clip;
        source.volume = data.GetVol();
        source.pitch = data.Pitch;
        source.loop = data.Loop;
        source.spatialBlend = data.SpatialBlend;
        //SetSourceOutput(data);
    }

    public void SetRandomProperties(AudioData data)
    {
        source.clip = data.GetRandomClip();
        source.volume = data.GetRandomVol();
        source.pitch = data.GetRandomPitch();
        source.loop = data.Loop;
        source.spatialBlend = data.SpatialBlend;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Play(AudioData data)
    {
        SetSourceProperties(data);
        source.Play();
        //Debug.Log("Playing Sound");
    }

    public void PlayRandom(AudioData data)
    {
        SetRandomProperties(data);
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}
