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

    public float FastballMultiplier;
    public float SliderMultiplier;
    public float CurveballMultiplier;
    public float ChangeUpMultiplier;

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
}
