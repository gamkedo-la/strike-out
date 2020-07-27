using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static float starterMorale, midRelivMorale, setUpMorale, closerMorale;
    public static float StarterEnergy, MidRelivEnergy, SetUpEnergy, CloserEnergy;

    public static int StarterMoraleMax, MidRelivMoraleMax, SetUpMoraleMax, CloserMoraleMax;
    public static int StarterEnergyMax, MidRelievEnergyMax, SetUpEnergyMax, CloserEnergyMax;

    private static float starterExp, mrExp, setUpExp, closerExp;

    Slider Starter, MidReliv, SetUp, Closer;
    Slider StarterE, MidRelivE, SetUpE, CloserE;

    public Text moneyUI;

    public static bool gameStartingStats;

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

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
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
