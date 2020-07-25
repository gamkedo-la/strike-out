using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    //public Slider stamSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl: " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

      //  stamSlider.maxValue = unit.maxStamina;
      //  stamSlider.value = unit.currentStamina;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetStam(int stam)
    {
      //  stamSlider.value = stam;
    }
}
