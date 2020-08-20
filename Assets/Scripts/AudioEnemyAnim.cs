using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnemyAnim : AudioEventGeneric
{
    public AudioData whoosh;
    public AudioData mittPop;

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
}
