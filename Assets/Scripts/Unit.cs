using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string unitName;
    public BattleSystemMultiple.CharacterIdentifier myEnumValue;
    public int unitLevel;

    public int fastballDamage;
    public int sliderDamage;
    public int curveballDamage;
    public int changeupDamage;

    public int fastballStamina;
    public int sliderStamina;
    public int curveballStamina;
    public int changeupStamina;

    public float FastballMultiplier = 1;
    public float SliderMultiplier = 1;
    public float CurveballMultiplier = 1;
    public float ChangeUpMultiplier = 1;

    int minDamage, maxDamage;
    public int enemyDamage;
    public string attackName;

    public int maxHP;
    public float currentHP;

    public int maxStamina;
    public int currentStamina;

    public int ExperienceToDistribute;
    public int minExperience;
    public int maxExperience;

    public int MoneyToDistribute;
    public int minMoney;
    public int maxMoney;

    public bool isEnemy;
    public Slider HealthSlider;
    public Text DescText;

    public bool isDizzy;
    public Animator anim;

    public bool m1, m2, m3, m4, m5, m6, m7, m8;
    public bool M1, M2, M3, M4, M5, M6, M7, M8;
    public GameObject StrWeakHolder;

    public static bool attackAll;

    public Text DamageUI;

    //enemyAttack
    public int minAttackAvil, maxAttackAvil;
    int attackToDo;

    /*  public void SetHUD(Unit unit)
      {
          HealthSlider.value = unit.currentHP / unit.maxHP;
      }
      */
    public void Start()
    {
        #region Minor
        if (m1 && GameManager.m1)
        {
            StrWeakHolder.SetActive(true);
        }
        if (m2 && GameManager.m2)
        {
            StrWeakHolder.SetActive(true);
        }
        if (m3 && GameManager.m3)
        {
            StrWeakHolder.SetActive(true);
        }
        if (m4 && GameManager.m4)
        {
            StrWeakHolder.SetActive(true);
        }
        if (m5 && GameManager.m5)
        {
            StrWeakHolder.SetActive(true);
        }
        if (m6 && GameManager.m6)
        {
            StrWeakHolder.SetActive(true);
        }
        if (m7 && GameManager.m7)
        {
            StrWeakHolder.SetActive(true);
        }
        if (m8 && GameManager.m8)
        {
            StrWeakHolder.SetActive(true);
        }

        if (m1 && !GameManager.m1)
        {
            StrWeakHolder.SetActive(false);
        }
        if (m2 && !GameManager.m2)
        {
            StrWeakHolder.SetActive(false);
        }
        if (m3 && !GameManager.m3)
        {
            StrWeakHolder.SetActive(false);
        }
        if (m4 && !GameManager.m4)
        {
            StrWeakHolder.SetActive(false);
        }
        if (m5 && !GameManager.m5)
        {
            StrWeakHolder.SetActive(false);
        }
        if (m6 && !GameManager.m6)
        {
            StrWeakHolder.SetActive(false);
        }
        if (m7 && !GameManager.m7)
        {
            StrWeakHolder.SetActive(false);
        }
        if (m8 && !GameManager.m8)
        {
            StrWeakHolder.SetActive(false);
        }
        #endregion
        #region Major
        if (M1 && GameManager.M1)
        {
            StrWeakHolder.SetActive(true);
        }
        if (M2 && GameManager.M2)
        {
            StrWeakHolder.SetActive(true);
        }
        if (M3 && GameManager.M3)
        {
            StrWeakHolder.SetActive(true);
        }
        if (M4 && GameManager.M4)
        {
            StrWeakHolder.SetActive(true);
        }
        if (M5 && GameManager.M5)
        {
            StrWeakHolder.SetActive(true);
        }
        if (M6 && GameManager.M6)
        {
            StrWeakHolder.SetActive(true);
        }
        if (M7 && GameManager.M7)
        {
            StrWeakHolder.SetActive(true);
        }
        if (M8 && GameManager.M8)
        {
            StrWeakHolder.SetActive(true);
        }

        if (M1 && !GameManager.M1)
        {
            StrWeakHolder.SetActive(false);
        }
        if (M2 && !GameManager.M2)
        {
            StrWeakHolder.SetActive(false);
        }
        if (M3 && !GameManager.M3)
        {
            StrWeakHolder.SetActive(false);
        }
        if (M4 && !GameManager.M4)
        {
            StrWeakHolder.SetActive(false);
        }
        if (M5 && !GameManager.M5)
        {
            StrWeakHolder.SetActive(false);
        }
        if (M6 && !GameManager.M6)
        {
            StrWeakHolder.SetActive(false);
        }
        if (M7 && !GameManager.M7)
        {
            StrWeakHolder.SetActive(false);
        }
        if (M8 && !GameManager.M8)
        {
            StrWeakHolder.SetActive(false);
        }
        #endregion
        DamageUI.text = "";
        if (isEnemy)
        {
            currentHP = maxHP;
        }

        if (DescText != null)
        {
            DescText.text = "";
        }

        if (anim == null)
        {
            return;
        }

        ExperienceToDistribute = Random.Range(minExperience, maxExperience);
        MoneyToDistribute = Random.Range(minMoney, maxMoney);

        //Remove This later
        currentHP = 1;
    }

    void Update()
    {
        //  currentHP -= 5 * Time.deltaTime;
        if (isEnemy)
        {
            HealthSlider.value = currentHP / maxHP;
        }
    }

    public void DetermineAttack()
    {
        attackToDo = Random.Range(minAttackAvil, maxAttackAvil);

        if (attackToDo == 0)
        { WeakGrounder(); }

        if (attackToDo == 1)
        { SeeingEye(); }

        if (attackToDo == 2)
        { PopUp(); }

        if (attackToDo == 3)
        { LineDrive(); }

        if (attackToDo == 4)
        { Shagging(); }

        if (attackToDo == 5)
        { GroundRule(); }

        if (attackToDo == 6)
        { TakingPitch(); }

        if (attackToDo == 7)
        { DeepFoul(); }

        if (attackToDo == 8)
        { CircusPlay(); }

        if (attackToDo == 9)
        { Grandstanding(); }

        if (attackToDo == 10)
        { TheCall(); }

        if (attackToDo == 11)
        { DucksOnPond(); }

        if (attackToDo == 12)
        { SouvenirDay(); }
    }
    
    public bool TakeDamage(int dmg)
    {
        print(dmg);

        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }

        else
            return false;
    }

    public bool TakeDamageFast(int dmg)
    {
        currentHP -= (dmg * FastballMultiplier);
        DamageUI.text = "-" + dmg.ToString();
        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
            StartCoroutine(ClearText());
            return true;
        }

        else
        {
            if (FastballMultiplier == .5f)
            {
                anim.Play("Armature|Swing");
                DescText.text = "Resist!";
            }
            if (FastballMultiplier == 1f)
            {
                anim.Play("Armature|SwingMiss");
            }
            if (FastballMultiplier == 2)
            {
                anim.Play("Armature|SpinDizzy");
                DescText.text = "Weak!";
            }
            StartCoroutine(ClearText());
            return false;
        }
    }


    public bool TakeDamageSlid(int dmg)
    {
        currentHP -= (dmg * SliderMultiplier);
        DamageUI.text = "-" + dmg.ToString();
        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
            StartCoroutine(ClearText());
            return true;
        }

        else
        {
            if (SliderMultiplier == .5f)
            {
                anim.Play("Armature|Swing");
                DescText.text = "Resist!";
            }
            if (SliderMultiplier == 1f)
            {
                anim.Play("Armature|SwingMiss");
            }
            if (SliderMultiplier == 2)
            {
                anim.Play("Armature|SpinDizzy");
                DescText.text = "Weak!";
            }
            StartCoroutine(ClearText());
            return false;
        }
    }

    public bool TakeDamageCurve(int dmg)
    {
        currentHP -= (dmg * CurveballMultiplier);
        DamageUI.text = "-" + dmg.ToString();

        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
            StartCoroutine(ClearText());
            return true;
        }

        else
        {
            if (CurveballMultiplier == .5f)
            {
                anim.Play("Armature|Swing");
                DescText.text = "Resist!";
            }
            if (CurveballMultiplier == 1f)
            {
                anim.Play("Armature|SwingMiss");
            }
            if (CurveballMultiplier == 2)
            {
                anim.Play("Armature|SpinDizzy");
                DescText.text = "Weak!";
            }
            StartCoroutine(ClearText());
            return false;
        }
    }

    public bool TakeDamageChange(int dmg)
    {
        currentHP -= (dmg * ChangeUpMultiplier);
        DamageUI.text = "-" + dmg.ToString();
        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
            StartCoroutine(ClearText());
            return true;
        }

        else
        {
            if (ChangeUpMultiplier == .5f)
            {
                anim.Play("Armature|Swing");
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (ChangeUpMultiplier == 1f)
            {
                anim.Play("Armature|SwingMiss");
                StartCoroutine(ClearText());
            }
            if (ChangeUpMultiplier == 2)
            {
                anim.Play("Armature|SpinDizzy");
                DescText.text = "Weak!";
                StartCoroutine(ClearText());
            }
            StartCoroutine(ClearText());
            return false;
        }
    }

    public bool SpendStamina(int dmg)
    {
        currentStamina -= dmg;

        if (currentStamina <= 0)
        {
            return true;
        }

        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }

    void WeakGrounder()
    {
        minDamage = 1;
        maxDamage = 5;

        enemyDamage = Random.Range(minDamage, maxDamage);
      //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Weak Grounder".ToString();
    }

    void SeeingEye()
    {
        minDamage = 2;
        maxDamage = 4;

        enemyDamage = Random.Range(minDamage, maxDamage);
      //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Seeing Eye Single".ToString();
    }

    void PopUp()
    {
        minDamage = 3;
        maxDamage = 4;

        enemyDamage = Random.Range(minDamage, maxDamage);
      //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Pop Up".ToString();
    }

    void LineDrive()
    {
        minDamage = 4;
        maxDamage = 6;

        enemyDamage = Random.Range(minDamage, maxDamage);
      //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Line Drive".ToString();
    }

    void FlyBall()
    {
        minDamage = 4;
        maxDamage = 7;

        enemyDamage = Random.Range(minDamage, maxDamage);
       // DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Fly Ball".ToString();
    }

    public bool Shagging()
    {
        attackAll = true;
        minDamage = 3;
        maxDamage = 5;

        enemyDamage = Random.Range(minDamage, maxDamage);
      //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Shagging".ToString();

        if (currentHP >= 0)
        {
            return true;
        }

        else
            return false;
    }

    void GroundRule()
    {
        minDamage = 10;
        maxDamage = 15;

        enemyDamage = Random.Range(minDamage, maxDamage);
      //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Ground Rule Double".ToString();
    }

    void TakingPitch()
    {
        minDamage = 0;
        maxDamage = 0;

        enemyDamage = Random.Range(minDamage, maxDamage);
       // DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Taking The Pitch".ToString();
    }

    void DeepFoul()
    {
        minDamage = 5;
        maxDamage = 9;

        enemyDamage = Random.Range(minDamage, maxDamage);
       // DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Deep Foul Ball".ToString();
    }

    void CircusPlay()
    {
        minDamage = 7;
        maxDamage = 12;

        enemyDamage = Random.Range(minDamage, maxDamage);
        // DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Circus Play".ToString();
    }

    void Grandstanding()
    {
        minDamage = 13;
        maxDamage = 17;

        enemyDamage = Random.Range(minDamage, maxDamage);
        // DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Grandstanding".ToString();
    }

    void TheCall()
    {
        minDamage = 10;
        maxDamage = 13;

        enemyDamage = Random.Range(minDamage, maxDamage);
        // DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "The Call".ToString();
    }

    public bool DucksOnPond()
    {
        attackAll = true;
        minDamage = 10;
        maxDamage = 15;

        enemyDamage = Random.Range(minDamage, maxDamage);
        //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Ducks On The Pond".ToString();

        if (currentHP >= 0)
        {
            return true;
        }

        else
            return false;
    }

    public bool SouvenirDay()
    {
        attackAll = true;
        minDamage = 15;
        maxDamage = 25;

        enemyDamage = Random.Range(minDamage, maxDamage);
        //  DamageUI.text = "-" + enemyDamage.ToString();
        attackName = "Souvenir Day".ToString();

        if (currentHP >= 0)
        {
            return true;
        }

        else
            return false;
    }

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        DamageUI.text = "".ToString();
        DescText.text = "";
    }
}
