using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int enemyDamage;

    public int maxHP;
    public int currentHP;

    public int maxStamina;
    public int currentStamina;

    public int ExperienceToDistribute;
    public int minExperience;
    public int maxExperience;

    public int MoneyToDistribute;
    public int minMoney;
    public int maxMoney;

    public void Start()
    {
        ExperienceToDistribute = Random.Range(minExperience, maxExperience);
        MoneyToDistribute = Random.Range(minMoney, maxMoney);
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
