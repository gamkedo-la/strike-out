using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public enum AudioOutputGroup { Music, Sounds, UI }

[CreateAssetMenu()]
public class AudioData : ScriptableObject
{
    public AudioClip Clip;
    public List<AudioClip> Sounds = new List<AudioClip>();
    
    public AudioOutputGroup output;

    public bool Loop = false;

    [Range(0, 1)]
    public float Volume = 1f;

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

    public float GetRandomPitch()
    {
        return AudioUtils.St2pitch(Random.Range(Pitch - RandomPitch, Pitch + RandomPitch));
    }

    public string GetOutputGroup(AudioOutputGroup group)
    {
        switch(group)
        {
            case AudioOutputGroup.Music:
                return "Music";

            case AudioOutputGroup.Sounds:
                return "Sounds";

            case AudioOutputGroup.UI:
                return "UI";

            default:
                return "Sounds";
        }
    }

    public AudioClip GetRandomClip()
    {
        if(Sounds.Count == 0)
        {
            Debug.LogWarning("AudioData does not contain any AudioClips.");
            return null;
        }

        int index = 0;
        if(Sounds.Count > 1)
        {
            index = Random.Range(0, Sounds.Count - 1);
        }

        return Sounds[index];
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
