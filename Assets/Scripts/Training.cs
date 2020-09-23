using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{

    public GameObject mainCam, TVCam;

    public static bool LevelSelectTV;

    private void Start()
    {
        ConcourseGameManager.AnnouncerKilled = false;
        ConcourseGameManager.AnnouncerHasAlreadyBeenKilled = false;
        ConcourseGameManager.McGeeKilled = false;
        ConcourseGameManager.McGeeHasAlreadyBeenKilled = false;

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
