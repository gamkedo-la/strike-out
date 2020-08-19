using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    public AudioSource source;
    private Vector3 position;
    //public AudioClip Clip;

    //public bool Loop = false;

    //[Range(0, 1)]
    //public float Volume = 1f;

    //[Range(.25f, 3)]
    //public float Pitch = 1f;

    //[Range(0f, 1f)]
    //public float SpacialBlend = 1f;

    //private Transform transform;

    void Awake()
    {
        if(source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        position = this.transform.position;
    }

    public void SetSourceProperties(AudioData data)
    {
        source.clip = data.GetRandomClip();
        source.volume = data.Volume;
        source.pitch = data.Pitch;
        source.loop = data.Loop;
        source.spatialBlend = data.SpatialBlend;
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
