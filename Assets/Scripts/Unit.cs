using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string unitName;
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

    public int enemyDamage;

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
    }

    void Update()
    {
        //  currentHP -= 5 * Time.deltaTime;
        if (isEnemy)
        {
            HealthSlider.value = currentHP / maxHP;
        }
    }
    
    public bool TakeDamage(int dmg)
    {
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

        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
            return true;
        }

        else
        {
            if (FastballMultiplier == .5f)
            {
                anim.Play("Armature|Swing");
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (FastballMultiplier == 1f)
            {
                anim.Play("Armature|SwingMiss");

                StartCoroutine(ClearText());
            }
            if (FastballMultiplier == 2)
            {
                anim.Play("Armature|SpinDizzy");
                DescText.text = "Weak!";
                StartCoroutine(ClearText());
            }

            return false;
        }
    }


    public bool TakeDamageSlid(int dmg)
    {
        currentHP -= (dmg * SliderMultiplier);

        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
            return true;
        }

        else
        {
            if (SliderMultiplier == .5f)
            {
                anim.Play("Armature|Swing");
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (SliderMultiplier == 1f)
            {
                anim.Play("Armature|SwingMiss");
                StartCoroutine(ClearText());
            }
            if (SliderMultiplier == 2)
            {
                anim.Play("Armature|SpinDizzy");
                DescText.text = "Weak!";
                StartCoroutine(ClearText());
            }

            return false;
        }
    }

    public bool TakeDamageCurve(int dmg)
    {
        currentHP -= (dmg * CurveballMultiplier);


        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
            return true;
        }

        else
        {
            if (CurveballMultiplier == .5f)
            {
                anim.Play("Armature|Swing");
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (CurveballMultiplier == 1f)
            {
                anim.Play("Armature|SwingMiss");
                StartCoroutine(ClearText());
            }
            if (CurveballMultiplier == 2)
            {
                anim.Play("Armature|SpinDizzy");
                DescText.text = "Weak!";
                StartCoroutine(ClearText());
            }
            return false;
        }
    }

    public bool TakeDamageChange(int dmg)
    {
        currentHP -= (dmg * ChangeUpMultiplier);

        if (currentHP <= 0)
        {
            anim.Play("Armature|Downed");
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


    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        DescText.text = "";
    }
}
