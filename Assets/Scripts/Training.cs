using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{

    public GameObject mainCam, TVCam;

    public static bool LevelSelectTV;

    private void Start()
    {
       // GameManager.Money += 10000;

        GameManager.StarterMorale = GameManager.StarterMoraleMax;
        GameManager.MidRelivMorale = GameManager.MidRelivMoraleMax;
        GameManager.SetUpMorale = GameManager.SetUpMoraleMax;
        GameManager.CloserMorale = GameManager.CloserMoraleMax;

        GameManager.StarterEnergy = GameManager.StarterEnergyMax;
        GameManager.MidRelivEnergy = GameManager.MidRelievEnergyMax;
        GameManager.SetUpEnergy = GameManager.SetUpEnergyMax;
        GameManager.CloserEnergy = GameManager.CloserEnergyMax;

        LevelSelectTV = false;

        ConcourseGameManager.AnnouncerKilled = false;
        ConcourseGameManager.AnnouncerHasAlreadyBeenKilled = false;
        ConcourseGameManager.McGeeKilled = false;
        ConcourseGameManager.McGeeHasAlreadyBeenKilled = false;

        HOEGameManager.redToggle = false;
        HOEGameManager.greenToggle = false;

        HOEGameManager.UmpireDefeated = false;

        HOEGameManager.plaqueArea = true;
        HOEGameManager.umpArea = false;
        HOEGameManager.displayArea = false;
        HOEGameManager.cornfieldArea = false;

        KeyConcourse.gateHasBeenOpened = false;

        GameManager.i1 = false;
        GameManager.i2 = false;
        GameManager.i3 = false;
        GameManager.i4 = false;
        GameManager.i5 = false;
        GameManager.i6 = false;
        GameManager.i7 = false;
        GameManager.i8 = false;
        GameManager.i9 = false;
        GameManager.i10 = false;
        GameManager.i11 = false;
        GameManager.i12 = false;
        GameManager.i13 = false;
        GameManager.i14 = false;
        GameManager.i15 = false;
        GameManager.i16 = false;
        GameManager.i17 = false;
        GameManager.i18 = false;
        GameManager.i19 = false;
        GameManager.i20 = false;
        GameManager.i21 = false;
        GameManager.i22 = false;
        GameManager.i23 = false;
        GameManager.i24 = false;
        GameManager.i25 = false;

        ConcourseGameManager.elevatorUnlocked = false;

        PlayerLocationDontDestroy.isStarting = true;
    }

    private void Update()
    {
        if (LevelSelectTV)
        {
            TVCam.SetActive(true);
            mainCam.SetActive(false);
        }

        if (!LevelSelectTV)
        {
            TVCam.SetActive(false);
            mainCam.SetActive(true);
        }
    }
}
