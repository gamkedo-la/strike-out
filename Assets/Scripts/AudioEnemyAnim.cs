using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnemyAnim : AudioEventGeneric
{
    public AudioData whoosh;
    public AudioData mittPop;
    public AudioData prep;
    public AudioData downed;
    public AudioData downedBatFall;
    public AudioData swingMiss;
    public AudioData swingDizzy;
    public AudioData swingDizzyBirds;

    void BatWhoosh()
    {
        controller.PlayRandom(whoosh);
    }

    void BatHit()
    {
        controller.PlayRandom(sound);
    }

    void MittPop()
    {
        controller.PlayRandom(mittPop);
    }

    public void Downed()
    {
        controller.PlayRandom(downed);
    }

    public void Prep()
    {
        controller.PlayRandom(prep);
    }

    public void SwingMiss()
    {
        controller.PlayRandom(swingMiss);
    }

    public void SwingDizzy()
    {
        SwingMiss();
        controller.PlayRandom(swingDizzy);
        controller.PlayRandom(swingDizzyBirds);
    }
}
