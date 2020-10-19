using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnemyAnim : AudioEventGeneric
{
    public BattleSystemMultiple currentBattle;
    public AudioData whoosh;
    public AudioData mittPop;
    public AudioData prep;
    public AudioData downed;
    public AudioData downedBatFall;
    public AudioData swingMiss;
    public AudioData swingDizzy;
    public AudioData swingDizzyBirds;
    public AudioData takeDmg;

    private void Start()
    {
        currentBattle = FindObjectOfType<BattleSystemMultiple>();
    }

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

    public void DownedBatFall()
    {
        controller.PlayRandom(downedBatFall);
    }

    public void Prep()
    {
        //if (currentBattle != null)
        //{
        //    if (currentBattle.state == BattleStateMultiple.ENEMYTURN)
        //        controller.PlayRandom(prep);
        //}
    }

    public void SwingMiss()
    {
        controller.PlayRandom(swingMiss);
    }

    public void SwingDizzy()
    {
        controller.PlayRandom(swingDizzy);
        controller.PlayRandom(swingDizzyBirds);
    }

    public void TakeDmg()
    {
        if (takeDmg != null)
            controller.PlayRandom(takeDmg);
        else
            Debug.LogWarning("No take damage audio data!");
        
    }
}
