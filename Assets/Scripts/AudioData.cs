﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioOutputGroup { Music, Sounds, UI }

[CreateAssetMenu()]
public class AudioData : ScriptableObject
{
    public AudioClip Clip;
    public List<AudioClip> Sounds = new List<AudioClip>();

    public AudioMixer mixer;
    public AudioOutputGroup output;

    public bool Loop = false;

    [Range(-80, 0.0001f)]
    public float Volume;

    [Range(-24, 24)]
    public float Pitch = 0f;

    [Range(0f, 1f)]
    public float SpatialBlend = 1f;

    [Range(-20, 0)]
    public float RandomVolume = 0f;

    [Range(0, 12)]
    public float RandomPitch = 0f;

    public float GetRandomVol()
    {
        return AudioUtils.DbToLinear(Random.Range(Volume + RandomVolume, 0));
    }

    public float GetVol()
    {
        return AudioUtils.DbToLinear(Volume);
    }

    public float GetRandomPitch()
    {
        return AudioUtils.St2pitch(Random.Range(Pitch - RandomPitch, Pitch + RandomPitch));
    }

    public AudioMixerGroup GetOutputGroup()
    {
        string group;

        switch (output)
        {
            case AudioOutputGroup.Music:
                group = "Music";
                break;
            case AudioOutputGroup.Sounds:
                group = "Sounds";
                break;
            case AudioOutputGroup.UI:
                group = "UI";
                break;
            default:
                group = "Sounds";
                break;
        }

        if (mixer != null)
        {
            return mixer.FindMatchingGroups(group)[0];
        }
        else
        {
            Debug.Log("No Mixer?");
            return null;
        }
    }

    public AudioClip GetRandomClip()
    {
        if (Sounds.Count == 0)
        {
            if (Clip != null)
            {
                return Clip;
            }
            else
            {
                Debug.LogWarning("AudioData does not contain any AudioClips.");
                return null;
            }
        }

        int index = 0;
        if (Sounds.Count > 1)
        {
            index = Random.Range(0, Sounds.Count - 1);
        }

        return Sounds[index];
    }

    public AudioClip GetRandomOutsideClip(List<AudioClip> other)
    {
        if (other.Count == 0)
        {
            if (Clip != null)
            {
                return Clip;
            }
            else
            {
                Debug.LogWarning("AudioData does not contain any AudioClips.");
                return null;
            }
        }

        int index = 0;
        if (other.Count > 1)
        {
            index = Random.Range(0, other.Count - 1);
        }

        return other[index];
    }

    //public AudioSourceController Play()
    //{
    //    AudioSourceController controller = AudioPoolManager.Instance.GetController();
    //    controller.SetSourceProperties(GetClip(), Volume, Pitch, Loop, SpacialBlend);
    //    controller.Play();
    //    return controller;
    //}

    //public AudioSourceController Play(Vector3 position)
    //{
    //    AudioSourceController controller = AudioPoolManager.Instance.GetController();
    //    controller.SetSourceProperties(GetClip(), Volume, Pitch, Loop, SpacialBlend);
    //    controller.SetPosition(position);
    //    controller.Play();
    //    return controller;
    //}

    //public AudioSourceController PlayRandom(Vector3 position)
    //{
    //    AudioSourceController controller = AudioPoolManager.Instance.GetController();

    //    controller.SetSourceProperties(GetClip(), GetRandomVol(), GetRandomPitch(), Loop, SpacialBlend);

    //    controller.SetPosition(position);
    //    controller.Play();
    //    return controller;
    //}
}
