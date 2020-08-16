using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //morale and energy
    public static float starterMorale, midRelivMorale, setUpMorale, closerMorale;
    public static float StarterEnergy, MidRelivEnergy, SetUpEnergy, CloserEnergy;

    public static int StarterMoraleMax, MidRelivMoraleMax, SetUpMoraleMax, CloserMoraleMax;
    public static int StarterEnergyMax, MidRelievEnergyMax, SetUpEnergyMax, CloserEnergyMax;

    //attack strength
    public static int starterFast, starterSlid, starterCurve, starterChange, starterAgil;
    public static int middleFast, middleSlid, middleCurve, middleChange, middleAgil;
    public static int setupFast, setupSlid, setupCurve, setupChange, setupAgil;
    public static int closerFast, closerSlid, closerCurve, closerChange, closerAgil;

    //experience
    private static float starterExp, mrExp, setUpExp, closerExp;
    private static float starterTargetExp, mrTargetExp, setupTargetExp, closerTargetExp;
    private static int starterLevel, mrLevel, setupLevel, closerLevel;
    //UI
    Slider Starter, MidReliv, SetUp, Closer;
    Slider StarterE, MidRelivE, SetUpE, CloserE;


    public Text moneyUI;

    public static bool gameStartingStats;
    public static bool playerStartingStats;

    public static float money;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public static float Money
    {
        get
        {
            return money;
        }

        set
        {
            money = value;
        }
    }

    #region Morale
    public static float StarterMorale
    {
        get
        {
            return starterMorale;
        }

        set
        {
            starterMorale = value;
        }
    }

    public static float MidRelivMorale
    {
        get
        {
            return midRelivMorale;
        }

        set
        {
            midRelivMorale = value;
        }
    }

    public static float SetUpMorale
    {
        get
        {
            return setUpMorale;
        }

        set
        {
            setUpMorale = value;
        }
    }

    public static float CloserMorale
    {
        get
        {
            return closerMorale;
        }

        set
        {
            closerMorale = value;
        }
    }
    #endregion
    #region Experience
           #region CurrentExp;
    public static float StarterExp
    {
        get
        {
            return starterExp;
        }

        set
        {
            starterExp = value;
        }
    }

    public static float MRExp
    {
        get
        {
            return mrExp;
        }

        set
        {
            mrExp = value;
        }
    }

    public static float SetUpExp
    {
        get
        {
            return setUpExp;
        }

        set
        {
            setUpExp = value;
        }
    }

    public static float CloserExp
    {
        get
        {
            return closerExp;
        }

        set
        {
            closerExp = value;
        }
    }
    #endregion
           #region TargetExp
    public static float StarterTargetExp
    {
        get
        {
            return starterTargetExp;
        }

        set
        {
            starterTargetExp = value;
        }
    }

    public static float MRTargetExp
    {
        get
        {
            return mrTargetExp;
        }

        set
        {
            mrTargetExp = value;
        }
    }

    public static float SetupTargetExp
    {
        get
        {
            return setupTargetExp;
        }

        set
        {
            setupTargetExp = value;
        }
    }

    public static float CloserTargetExp
    {
        get
        {
            return closerTargetExp;
        }

        set
        {
            closerTargetExp = value;
        }
    }
    #endregion

           #region Level
    public static int StarterLevel
    {
        get
        {
            return starterLevel;
        }

        set
        {
            starterLevel = value;
        }
    }

    public static int MRLevel
    {
        get
        {
            return mrLevel;
        }

        set
        {
            mrLevel = value;
        }
    }

    public static int SetUpLevel
    {
        get
        {
            return setupLevel;
        }

        set
        {
            setupLevel = value;
        }
    }

    public static int CloserLevel
    {
        get
        {
            return closerLevel;
        }

        set
        {
            closerLevel = value;
        }
    }
    #endregion
    #endregion
    #region PlayerStats

    #region Starter
    public static int StarterFast
    {
        get
        {
            return starterFast;
        }

        set
        {
            starterFast = value;
        }
    }

    public static int StarterSlid
    {
        get
        {
            return starterSlid;
        }

        set
        {
            starterSlid = value;
        }
    }

    public static int StarterCurve
    {
        get
        {
            return starterCurve;
        }

        set
        {
            starterCurve = value;
        }
    }

    public static int StarterChange
    {
        get
        {
            return starterChange;
        }

        set
        {
            starterChange = value;
        }
    }

    public static int StarterAgil
    {
        get
        {
            return starterAgil;
        }

        set
        {
            starterAgil = value;
        }
    }
    #endregion

    #region Middle
    public static int MiddleFast
    {
        get
        {
            return middleFast;
        }

        set
        {
            middleFast = value;
        }
    }

    public static int MiddleSlid
    {
        get
        {
            return middleSlid;
        }

        set
        {
            middleSlid = value;
        }
    }

    public static int MiddleCurve
    {
        get
        {
            return middleCurve;
        }

        set
        {
            middleCurve = value;
        }
    }

    public static int MiddleChange
    {
        get
        {
            return middleChange;
        }

        set
        {
            middleChange = value;
        }
    }

    public static int MiddleAgil
    {
        get
        {
            return middleAgil;
        }

        set
        {
            middleAgil = value;
        }
    }
    #endregion

    #region SetUp
    public static int SetUpFast
    {
        get
        {
            return setupFast;
        }

        set
        {
            setupFast = value;
        }
    }

    public static int SetUpSlid
    {
        get
        {
            return setupSlid;
        }

        set
        {
            setupSlid = value;
        }
    }

    public static int SetUpCurve
    {
        get
        {
            return setupCurve;
        }

        set
        {
            setupCurve = value;
        }
    }

    public static int SetUpChange
    {
        get
        {
            return setupChange;
        }

        set
        {
            setupChange = value;
        }
    }

    public static int SetUpAgil
    {
        get
        {
            return setupAgil;
        }

        set
        {
            setupAgil = value;
        }
    }
    #endregion

    #region Closer
    public static int CloserFast
    {
        get
        {
            return closerFast;
        }

        set
        {
            closerFast = value;
        }
    }

    public static int CloserSlid
    {
        get
        {
            return closerSlid;
        }

        set
        {
            closerSlid = value;
        }
    }

    public static int CloserCurve
    {
        get
        {
            return closerCurve;
        }

        set
        {
            closerCurve = value;
        }
    }

    public static int CloserChange
    {
        get
        {
            return closerChange;
        }

        set
        {
            closerChange = value;
        }
    }

    public static int CloserAgil
    {
        get
        {
            return closerAgil;
        }

        set
        {
            closerAgil = value;
        }
    }
    #endregion

    #endregion
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        #region StartingStats 
        if (!playerStartingStats)
        {
            starterFast = 2;
            starterSlid = 2;
            starterCurve = 5;
            starterChange = 3;
            starterAgil = 3;

            middleFast = 3;
            middleSlid = 5;
            middleCurve = 2;
            middleChange = 3;
            middleAgil = 2;

            setupFast = 2;
            setupSlid = 1;
            setupCurve = 1;
            setupChange = 6;
            setupAgil = 4;

            closerFast = 7;
            closerSlid = 3;
            closerCurve = 1;
            closerChange = 1;
            closerAgil = 3;

            playerStartingStats = true;
        }
        #endregion

        #region Experience
        starterTargetExp = 5;
        mrTargetExp = 5;
        setupTargetExp = 5;
        closerTargetExp = 5;
        #endregion

        #region Level
        starterLevel = 1;
        mrLevel = 1;
        setupLevel = 1;
        closerLevel = 1;
        #endregion

        if (!gameStartingStats)
        {
            StarterMorale = 25;
            MidRelivMorale = 20;
            SetUpMorale = 15;
            CloserMorale = 10;


            StarterMoraleMax = 25;
            MidRelivMoraleMax = 20;
            SetUpMoraleMax = 15;
            CloserMoraleMax = 10;

            StarterEnergy = 25;
            MidRelivEnergy = 20;
            SetUpEnergy = 15;
            CloserEnergy = 10;

            StarterEnergyMax = 25;
            MidRelievEnergyMax = 20;
            SetUpEnergyMax = 15;
            CloserEnergyMax = 10;

            gameStartingStats = true;
        }

        if (StarterMorale == 0)
        {
            StarterMorale = 1;
        }

        if (MidRelivMorale == 0)
        {
            MidRelivMorale = 1;
        }

        if (SetUpMorale == 0)
        {
            SetUpMorale = 1;
        }

        if (CloserMorale == 0)
        {
            CloserMorale = 1;
        }

        moneyUI.text = "$ " + Money.ToString("F0");

        Starter = GameObject.Find("StarterMorale").GetComponent<Slider>();
        MidReliv = GameObject.Find("MiddleRelivMorale").GetComponent<Slider>();
        SetUp = GameObject.Find("SetUpMorale").GetComponent<Slider>();
        Closer = GameObject.Find("CloserMorale").GetComponent<Slider>();

        StarterE = GameObject.Find("StarterEnergy").GetComponent<Slider>();
        MidRelivE = GameObject.Find("MiddleRelivEnergy").GetComponent<Slider>();
        SetUpE = GameObject.Find("SetUpEnergy").GetComponent<Slider>();
        CloserE = GameObject.Find("CloserEnergy").GetComponent<Slider>();
    }

    private void Update()
    {
        Starter.value = StarterMorale / StarterMoraleMax;
        MidReliv.value = MidRelivMorale / MidRelivMoraleMax;
        SetUp.value = SetUpMorale / SetUpMoraleMax;
        Closer.value = CloserMorale / CloserMoraleMax;

        StarterE.value = StarterEnergy / StarterEnergyMax;
        MidRelivE.value = MidRelivEnergy / MidRelievEnergyMax;
        SetUpE.value = SetUpEnergy / SetUpEnergyMax;
        CloserE.value = CloserEnergy / CloserEnergyMax;
    }

}
