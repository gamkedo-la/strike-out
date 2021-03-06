﻿using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    public AudioSource source;
    public List<AudioSource> sources = new List<AudioSource>();
    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private int maxSources = 5;
    private Vector3 position;

    void Awake()
    {
        //if(source == null)
        //{
        //    CreateNewSource();
        //}

        for (int i = 0; i < maxSources; ++i)
        {
            CreateNewSource();
        }

        position = this.transform.position;
    }

    private void CreateNewSource()
    {
        source = gameObject.AddComponent<AudioSource>();
        sources.Add(source);
    }

    private AudioSource GetNextSource()
    {
        if (currentIndex < sources.Count)
        {
            if (source.isPlaying == false)
            {
                source = sources[currentIndex];
                return source;
            }
            else
            {
                //currentIndex += 1;
                IncrementIndex();
                source = sources[currentIndex];
                return source;
                //return GetNextSource();
            }
        }
        else if (sources.Count < maxSources)
        {
            CreateNewSource();
            return source;
        }

        IncrementIndex();
        source = sources[currentIndex];
        return source;
        //return sources[currentIndex];
    }

    public void SetSourceOutput(AudioData data)
    {
        source.outputAudioMixerGroup = data.GetOutputGroup();
    }

    public void SetSourceProperties(AudioData data)
    {
        source.clip = data.GetRandomClip();
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

    public void SetRandomProperties(AudioData data, List<AudioClip> sounds)
    {
        source.clip = data.GetRandomOutsideClip(sounds);
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
        source = GetNextSource();
        SetSourceOutput(data);
        SetRandomProperties(data);
        source.Play();
        // IncrementIndex();
    }

    public void PlayRandomRead(AudioData data, List<AudioClip> sounds)
    {
        source = GetNextSource();
        SetSourceOutput(data);
        SetRandomProperties(data, sounds);
        source.Play();
        // IncrementIndex();
    }

    public void PlayScheduled(AudioData data, double time)
    {
        source = GetNextSource();
        SetSourceOutput(data);
        SetRandomProperties(data);
        //SetSourceProperties(data);
        source.PlayScheduled(time);
        //IncrementIndex();
    }

    private void IncrementIndex()
    {
        //Debug.LogWarning("Increment Index");
        currentIndex = (currentIndex + 1) % maxSources;
    }

    public void Stop()
    {
        source.Stop();
    }

    public void StopAll()
    {
        foreach (AudioSource source in sources)
        {
            source.Stop();
        }
    }

    //TODO add fade out and stop function
}
