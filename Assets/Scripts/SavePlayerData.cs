using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayerData 
{
    public float money;
    public int StarterFast, StarterSlid, StarterCurve, StarterChange, StarterAgil;
    public int MidRelieverFast, MidRelieverSlid, MidRelieverCurve, MidRelieverChange, MidRelieverAgil;
    public int SetUpFast, SetUpSlid, SetUpCurve, SetUpChange, SetUpAgil;
    public int CloserFast, CloserSlid, CloserCurve, CloserChange, CloserAgil;
    public int StarterLevel, MiddleLevel, SetUpLevel, CloserLevel;
    public float StarterMorale, StarterEnergy, StarterMoraleMax, StarterEnergyMax;
    public float MidRelieverMorale, MidRelieverEnergy, MiddleMoraleMax, MiddleEnergyMax;
    public float SetUpMorale, SetUpEnergy, SetUpMoraleMax, SetUpEnergyMax;
    public float CloserMorale, CloserEnergy, CloserMoraleMax, CloserEnergyMax;
    public bool Minor1Unlocked, Minor2Unlocked, Minor3Unlocked, Minor4Unlocked, Minor5Unlocked, Minor6Unlocked, Minor7Unlocked, Minor8Unlocked;
    public bool Major1Unlocked, Major2Unlocked, Major3Unlocked, Major4Unlocked, Major5Unlocked, Major6Unlocked, Major7Unlocked, Major8Unlocked;
    
    //dump player into the Training area when loaded
    //items
    //
    public bool HoEUnlocked; //located in TVTurnOn in TrainingArea, move this when done
    //

    public SavePlayerData(GameManager gameManager)
    {
     
    }
}
