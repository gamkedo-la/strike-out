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


  /*  public void SetHUD(Unit unit)
    {
        HealthSlider.value = unit.currentHP / unit.maxHP;
    }
    */
    public void Start()
    {
        if (isEnemy)
        {
            currentHP = maxHP;
        }

        if (DescText != null)
        {
            DescText.text = "";
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
            return true;
        }

        else
        {
            if (FastballMultiplier == .5f)
            {
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (FastballMultiplier == 2)
            {
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
            
            return true;
        }

        else
        {
            if (SliderMultiplier == .5f)
            {
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (SliderMultiplier == 2)
            {
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

            return true;
        }

        else
        {
            if (CurveballMultiplier == .5f)
            {
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (CurveballMultiplier == 2)
            {
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

            return true;
        }

        else
        {
            if (ChangeUpMultiplier == .5f)
            {
                DescText.text = "Resist!";
                StartCoroutine(ClearText());
            }
            if (ChangeUpMultiplier == 2)
            {
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
