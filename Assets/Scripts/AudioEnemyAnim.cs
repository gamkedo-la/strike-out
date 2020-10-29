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
    public AudioData leadup;

    private void Start()
    {
        currentBattle = FindObjectOfType<BattleSystemMultiple>();

        if (controller == null)
            controller = gameObject.AddComponent<AudioSourceController>();
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
        if (mittPop)
            controller.PlayRandom(mittPop);
        else
        {
            Debug.LogError("No Mitt Pop Sound");
        }
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
        if (swingMiss)
            controller.PlayRandom(swingMiss);
        else
        {
            Debug.LogError("No Swing Miss Sound");
        }
    }

    public void SwingDizzy()
    {
        if (swingDizzy)
        {
            controller.PlayRandom(swingDizzy);
            controller.PlayRandom(swingDizzyBirds);
        }
        else
        {
            Debug.LogError("No Swing Dizzy Sound");
        }
    }

    public void TakeDmg()
    {
        if (takeDmg != null)
            controller.PlayRandom(takeDmg);
        else
            Debug.LogWarning("No take damage audio data!");

    }

    public void LeadUp()
    {
        if (leadup)
        {
            if (currentBattle)
                controller.PlayRandom(leadup);
        }
    }
}
