using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public float currentXP, targetXP = 5, level = 1;

    public static XPManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
      //  currentXPtext.text = currentXP.ToString();
     //   targetXPtext.text = currentXP.ToString();
      //  levelText.text = currentXP.ToString();
    }

    public void AddXP(int xp)
    {
        currentXP += xp;

        //Level Up
        while (currentXP >= targetXP)
        {
            currentXP = currentXP - targetXP;
            level++;
            targetXP *= 1.025f;
            //add training points
           // levelText.text = level.ToString();
           // targetXPtext.text = targetXP.ToString();
        }

       // currentXPtext.text = currentXP.ToString("F0");
    }
}
