using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolManager
{
    private static AudioPoolManager instance;
    public static AudioPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("Instantiating AudioPoolManager.");
                instance = new AudioPoolManager();
            }
            return instance;
        }
    }

    private List<AudioSourceController> pool = new List<AudioSourceController>();

    public AudioSourceController GetController()
    {
        AudioSourceController output = null;

        if(pool.Count > 0)
        {
            output = pool[0];
            pool.Remove(output);
            return output;
        }
        else
        {
            GameObject go = new GameObject("AudioController");
            output = go.AddComponent<AudioSourceController>();
            return output;
        }
    }

    public void ReturnController(AudioSourceController controller)
    {
        if (pool.Contains(controller) == false)
            pool.Add(controller);
    }

    void Start()
    {
        
    }
}
