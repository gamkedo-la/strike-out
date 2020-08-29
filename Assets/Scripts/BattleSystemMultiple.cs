﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This is the definition of the Game States
public enum BattleStateMultiple { START, STARTER, MIDDLE, SETUP, CLOSER, ENEMYTURN, WON, LOST }

public class BattleSystemMultiple : MonoBehaviour
{
    public BattleStateMultiple state;

    public GameObject StarterPrefab;
    public GameObject MiddleRelievPrefab;
    public GameObject SetUpPrefab;
    public GameObject CloserPrefab;

    Animator StarterAnim;
    Animator MidRelAnim;
    Animator SetUpAnim;
    Animator CloserAnim;


    public Transform StarterStation;
    public Transform MiddleStationStation;
    public Transform SetUpStation;
    public Transform CloserStation;
    public Transform EnemyBattleStation;

    public GameObject PlayerMenu;
    public GameObject PlayerPitches;
    public GameObject ConfirmMenu;
    public GameObject EndingMenu;
    //the player characters
    Unit Starter;
    Unit MiddleReliever;
    Unit SetUp;
    Unit Closer;
    //slider in the bottom right
    public Slider StarterMorale, StarterEnergy;
    public Slider MiddleMorale, MiddleEnergy;
    public Slider SetUpMorale, SetUpEnergy;
    public Slider CloserMorale, CloserEnergy;

    //Setting Color for being up versus downed
    public Color staminaBaseColor, energyBaseColor, downedColor;
    #region End screen experience
    public Text StarterExpGain, MRExpGain, SetUpExpGain, CloserExpGain;
    public Text StarterExpToNext, MRExpToNext, SetUpExpToNext, CloserExpToNext;
    public Text StartTotalExp, MRTotalExp, SetUpTotalExp, CloserTotalExp;
    bool SLevel, MLevel, SeLevel, CLevel;
    public GameObject SLevelUp, MLevelUp, SetUpLevelUp, CloserLevelUp;

    public GameObject PlayerStatsScreen, SLevelUpScreen, MLevelUpScreen, SeLevelUpScreen, CLevelUpScreen;

    public Slider SFSlider, SSSlider, SCSlider, SChSlider, SASlider;
    public Slider MFSlider, MSSlider, MCSlider, MChSlider, MASlider;
    public Slider SeFSlider, SeSSlider, SeCSlider, SeChSlider, SeASlider;
    public Slider CFSlider, CSSlider, CCSlider, CChSlider, CASlider;
    public Text SPoints, MPoints, SePoints, CPoints;
    
    int SPointsToGive, MPointsToGive, SePointsToGive, CPointsToGive;
    #endregion
    public Text MoneyText;
    //Particle systems for selection
    public GameObject enemySelectionParticle;
    public GameObject playerSelectionParticle;
    //determining enemy selection
    public int enemyUnitSelected;
    public List<Transform> enemyBattleStationLocations;
    public List<GameObject> enemyPrefab;
    public List<Unit> enemyUnit;
    public List<Animator> enemyAnim;
    //text informing the player what is going on
    public Text dialogueText;

    //PlayerPitchChoice
    bool fastball;
    bool curveball;
    bool slider;
    bool changeup;

    //Enemy
    bool isDead;
    //determing player losing conditions
    public GameObject[] playerStations;
    bool starterDead, middleDead, setupDead, closerDead;
    bool allPlayersDead;
    int enemyCount;

    //Choosing which player to attack
    int WhoToAttack;
    //
    bool enemySelect;
    bool isOver;
    int totalExp;
    //PitchLevelUp
    bool starterLevel, middleLevel, setUpLevel, closerLevel;
    public GameObject FastballButton, SliderButton, CurveballButton, ChangeUpButton, AgilityButton, Macro;

    //Initiated GameObjects
    GameObject playerGO1, playerGO2, playerGO3, playerGO4;

    //ItemMenuButton
    GameObject ItemMenu;
    public GameObject backButtonItem;

    GameObject GameManagerObject;

    //Cam positions
    public GameObject Camera;
    public Transform starterCam, middleCam, setupCam, closerCam, enemyCam, battleCam, enemyCamTarget;
    private void Start()
    {
        Camera.transform.position = battleCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget.transform.position);

        state = BattleStateMultiple.START;

        starterDead = false;
        middleDead = false;
        setupDead = false;
        closerDead = false;

        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);

        StarterEnergy.value = (GameManager.StarterEnergy / GameManager.StarterEnergyMax);
        MiddleEnergy.value = (GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax);
        SetUpEnergy.value = (GameManager.SetUpEnergy / GameManager.SetUpEnergyMax);
        CloserEnergy.value = (GameManager.CloserEnergy / GameManager.CloserEnergyMax);

        ItemMenu = GameObject.Find("Inventory");
        GameManagerObject = GameObject.Find("GameManager");

        for (int i = 0; i < enemyBattleStationLocations.Count; i++)
        {
            enemyCount++;
            enemyPrefab[i].SetActive(true);
        }

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerGO1 = Instantiate(StarterPrefab, StarterStation);
        playerGO2 = Instantiate(MiddleRelievPrefab, MiddleStationStation);
        playerGO3 = Instantiate(SetUpPrefab, SetUpStation);
        playerGO4 = Instantiate(CloserPrefab, CloserStation);

        Starter = playerGO1.GetComponent<Unit>();
        MiddleReliever = playerGO2.GetComponent<Unit>();
        SetUp = playerGO3.GetComponent<Unit>();
        Closer = playerGO4.GetComponent<Unit>();


        StarterAnim = playerGO1.GetComponent<Animator>();
        MidRelAnim = playerGO2.GetComponent<Animator>();
        SetUpAnim = playerGO3.GetComponent<Animator>();
        CloserAnim = playerGO4.GetComponent<Animator>();

        for (int i = 0; i < enemyBattleStationLocations.Count; i++)
        {
            // Instantiate(enemyPrefab[i], enemyBattleStationLocations[i].position, Quaternion.identity);
            // GameObject enemyGO = enemyPrefab[i];
            GameObject enemyGO = Instantiate(enemyPrefab[i], enemyBattleStationLocations[i]);
            enemyUnit[i] = enemyGO.GetComponent<Unit>();
            enemyAnim[i] = enemyGO.GetComponentInChildren<Animator>();
        }

        yield return new WaitForSeconds(4f);

        state = BattleStateMultiple.STARTER;
        StarterTurn();
    }

    private void Update()
    {
        if ((state == BattleStateMultiple.STARTER || state == BattleStateMultiple.MIDDLE || state == BattleStateMultiple.SETUP || state == BattleStateMultiple.CLOSER) && enemySelect)
        {
            //SelectionProcess
            enemySelectionParticle.SetActive(true);
            enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (enemyUnitSelected >= 0)
                {
                    enemyUnitSelected--;
                }
                if (enemyUnitSelected < 0)
                {
                    enemyUnitSelected = enemyBattleStationLocations.Count-1;
                }

                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
                Camera.transform.LookAt(enemyBattleStationLocations[enemyUnitSelected]);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(enemyUnitSelected <= enemyBattleStationLocations.Count-1)
                    enemyUnitSelected++;
                if (enemyUnitSelected > enemyBattleStationLocations.Count-1)
                    enemyUnitSelected = 0;

                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
                Camera.transform.LookAt(enemyBattleStationLocations[enemyUnitSelected]);
            }

            if (state == BattleStateMultiple.STARTER)
            {
                Starter.transform.LookAt(enemyBattleStationLocations[enemyUnitSelected]);
            }

            if (state == BattleStateMultiple.MIDDLE)
            {
                MiddleReliever.transform.LookAt(enemyBattleStationLocations[enemyUnitSelected]);
            }

            if (state == BattleStateMultiple.SETUP)
            {
                SetUp.transform.LookAt(enemyBattleStationLocations[enemyUnitSelected]);
            }

            if (state == BattleStateMultiple.CLOSER)
            {
                Closer.transform.LookAt(enemyBattleStationLocations[enemyUnitSelected]);
            }
        }

        if (enemyCount == 0 && !isOver)
        {
            state = BattleStateMultiple.WON;
            EndBattle();
        }

        if (starterDead && middleDead && setupDead && closerDead)
        {
            state = BattleStateMultiple.LOST;
            allPlayersDead = true;
            EndBattle();
        }
        #region Animations based on Health
        //Starter
        if (GameManager.StarterMorale > (GameManager.StarterMoraleMax * .2))
        {
            StarterAnim.SetBool("isInjured", false);
        }
        if (GameManager.StarterMorale <= (GameManager.StarterMoraleMax * .2) && GameManager.StarterMorale > 0)
        {
            StarterAnim.SetBool("isInjured", true);
        }
        if (GameManager.StarterMorale <= 0)
        {
            starterDead = true;
            StarterAnim.SetBool("isDead", true);
        }
        //MR
        if (GameManager.MidRelivMorale > (GameManager.MidRelivMoraleMax * .2))
        {
            MidRelAnim.SetBool("isInjured", false);
        }
        if (GameManager.MidRelivMorale <= (GameManager.MidRelivMoraleMax * .2) && GameManager.MidRelivMorale > 0)
        {
            MidRelAnim.SetBool("isInjured", true);
        }
        if (GameManager.MidRelivMorale <= 0)
        {
            middleDead = true;
            MidRelAnim.SetBool("isDead", true);
        }
        //SetUp
        if (GameManager.SetUpMorale > (GameManager.SetUpMoraleMax * .2))
        {
            SetUpAnim.SetBool("isInjured", false);
        }
        if (GameManager.SetUpMorale <= (GameManager.SetUpMoraleMax * .2) && GameManager.SetUpMorale > 0)
        {
            SetUpAnim.SetBool("isInjured", true);
        }
        if (GameManager.SetUpMorale <= 0)
        {
            setupDead = true;
            SetUpAnim.SetBool("isDead", true);
        }
        //Closer
        if (GameManager.CloserMorale > (GameManager.CloserMoraleMax * .2))
        {
            CloserAnim.SetBool("isInjured", false);
        }
        if (GameManager.CloserMorale <= (GameManager.CloserMoraleMax * .2) && GameManager.CloserMorale > 0)
        {
            CloserAnim.SetBool("isInjured", true);
        }
        if (GameManager.CloserMorale <= 0)
        {
            closerDead = true;
            CloserAnim.SetBool("isDead", true);
        }
        print(GameManager.StarterMorale + " " + GameManager.MidRelivMorale + " " + GameManager.SetUpMorale + " " + GameManager.CloserMorale + " " );
        #endregion
    }

    public void CallTime()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            GameManager.StarterEnergy += 5;
            for (int i = 0; i < enemyUnit.Count; i++)
            {
                enemyUnit[i].TakeDamage(-5);
            }
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 0);
        }

        if (state == BattleStateMultiple.MIDDLE)
        {
            GameManager.MidRelivEnergy += 5;
            for (int i = 0; i < enemyUnit.Count; i++)
            {
                enemyUnit[i].TakeDamage(-5);
            }
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 1);
        }

        if (state == BattleStateMultiple.SETUP)
        {
            GameManager.SetUpEnergy += 5;
            for (int i = 0; i < enemyUnit.Count; i++)
            {
                enemyUnit[i].TakeDamage(-5);
            }
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 2);
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            GameManager.CloserEnergy += 5;
            for (int i = 0; i < enemyUnit.Count; i++)
            {
                enemyUnit[i].TakeDamage(-5);
            }
            state = BattleStateMultiple.STARTER;
            StarterTurn();
        }
    }
    #region ItemManagement

    public void SportsDrink()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            GameManagerObject.GetComponent<GameManager>().StarterHealthUp(20);
        }
        if (state == BattleStateMultiple.SETUP)
        {
            GameManagerObject.GetComponent<GameManager>().SetUpHealthUp(20);
        }
        if (state == BattleStateMultiple.MIDDLE)
        {
            GameManagerObject.GetComponent<GameManager>().MiddleHealthUp(20);
        }
        if (state == BattleStateMultiple.CLOSER)
        {
            GameManagerObject.GetComponent<GameManager>().CloserHealthUp(20);
        }
        AdvanceTurn();
    }

    public void GranolaBar()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            GameManagerObject.GetComponent<GameManager>().StarterEnergyUp(10);
        }
        if (state == BattleStateMultiple.SETUP)
        {
            GameManagerObject.GetComponent<GameManager>().MiddleEnergyUp(10);
        }
        if (state == BattleStateMultiple.MIDDLE)
        {
            GameManagerObject.GetComponent<GameManager>().SetUpEnergyUp(10);
        }
        if (state == BattleStateMultiple.CLOSER)
        {
            GameManagerObject.GetComponent<GameManager>().CloserEnergyUp(10);
        }
        AdvanceTurn();
    }

    public void UpdateHealthUI()
    {
        AdvanceTurn();
    }
    public void DefensiveShiftItem()
    {
        print("Shift");
        for (int i = 0; i < enemyUnit.Count; i++)
        {
            isDead = enemyUnit[i].TakeDamage(20);

            if (isDead)
            {
                //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                //Destroy(enemyPrefab[enemyUnitSelected]);
                totalExp += enemyUnit[i].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[i]);
                enemyPrefab.Remove(enemyPrefab[i]);
                enemyUnit.Remove(enemyUnit[i]);

                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine("EnemyTurn", 0);
            }

            if (!isDead)
            {
                if (state == BattleStateMultiple.STARTER || state == BattleStateMultiple.MIDDLE || state == BattleStateMultiple.SETUP)
                {
                    state = BattleStateMultiple.ENEMYTURN;
                    StartCoroutine("EnemyTurn", 0);
                }
                if (state == BattleStateMultiple.CLOSER)
                {
                    state = BattleStateMultiple.STARTER;
                    StarterTurn();
                }
            }
        }
        AdvanceTurn();
        print("Did work?");
    }

    public void ScoutingReportItem()
    {
        isDead = enemyUnit[enemyUnitSelected].TakeDamage(20);

        if (state == BattleStateMultiple.STARTER || state == BattleStateMultiple.MIDDLE || state == BattleStateMultiple.SETUP)
        {
            if (isDead)
            {
                //enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                //Destroy(enemyPrefab[enemyUnitSelected]);
                totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                enemyUnitSelected = 0;
                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            }
            if (!isDead)
            {
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine("EnemyTurn", 0);
            }
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            if (isDead)
            {
                //enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                //Destroy(enemyPrefab[enemyUnitSelected]);
                totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                enemyUnitSelected = 0;
                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            }
            if (!isDead)
            {
                state = BattleStateMultiple.STARTER;
                StarterTurn();
            }
        }
    }

    public void ItemMenuButton()
    {
        ItemMenu.transform.localPosition = new Vector3(233, 20, 0);
        backButtonItem.SetActive(true);
        PlayerMenu.SetActive(false);
        PlayerPitches.SetActive(false);
    }

    public void ItemMenuBack()
    {
        backButtonItem.SetActive(false);
        ItemMenu.transform.localPosition = new Vector3(233, -900, 0);
        PlayerMenu.SetActive(true);
        PlayerPitches.SetActive(false);
    }

    public void AdvanceTurn()
    {
        backButtonItem.SetActive(false);
        PlayerMenu.SetActive(false);
        PlayerPitches.SetActive(false);
        ItemMenu.transform.localPosition = new Vector3(233, -900, 0);

        if (state == BattleStateMultiple.STARTER)
        {
            
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 0);
        }

        if (state == BattleStateMultiple.MIDDLE)
        {
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 1);
        }

        if (state == BattleStateMultiple.SETUP)
        {
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 2);
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            state = BattleStateMultiple.STARTER;
            StartCoroutine(CloserToStarterWait());
        }

        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);

        StarterEnergy.value = (GameManager.StarterEnergy / GameManager.StarterEnergyMax);
        MiddleEnergy.value = (GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax);
        SetUpEnergy.value = (GameManager.SetUpEnergy / GameManager.SetUpEnergyMax);
        CloserEnergy.value = (GameManager.CloserEnergy / GameManager.CloserEnergyMax);
    }

    IEnumerator CloserToStarterWait()
    {
        yield return new WaitForSeconds(0.5f);
        StarterTurn();
    }
    #endregion

    public void CallBullpen()
    {
        int Rand = Random.Range(0, 2);
        if (Rand == 0)
        {
            dialogueText.text = "No one is warming up! You can't leave!";
        }
        if (Rand == 1)
        {
            dialogueText.text = "The Manager is signaling a replacement, you get away!";
            StartCoroutine(WaitingAtEndOfBattle());
        }
    }

    public void ConfirmAttack()
    {
        //Starter
        if (state == BattleStateMultiple.STARTER)
        {
            if (fastball)
            {
                if (Starter.fastballStamina <= GameManager.StarterEnergy)
                {
                    StarterAnim.Play("Armature|Windup");
                    GameManager.StarterEnergy -= Starter.fastballStamina;
                    //UpdateStarterUI;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;

                }
            }
            if (slider)
            {
                if (Starter.sliderStamina <= GameManager.StarterEnergy)
                {
                    StarterAnim.Play("Armature|Windup");
                    GameManager.StarterEnergy -= Starter.sliderStamina;
                    //UpdateStarterUI;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                }
            }
            if (curveball)
            {
                if (Starter.curveballStamina <= GameManager.StarterEnergy)
                {
                    StarterAnim.Play("Armature|Windup");
                    GameManager.StarterEnergy -= Starter.curveballStamina;
                    //UpdateStarterUI;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                }
            }
            if (changeup)
            {
                if (Starter.changeupStamina <= GameManager.StarterEnergy)
                {
                    StarterAnim.Play("Armature|Windup");
                    GameManager.StarterEnergy -= Starter.changeupStamina;
                    //UpdateStarterUI;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                }
            }
            //lock in enemy
            StartCoroutine(PlayerAttack());
        }
        //Middle
        if (state == BattleStateMultiple.MIDDLE)
        {
            if (fastball)
            {
                if (MiddleReliever.fastballStamina <= GameManager.MidRelivEnergy)
                {
                    MidRelAnim.Play("Armature|Windup");
                    GameManager.MidRelivEnergy -= MiddleReliever.fastballStamina;
                    //UpdateStarterUI;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                }
            }
            if (slider)
            {
                if (MiddleReliever.sliderStamina <= GameManager.StarterEnergy)
                {
                    MidRelAnim.Play("Armature|Windup");
                    GameManager.MidRelivEnergy -= MiddleReliever.sliderStamina;
                    //UpdateStarterUI;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                }
            }
            if (curveball)
            {
                if (MiddleReliever.curveballStamina <= GameManager.StarterEnergy)
                {
                    MidRelAnim.Play("Armature|Windup");
                    GameManager.MidRelivEnergy -= MiddleReliever.curveballStamina;
                    //UpdateStarterUI;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                }
            }
            if (changeup)
            {
                if (MiddleReliever.changeupStamina <= GameManager.StarterEnergy)
                {
                    MidRelAnim.Play("Armature|Windup");
                    GameManager.MidRelivEnergy -= MiddleReliever.changeupStamina;
                    //UpdateStarterUI;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                }
            }
            //lock in enemy
            StartCoroutine(MiddleAttack());
        }
        //setup
        if (state == BattleStateMultiple.SETUP)
        {
            if (fastball)
            {
                if (SetUp.fastballStamina <= GameManager.SetUpEnergy)
                {
                    SetUpAnim.Play("Armature|Windup");
                    GameManager.SetUpEnergy -= SetUp.fastballStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                }
            }
            if (slider)
            {
                if (SetUp.sliderStamina <= GameManager.SetUpEnergy)
                {
                    SetUpAnim.Play("Armature|Windup");
                    GameManager.SetUpEnergy -= SetUp.sliderStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                }
            }
            if (curveball)
            {
                if (SetUp.curveballStamina <= GameManager.SetUpEnergy)
                {
                    SetUpAnim.Play("Armature|Windup");
                    GameManager.SetUpEnergy -= SetUp.curveballStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                }
            }
            if (changeup)
            {
                if (SetUp.changeupStamina <= GameManager.SetUpEnergy)
                {
                    SetUpAnim.Play("Armature|Windup");
                    GameManager.SetUpEnergy -= SetUp.changeupStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                }
            }
            //lock in enemy
            StartCoroutine(SetUpAttack());
        }
        //Closer
        if (state == BattleStateMultiple.CLOSER)
        {
            if (fastball)
            {
                if (Closer.fastballStamina <= GameManager.CloserEnergy)
                {
                    CloserAnim.Play("Armature|Windup");
                    GameManager.CloserEnergy -= Closer.fastballStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                }
            }
            if (slider)
            {
                if (Closer.sliderStamina <= GameManager.CloserEnergy)
                {
                    CloserAnim.Play("Armature|Windup");
                    GameManager.CloserEnergy -= Closer.sliderStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                }
            }
            if (curveball)
            {
                if (Closer.curveballStamina <= GameManager.CloserEnergy)
                {
                    CloserAnim.Play("Armature|Windup");
                    GameManager.CloserEnergy -= Closer.curveballStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                }
            }
            if (changeup)
            {
                if (Closer.changeupStamina <= GameManager.CloserEnergy)
                {
                    CloserAnim.Play("Armature|Windup");
                    GameManager.CloserEnergy -= Closer.changeupStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                }
            }
            //lock in enemy
            StartCoroutine(CloserAttack());
        }
        ConfirmMenu.SetActive(false);
        enemySelectionParticle.SetActive(false);
        enemySelect = false;
    }

    public void CancelAttack()
    {
        PlayerPitches.SetActive(true);
        ConfirmMenu.SetActive(false);
        enemySelectionParticle.SetActive(false);
        enemySelect = false;
    }
    #region PlayerAttack
    IEnumerator PlayerAttack()
    {
        //To Do Damage Enemy
        yield return new WaitForSeconds(1f);

        if (state == BattleStateMultiple.STARTER)
        {
            if (fastball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    fastball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(PlayerAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(Starter.fastballDamage + GameManager.StarterFast);
                    fastball = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        //Destroy(enemyPrefab[enemyUnitSelected]);
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }

                }
            }
            if (slider)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    slider = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(PlayerAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(Starter.sliderDamage + GameManager.StarterSlid);
                    slider = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        //Destroy(enemyPrefab[enemyUnitSelected]);
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }
                }
            }
            if (curveball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    curveball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(PlayerAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(Starter.curveballDamage + GameManager.StarterCurve);
                    curveball = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        //Destroy(enemyPrefab[enemyUnitSelected]);
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }
                }
            }
            if (changeup)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    changeup = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(PlayerAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(Starter.changeupDamage + GameManager.StarterChange);
                    changeup = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        //Destroy(enemyPrefab[enemyUnitSelected]);
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 0);
                    }
                }
            }
        }
    }


    IEnumerator MiddleAttack()
    {
        yield return new WaitForSeconds(1f);

        if (state == BattleStateMultiple.MIDDLE)
        {
            if (fastball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    fastball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(MiddleAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(MiddleReliever.fastballDamage + GameManager.MiddleFast);
                    fastball = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                         */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }

                    if (!isDead)
                    {
                        //Enemy Attack turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }
                }
            }
            if (slider)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    slider = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(MiddleAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(MiddleReliever.sliderDamage + GameManager.MiddleSlid);
                    slider = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                         */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }

                    if (!isDead)
                    {
                        //Enemy Attack turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }
                }
            }
            if (curveball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    curveball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(MiddleAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(MiddleReliever.curveballDamage + GameManager.MiddleCurve);
                    curveball = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                         */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }

                    if (!isDead)
                    {
                        //Enemy Attack turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }
                }
            }
            if (changeup)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    changeup = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(MiddleAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(MiddleReliever.changeupDamage + GameManager.MiddleChange);
                    changeup = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                         */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }

                    if (!isDead)
                    {
                        //Enemy Attack turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 1);
                    }
                }
            }
        }
    }

    IEnumerator SetUpAttack()
    {
        yield return new WaitForSeconds(1f);

        if (state == BattleStateMultiple.SETUP)
        {
            //To Do Start Attack Animation
            if (fastball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    fastball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(SetUpAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(SetUp.fastballDamage + GameManager.SetUpFast);
                    fastball = false;
                    // enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }

                    if (!isDead)
                    {
                        //Closer turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }
                }
            }
            if (slider)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    slider = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(SetUpAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(SetUp.sliderDamage + GameManager.SetUpSlid);
                    slider = false;
                    // enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }

                    if (!isDead)
                    {
                        //Closer turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }
                }
            }
            if (curveball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    curveball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(SetUpAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(SetUp.curveballDamage + GameManager.SetUpCurve);
                    curveball = false;
                    // enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }

                    if (!isDead)
                    {
                        //Closer turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }
                }
            }
            if (changeup)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    changeup = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(SetUpAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(SetUp.changeupDamage + GameManager.SetUpChange);
                    changeup = false;
                    // enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /* enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                         enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                         enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                         enemyUnitSelected = 0;
                        */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }

                    if (!isDead)
                    {
                        //Closer turn
                        state = BattleStateMultiple.ENEMYTURN;
                        StartCoroutine("EnemyTurn", 2);
                    }
                }
            }

        }

    }

    IEnumerator CloserAttack()
    {
        yield return new WaitForSeconds(1f);

        if (state == BattleStateMultiple.CLOSER)
        {
            if (fastball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    fastball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(CloserAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(Closer.fastballDamage + GameManager.CloserFast);
                    fastball = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /*enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                        enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                        enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                        enemyUnitSelected = 0;
                       */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }
                }
            }
            if (slider)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    slider = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(CloserAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(Closer.sliderDamage + GameManager.CloserSlid);
                    slider = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /*enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                        enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                        enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                        enemyUnitSelected = 0;
                       */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }
                }
            }
            if (curveball)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    curveball = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(CloserAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(Closer.curveballDamage + GameManager.CloserCurve);
                    curveball = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /*enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                        enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                        enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                        enemyUnitSelected = 0;
                       */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }
                }
            }
            if (changeup)
            {
                if (enemyUnit[enemyUnitSelected].currentHP <= 0)
                {
                    changeup = false;
                    dialogueText.text = "Enemy is knocked out, select another target.";
                    yield return new WaitForSeconds(1f);
                    dialogueText.text = "Select someone to attack!";
                    StartCoroutine(CloserAttack());
                }
                else
                {
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(Closer.changeupDamage + GameManager.CloserChange);
                    changeup = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        //               enemyAnim[enemyUnitSelected].Play("Armature|Downed");
                        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;
                        /*enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                        enemyPrefab.Remove(enemyPrefab[enemyUnitSelected]);
                        enemyUnit.Remove(enemyUnit[enemyUnitSelected]);
                        enemyUnitSelected = 0;
                       */
                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }

                    if (!isDead)
                    {
                        //Middle Reliever turn
                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }
                }
            }

        }
    }
    #endregion

    bool isPlayerIndexDead(int playerID)
    {
        switch (playerID) {
            case 0:
                return starterDead;
            case 1:
                return middleDead;
            case 2:
                return setupDead;
            case 3:
                return closerDead;
        }

        Debug.LogError("isPlayerIndexDead received an invalid index " + playerID);
        return false;
    }

    void NextPlayerTurnAfterEnemyTurn(int enemyIndex)
    {
        switch (enemyIndex)
        {
            case 0:
                state = BattleStateMultiple.MIDDLE;
                //Skipping Turn to go to pitcher
                MiddleTurn();
                break;
            case 1:
                state = BattleStateMultiple.SETUP;
                //Skipping Turn to go to pitcher
                SETUPTurn();
                break;
            case 2:
                state = BattleStateMultiple.CLOSER;
                //Skipping Turn to go to pitcher
                CloserTurn();
                break;
            default:
                Debug.LogError("Unable to find a player with the given index " + enemyIndex);
                break;
        }
    }

    IEnumerator EnemyTurn(int enemyIndex)
    {
        Camera.transform.position = enemyCam.transform.position;
        Camera.transform.LookAt(Starter.transform.position);
        GameManager.Instance.DebugBall.transform.position = enemyUnit[enemyIndex].transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (enemyUnit[enemyIndex].currentHP <= 0)
        {
            NextPlayerTurnAfterEnemyTurn(enemyIndex);    
        }  else {

            if (starterDead && middleDead && setupDead && closerDead)
            {
                EndBattle();
            }

            int RandomAttack = Random.Range(0, 100);
            //Need logic to determine what attack the enemy will do

            //attack animation

            //Choosing Who To Attack
            //happens at least once, if it is true, it does it again. (keep going until valid)
            int safteyCounter = 1000;
            do
            {
                WhoToAttack = Random.Range(0, 4);
                if (safteyCounter-- < 0)
                {
                    Debug.LogError("Couldn't find a living WhoToAttack, is the Whole Team Dead?");
                    break;
                    //bails us out of the do while
                }

            } while (isPlayerIndexDead(WhoToAttack));

            yield return new WaitForSeconds(1.5f);

            enemyAnim[enemyIndex].Play("Armature|Swing");

            if (WhoToAttack == 0 && !starterDead)
            {
                Camera.transform.LookAt(Starter.transform.position);
                if (GameManager.StarterAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Starter Dodges!";
                    yield return new WaitForSeconds(1f);
                    NextPlayerTurnAfterEnemyTurn(enemyIndex);
                }

                if (GameManager.StarterAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";

                    yield return new WaitForSeconds(2f);
                    bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                        starterDead = true;

                        StarterAnim.SetBool("isDead", true);
                        yield return new WaitForSeconds(3f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }

                    else
                    {
                        yield return new WaitForSeconds(.5f);
                        StarterAnim.Play("Armature|Oof");

                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                        yield return new WaitForSeconds(2f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }
                }
            }
            if (WhoToAttack == 1 && !middleDead)
            {
                Camera.transform.LookAt(MiddleReliever.transform.position);
                if (GameManager.MiddleAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Mid Reliever Dodges!";
                    yield return new WaitForSeconds(1f);
                    NextPlayerTurnAfterEnemyTurn(enemyIndex);
                }
                if (GameManager.MiddleAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                        middleDead = true;
                        MidRelAnim.SetBool("isDead", true);
                        yield return new WaitForSeconds(3f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }

                    else
                    {
                        yield return new WaitForSeconds(.5f);
                        MidRelAnim.Play("Armature|Oof");

                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                        yield return new WaitForSeconds(2f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }
                }

                yield return new WaitForSeconds(.5f);
            }
            if (WhoToAttack == 2 && !setupDead)
            {
                Camera.transform.LookAt(SetUp.transform.position);
                if (GameManager.SetUpAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks SetUp!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "SetUp Dodges!";
                    yield return new WaitForSeconds(1f);
                    NextPlayerTurnAfterEnemyTurn(enemyIndex);
                }
                if (GameManager.SetUpAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        SetUpAnim.SetBool("isDead", true);
                        setupDead = true;
                        yield return new WaitForSeconds(3f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }

                    else
                    {
                        SetUpAnim.Play("Armature|Oof");
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        yield return new WaitForSeconds(2f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }
                }
            }
            if (WhoToAttack == 3 && !closerDead)
            {
                Camera.transform.LookAt(Closer.transform.position);
                if (GameManager.CloserAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Closer Dodges!";
                    yield return new WaitForSeconds(1f);
                    NextPlayerTurnAfterEnemyTurn(enemyIndex);
                }

                if (GameManager.CloserAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        CloserAnim.SetBool("isDead", true);
                        closerDead = true;
                        yield return new WaitForSeconds(3f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }

                    else
                    {
                        CloserAnim.Play("Armature|Oof");
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        yield return new WaitForSeconds(2f);
                        NextPlayerTurnAfterEnemyTurn(enemyIndex);
                    }
                }
            }
        }

    }

    #region Enemy Attack
    /*
    IEnumerator EnemyTurn1()
    {

        if (enemyUnit[0].currentHP <= 0)
        {
            state = BattleStateMultiple.MIDDLE;
            //Skipping Turn to go to pitcher
            MiddleTurn();
        }

        if (enemyUnit[0].currentHP > 0)
        {
            if (starterDead && middleDead && setupDead && closerDead)
            {
                EndBattle();
            }

            int RandomAttack = Random.Range(0, 100);
            //Need logic to determine what attack the enemy will do

            //attack animation

            //Choosing Who To Attack
            WhoToAttack = Random.Range(0, 4);

            if (starterDead && WhoToAttack == 0)
            {
                StartCoroutine(EnemyTurn1());
            }
            if (middleDead && WhoToAttack == 1)
            {
                StartCoroutine(EnemyTurn1());
            }
            if (setupDead && WhoToAttack == 2)
            {
                StartCoroutine(EnemyTurn1());
            }
            if (closerDead && WhoToAttack == 3)
            {
                StartCoroutine(EnemyTurn1());
            }
            yield return new WaitForSeconds(1.5f);

            enemyAnim[0].Play("Armature|Swing");

            if (WhoToAttack == 0 && !starterDead)
            {
                if (GameManager.StarterAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Starter Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }

                if (GameManager.StarterAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";

                    yield return new WaitForSeconds(2f);
                    bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                        starterDead = true;

                        StarterAnim.SetBool("isDead", true);

                        state = BattleStateMultiple.MIDDLE;
                        MiddleTurn();
                    }

                    else
                    {
                        yield return new WaitForSeconds(.5f);
                        StarterAnim.Play("Armature|Oof");

                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);

                        state = BattleStateMultiple.MIDDLE;
                        MiddleTurn();
                    }
                }
            }
            if (WhoToAttack == 1 && !middleDead)
            {
                if (GameManager.MiddleAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Mid Reliever Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
                if (GameManager.MiddleAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                        middleDead = true;
                        MidRelAnim.SetBool("isDead", true);
                        state = BattleStateMultiple.SETUP;
                        SETUPTurn();
                    }

                    else
                    {
                        yield return new WaitForSeconds(.5f);
                        MidRelAnim.Play("Armature|Oof");

                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);

                        state = BattleStateMultiple.MIDDLE;
                        MiddleTurn();
                    }
                }

                yield return new WaitForSeconds(.5f);
            }
            if (WhoToAttack == 2 && !setupDead)
            {

                if (GameManager.SetUpAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks SetUp!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "SetUp Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
                if (GameManager.SetUpAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        SetUpAnim.SetBool("isDead", true);
                        setupDead = true;
                        state = BattleStateMultiple.MIDDLE;
                        MiddleTurn();
                    }

                    else
                    {
                        SetUpAnim.Play("Armature|Oof");
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        state = BattleStateMultiple.MIDDLE;
                        MiddleTurn();
                    }
                }
            }
            if (WhoToAttack == 3 && !closerDead)
            {

                if (GameManager.CloserAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Closer Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }

                if (GameManager.CloserAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        CloserAnim.SetBool("isDead", true);
                        closerDead = true;
                        state = BattleStateMultiple.MIDDLE;
                        MiddleTurn();
                    }

                    else
                    {
                        CloserAnim.Play("Armature|Oof");
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        state = BattleStateMultiple.MIDDLE;
                        MiddleTurn();
                    }
                }
            }
        }

    }
    /
    IEnumerator EnemyTurn2()
    {
        if (enemyUnit[0].currentHP <= 0)
        {
            state = BattleStateMultiple.SETUP;
            //Skipping Turn to go to pitcher
            SETUPTurn();
        }

        if (enemyUnit[0].currentHP > 0)
        { 
        if (starterDead && middleDead && setupDead && closerDead)
        {
            EndBattle();
        }

        int RandomAttack = Random.Range(0, 100);
        //Need logic to determine what attack the enemy will do

        //attack animation
        //attack animation

        //Choosing Who To Attack
        WhoToAttack = Random.Range(0, 4);

        if (starterDead && WhoToAttack == 0)
        {
            StartCoroutine(EnemyTurn2());
        }
        if (middleDead && WhoToAttack == 1)
        {
            StartCoroutine(EnemyTurn2());
        }
        if (setupDead && WhoToAttack == 2)
        {
            StartCoroutine(EnemyTurn2());
        }
        if (closerDead && WhoToAttack == 3)
        {
            StartCoroutine(EnemyTurn2());
        }
        yield return new WaitForSeconds(0.1f);

        if (WhoToAttack == 0 && !starterDead) 
        {
            if (GameManager.StarterAgil >= RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                yield return new WaitForSeconds(.5f);
                dialogueText.text = "Starter Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.StarterAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";

                yield return new WaitForSeconds(1.5f);
                bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                {
                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                        starterDead = true;
                        StarterAnim.SetBool("isDead", true);
                        state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }

                else
                {
                        yield return new WaitForSeconds(.5f);
                        StarterAnim.Play("Armature|Oof");

                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                        state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }
            }
        }
        if (WhoToAttack == 1 && !middleDead)
        {
            if (GameManager.MiddleAgil >= RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                yield return new WaitForSeconds(.5f);
                dialogueText.text = "Mid Reliever Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.MiddleAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                yield return new WaitForSeconds(1f);
                bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                {
                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                        middleDead = true;
                        MidRelAnim.SetBool("isDead", true);
                        state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }

                else
                {
                        MidRelAnim.Play("Armature|Oof");
                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                        state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }
            }
        }
        if (WhoToAttack == 2 && !setupDead)
        {
            if (GameManager.SetUpAgil >= RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
                yield return new WaitForSeconds(.5f);
                dialogueText.text = "Set Up Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.SetUpAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";

                yield return new WaitForSeconds(1f);
               bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                {
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        setupDead = true;
                        SetUpAnim.SetBool("isDead", true);
                        state = BattleStateMultiple.CLOSER;
                    CloserTurn();
                }

                else
                {
                        SetUpAnim.Play("Armature|Oof");
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }
            }
        }
            if (WhoToAttack == 3 && !closerDead)
            {
                if (GameManager.CloserAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Closer Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
                if (GameManager.CloserAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        closerDead = true;
                        CloserAnim.SetBool("isDead", true);

                        state = BattleStateMultiple.SETUP;
                        SETUPTurn();
                    }

                    else
                    {
                        CloserAnim.Play("Armature|Oof");
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        state = BattleStateMultiple.SETUP;
                        SETUPTurn();
                    }
                }
            }
        }

    }

    IEnumerator EnemyTurn3()
    {
        if (enemyUnit[0].currentHP <= 0)
        {
            state = BattleStateMultiple.CLOSER;
            //Skipping Turn to go to pitcher
            CloserTurn();
        }

        if (enemyUnit[0].currentHP > 0)
        {
            if (starterDead && middleDead && setupDead && closerDead)
            {
                EndBattle();
            }
            int RandomAttack = Random.Range(0, 100);

            //Need logic to determine what attack the enemy will do

            //attack animation

            //Choosing Who To Attack
            WhoToAttack = Random.Range(0, 4);

            if (starterDead && WhoToAttack == 0)
            {
                StartCoroutine(EnemyTurn3());
            }
            if (middleDead && WhoToAttack == 1)
            {
                StartCoroutine(EnemyTurn3());
            }
            if (setupDead && WhoToAttack == 2)
            {
                StartCoroutine(EnemyTurn3());
            }
            if (closerDead && WhoToAttack == 3)
            {
                StartCoroutine(EnemyTurn3());
            }
            yield return new WaitForSeconds(0.1f);

            if (WhoToAttack == 0 && !starterDead)
            {
                if (GameManager.StarterAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Starter Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
                if (GameManager.StarterAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";

                    yield return new WaitForSeconds(1.5f);
                    bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                        starterDead = true;
                        StarterAnim.SetBool("isDead", true);
                        state = BattleStateMultiple.CLOSER;
                        CloserTurn();
                    }

                    else
                    {
                        yield return new WaitForSeconds(.5f);
                        StarterAnim.Play("Armature|Oof");
                        GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                        state = BattleStateMultiple.CLOSER;
                        CloserTurn();
                    }
                }
            }
            if (WhoToAttack == 1 && !middleDead)
            {
                if (GameManager.MiddleAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "MidReliever Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
                if (GameManager.MiddleAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                        middleDead = true;
                        MidRelAnim.SetBool("isDead", true);
                        state = BattleStateMultiple.CLOSER;
                        CloserTurn();
                    }

                    else
                    {
                        MidRelAnim.Play("Armature|Oof");
                        GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                        state = BattleStateMultiple.CLOSER;
                        CloserTurn();
                    }
                }
            }
            if (WhoToAttack == 2 && !setupDead)
            {
                if (GameManager.SetUpAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Set Up Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
                if (GameManager.SetUpAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        middleDead = true;
                        SetUpAnim.SetBool("isDead", true);
                        state = BattleStateMultiple.CLOSER;
                        CloserTurn();
                    }

                    else
                    {
                        SetUpAnim.Play("Armature|Oof");
                        GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                        state = BattleStateMultiple.CLOSER;
                        CloserTurn();
                    }
                }
            }
            if (WhoToAttack == 3 && !closerDead)
            {
                if (GameManager.CloserAgil >= RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
                    yield return new WaitForSeconds(.5f);
                    dialogueText.text = "Closer Dodges!";
                    yield return new WaitForSeconds(1f);
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
                if (GameManager.CloserAgil < RandomAttack)
                {
                    dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";

                    yield return new WaitForSeconds(1f);
                    bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                    if (isDead)
                    {
                        closerDead = true;
                        CloserAnim.SetBool("isDead", true);
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        state = BattleStateMultiple.STARTER;
                        StarterTurn();
                    }

                    else
                    {
                        CloserAnim.Play("Armature|Oof");
                        GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                        state = BattleStateMultiple.CLOSER;
                        CloserTurn();
                    }
                }
            }
        }

    }
    */
    #endregion

    #region Player Turns
    void StarterTurn()
    {
        Camera.transform.position = starterCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget);
        GameManager.Instance.DebugBall.transform.position = Starter.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (starterDead)
        {
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 0);
        }
        if (!starterDead)
        {
            PlayerMenu.SetActive(true);
            dialogueText.text = "Starter: Choose an Action.";

            fastball = false;
            slider = false;
            changeup = false;
            curveball = false;
        }
    }

    void MiddleTurn()
    {
        Camera.transform.position = middleCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget);
        GameManager.Instance.DebugBall.transform.position = MiddleReliever.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (middleDead)
        {
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn",1);
        }
        if (!middleDead)
        {
            PlayerMenu.SetActive(true);
            dialogueText.text = "Middle Reliever: Choose an Action.";

            fastball = false;
            slider = false;
            changeup = false;
            curveball = false;
        }
    }

    void SETUPTurn()
    {
        Camera.transform.position = setupCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget);
        GameManager.Instance.DebugBall.transform.position = SetUp.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (SetUpAnim.GetBool("isDead"))
        {
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 2);
        }

        if (setupDead)
        {
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 2);
        }
        if (!setupDead)
        {
            PlayerMenu.SetActive(true);
            dialogueText.text = "Set Up: Choose an Action.";

            fastball = false;
            slider = false;
            changeup = false;
            curveball = false;
        }
    }

    void CloserTurn()
    {
        Camera.transform.position = closerCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget);
        GameManager.Instance.DebugBall.transform.position = Closer.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (CloserAnim.GetBool("isDead"))
        {
            state = BattleStateMultiple.STARTER;
            StarterTurn();
        }
        if (closerDead)
        {
            state = BattleStateMultiple.STARTER;
            StarterTurn();
        }
        if (!closerDead)
        {
            PlayerMenu.SetActive(true);
            dialogueText.text = "Closer: Choose an Action.";

            fastball = false;
            slider = false;
            changeup = false;
            curveball = false;
        }
    }
    #endregion

    public void OnPitchesButton()
    {
        // if (state != BattleStateMultiple.PLAYERTURN)
        //     return;

        PlayerPitches.SetActive(true);
        PlayerMenu.SetActive(false);
    }
    #region Player Pitches
    public void OnFastballButton()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            if (Starter.fastballStamina <= GameManager.StarterEnergy)
            {
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.MIDDLE)
        {
            if (MiddleReliever.fastballStamina <= GameManager.MidRelivEnergy)
            {
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.SETUP)
        {
            if (SetUp.fastballStamina <= GameManager.SetUpEnergy)
            {
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            if (Closer.fastballStamina <= GameManager.CloserEnergy)
            {
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        //if (state != BattleStateMultiple.PLAYERTURN)
        //    return;


    }

    public void OnSliderButton()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            if (Starter.sliderStamina <= GameManager.StarterEnergy)
            {
                slider = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.MIDDLE)
        {
            if (MiddleReliever.sliderStamina <= GameManager.MidRelivEnergy)
            {
                slider = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.SETUP)
        {
            if (SetUp.sliderStamina <= GameManager.SetUpEnergy)
            {
                slider = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            if (Closer.sliderStamina <= GameManager.CloserEnergy)
            {
                slider = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false); 
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        //if (state != BattleStateMultiple.PLAYERTURN)
        //    return;

    }

    public void OnCurveballButton()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            if (Starter.curveballStamina <= GameManager.StarterEnergy)
            {
                curveball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.MIDDLE)
        {
            if (MiddleReliever.curveballStamina <= GameManager.MidRelivEnergy)
            {
                curveball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.SETUP)
        {
            if (SetUp.curveballStamina <= GameManager.SetUpEnergy)
            {
                curveball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            if (Closer.curveballStamina <= GameManager.CloserEnergy)
            {
                curveball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        //if (state != BattleStateMultiple.PLAYERTURN)
        //    return;

    }

    public void OnChangeUpButton()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            if (Starter.changeupStamina <= GameManager.StarterEnergy)
            {
                changeup = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.MIDDLE)
        {
            if (MiddleReliever.changeupStamina <= GameManager.MidRelivEnergy)
            {
                changeup = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.SETUP)
        {
            if (SetUp.changeupStamina <= GameManager.SetUpEnergy)
            {
                changeup = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            if (Closer.changeupStamina <= GameManager.CloserEnergy)
            {
                changeup = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                enemySelect = true;
                ConfirmMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                PlayerMenu.SetActive(false);
            }
            else
                dialogueText.text = "Not enough energy!";
        }

        //if (state != BattleStateMultiple.PLAYERTURN)
        //    return;

    }

    public void OnCancelButton()
    {
        PlayerMenu.SetActive(true);
        PlayerPitches.SetActive(false);
    }
    #endregion
    /*  public void OnHealButton()
      {
          if (state != BattleStateMultiple.PLAYERTURN)
              return;

          //To Do Start Attack Animation
          StartCoroutine(PlayerHeal());
      }*/
    #region End Battle Conditions
    void EndBattle()
    {
        Camera.transform.position = battleCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget);
        if (state == BattleStateMultiple.WON)
        {
            if (!starterDead)
            {
                StarterAnim.Play("Armature|Victory");
            }
            if (!middleDead)
            {
                MidRelAnim.Play("Armature|Victory");
            }
            if (!setupDead)
            {
                SetUpAnim.Play("Armature|Victory");
            }
            if (!closerDead)
            {
                CloserAnim.Play("Armature|Victory");
            }

            dialogueText.text = "You won the Battle!";
            EndingMenu.SetActive(true);
            PlayerPitches.SetActive(false);
            PlayerMenu.SetActive(false);
            if (!isOver)
            {
                AddXP();
            } 
            //Add this later, this is money at the end of the battle. Needs to be a sum of all units
            //GameManager.Money += enemyUnit[enemyUnitSelected].MoneyToDistribute;
            //MoneyText.text = "$ " + enemyUnit[enemyUnitSelected].MoneyToDistribute.ToString("F0");
            isOver = true;
        }
        else if (state == BattleStateMultiple.LOST)
        {
            dialogueText.text = "You lost the battle...";
            StartCoroutine(WaitingAtEndOfBattle());
        }
    }
    public void AddXP()
    {
        if (!starterDead)
        {
            StarterExpGain.text = "   +" + (totalExp / 4).ToString("F0");
            GameManager.StarterExp = totalExp / 4 + GameManager.StarterExp;

            StarterExp(totalExp / 4);
        }
        if (!middleDead)
        {
            MRExpGain.text = "   +" + (totalExp / 4).ToString("F0");
            GameManager.MRExp = totalExp / 4 + GameManager.MRExp;

            MidExp(totalExp / 4);
        }
        if (!setupDead)
        {
            SetUpExpGain.text = "   +" + (totalExp / 4).ToString("F0");
            GameManager.SetUpExp = totalExp / 4 + GameManager.SetUpExp;

            SetUpExp(totalExp / 4);
        }
        if (!closerDead)
        {
            CloserExpGain.text = "   +" + (totalExp / 4).ToString("F0");
            GameManager.CloserExp = totalExp / 4 + GameManager.CloserExp;

            CloserExp(totalExp / 4);
        }
        if (starterDead)
        {
            StarterExpGain.text = "0";
            StarterExpToNext.text = GameManager.StarterTargetExp.ToString("F0");
            StartTotalExp.text = GameManager.StarterLevel.ToString("F0");
        }
        if (middleDead)
        {
            MRExpGain.text = "0";
            MRExpToNext.text = GameManager.MRTargetExp.ToString("F0");
            MRTotalExp.text = GameManager.MRLevel.ToString("F0");
        }
        if (setupDead)
        {
            SetUpExpGain.text = "0";
            SetUpExpToNext.text = GameManager.SetupTargetExp.ToString("F0");
            SetUpTotalExp.text = GameManager.SetUpLevel.ToString("F0");
        }
        if (closerDead)
        {
            CloserExpGain.text = "0";
            CloserExpToNext.text = GameManager.CloserTargetExp.ToString("F0");
            CloserTotalExp.text = GameManager.CloserLevel.ToString("F0");
        }
    }

    void StarterExp(int xp)
    {
        if (!starterDead)
        {
            xp = (totalExp / 4);
            int OldLevelS = GameManager.StarterLevel;

            while (GameManager.StarterExp >= GameManager.StarterTargetExp)
            {
                GameManager.StarterExp = GameManager.StarterExp - GameManager.StarterTargetExp;
                GameManager.StarterLevel++;
                SLevel = true;
                SLevelUp.SetActive(true);
                GameManager.StarterTargetExp *= 2f;
                //add training points
                StarterExpToNext.text = (GameManager.StarterTargetExp - GameManager.StarterExp).ToString("F0");
                int NewLevelS = GameManager.StarterLevel;
                int Difference = NewLevelS - OldLevelS;
                SPointsToGive = (Difference * 3);
                StartTotalExp.text = GameManager.StarterLevel.ToString("F0");
            }
        }
        else
            MidExp(totalExp / 4);
    }

    void MidExp(int xp)
    {
        if (!middleDead)
        {
            xp = (totalExp / 4);
            int OldLevelM = GameManager.MRLevel;

            while (GameManager.MRExp >= GameManager.MRTargetExp)
            {
                GameManager.MRExp = GameManager.MRExp - GameManager.MRTargetExp;
                GameManager.MRLevel++;
                MLevel = true;
                MLevelUp.SetActive(true);
                GameManager.MRTargetExp *= 2f;
                //add training points
                MRExpToNext.text = (GameManager.MRTargetExp - GameManager.MRExp).ToString("F0");
                int NewLevelM = GameManager.MRLevel;
                int Difference = NewLevelM - OldLevelM;
                MPointsToGive = (Difference * 3) + 1;
                MRTotalExp.text = GameManager.MRLevel.ToString("F0");
            }
        }
        else
            SetUpExp(totalExp / 4);
    }

    void SetUpExp(int xp)
    {
        if (!setupDead)
        {
            xp = (totalExp / 4);
            int OldLevelSe = GameManager.SetUpLevel;

            while (GameManager.SetUpExp >= GameManager.SetupTargetExp)
            {
                GameManager.SetUpExp = GameManager.SetUpExp - GameManager.SetupTargetExp;
                GameManager.SetUpLevel++;
                SeLevel = true;
                SetUpLevelUp.SetActive(true);
                GameManager.SetupTargetExp *= 2f;
                //add training points
                SetUpExpToNext.text = (GameManager.SetupTargetExp - GameManager.SetUpExp).ToString("F0");
                int NewLevelSe = GameManager.SetUpLevel;
                int Difference = NewLevelSe - OldLevelSe;
                SePointsToGive = (Difference * 3) + 1;
                SetUpTotalExp.text = GameManager.SetUpLevel.ToString("F0");
            }
        }
        else
            CloserExp(totalExp / 4);
    }

    void CloserExp(int xp)
    {
        if (!closerDead)
        {
            xp = (totalExp / 4);
            int OldLevelC = GameManager.CloserLevel;

            while (GameManager.CloserExp >= GameManager.CloserTargetExp)
            {
                GameManager.CloserExp = GameManager.CloserExp - GameManager.CloserTargetExp;
                GameManager.CloserLevel++;
                CLevel = true;
                CloserLevelUp.SetActive(true);
                GameManager.CloserTargetExp *= 2f;
                //add training points
                CloserExpToNext.text = (GameManager.CloserTargetExp - GameManager.CloserExp).ToString("F0");
                int NewLevelC = GameManager.CloserLevel;
                int Difference = NewLevelC - OldLevelC;
                CPointsToGive = (Difference * 3) + 1;
                CloserTotalExp.text = GameManager.CloserLevel.ToString("F0");
            }
        }
        else
            StartCoroutine(WaitingAtEndOfBattle());
    }

    public void BattleFinished()
    {
        if (isOver)
        {
            print(SPointsToGive + "   " + MPointsToGive + "   " + SePointsToGive + "   " + CPointsToGive);

            Macro.SetActive(true);

            PlayerStatsScreen.SetActive(true);
            SFSlider.value = GameManager.StarterFast;
            SSSlider.value = GameManager.StarterSlid;
            SCSlider.value = GameManager.StarterCurve;
            SChSlider.value = GameManager.StarterChange;
            SASlider.value = GameManager.StarterAgil;

            MFSlider.value = GameManager.MiddleFast;
            MSSlider.value = GameManager.MiddleSlid;
            MCSlider.value = GameManager.MiddleCurve;
            MChSlider.value = GameManager.MiddleChange;
            MASlider.value = GameManager.MiddleAgil;

            SeFSlider.value = GameManager.SetUpFast;
            SeSSlider.value = GameManager.SetUpSlid;
            SeCSlider.value = GameManager.SetUpCurve;
            SeChSlider.value = GameManager.SetUpChange;
            SeASlider.value = GameManager.SetUpAgil;

            CFSlider.value = GameManager.CloserFast;
            CSSlider.value = GameManager.CloserSlid;
            CCSlider.value = GameManager.CloserCurve;
            CChSlider.value = GameManager.CloserChange;
            CASlider.value = GameManager.CloserAgil;

            SPoints.text = SPointsToGive.ToString();

            if (SLevel)
            {
                PlayerStatsScreen.SetActive(true);
                SLevelUpScreen.SetActive(true);
                starterLevel = true;
               // StarterLevelUp();

                FastballButton.SetActive(true);
                SliderButton.SetActive(true);
                CurveballButton.SetActive(true);
                ChangeUpButton.SetActive(true);
                AgilityButton.SetActive(true);
            }
            if (!SLevel && MLevel)
            {
                middleLevel = true;
                MLevelUpScreen.SetActive(true);
              // MidRelieverLevelUp();

                FastballButton.SetActive(true);
                SliderButton.SetActive(true);
                CurveballButton.SetActive(true);
                ChangeUpButton.SetActive(true);
                AgilityButton.SetActive(true);
            }
            if (!SLevel && !MLevel && SeLevel)
            {
                setUpLevel = true;
                SeLevelUpScreen.SetActive(true);
               // SetLevelUp();

                FastballButton.SetActive(true);
                SliderButton.SetActive(true);
                CurveballButton.SetActive(true);
                ChangeUpButton.SetActive(true);
                AgilityButton.SetActive(true);
            }
            if (!SLevel && !MLevel && !SeLevel && CLevel)
            {
                closerLevel = true;
                CLevelUpScreen.SetActive(true);
              //  CloseLevelUp();

                FastballButton.SetActive(true);
                SliderButton.SetActive(true);
                CurveballButton.SetActive(true);
                ChangeUpButton.SetActive(true);
                AgilityButton.SetActive(true);
            }
            EndingMenu.SetActive(false);
            if (!SLevel && !MLevel && !SeLevel && !CLevel)
              {
                  StartCoroutine(WaitingAtEndOfBattle());
              }
        }
    }

    void StarterLevelUp()
    {
        if (MLevel)
        {
            starterLevel = false;
            middleLevel = true;
            MLevelUpScreen.SetActive(true);
            SLevelUpScreen.SetActive(false);
        }
        if (!MLevel && SeLevel)
        {
            starterLevel = false;
            setUpLevel = true;
            SeLevelUpScreen.SetActive(true);
            MLevelUpScreen.SetActive(false);
        }
        if (!SeLevel && CLevel)
        {
            starterLevel = false;
            closerLevel = true;
            CLevelUpScreen.SetActive(true);
            SeLevelUpScreen.SetActive(false);
        }
        if (!MLevel && !SeLevel && !CLevel)
        {
            starterLevel = false;
            StartCoroutine(WaitingAtEndOfBattle());
        }
    }

    void MidRelieverLevelUp()
    {
        MPoints.text = MPointsToGive.ToString();

        if (MLevel)
        {
            middleLevel = true; 
            SLevelUpScreen.SetActive(false);
            MLevelUpScreen.SetActive(true);
        }
        if (!MLevel && SeLevel)
        {
            middleLevel = false;
            setUpLevel = true;
            SeLevelUpScreen.SetActive(true);
            MLevelUpScreen.SetActive(false);
            SLevelUpScreen.SetActive(false);
        }
        if (!MLevel && !SeLevel && CLevel)
        {
            middleLevel = false;
            closerLevel = true;
            SLevelUpScreen.SetActive(false);
            MLevelUpScreen.SetActive(false);
            CLevelUpScreen.SetActive(true);
        }
        if (!SeLevel && !CLevel)
        {
            middleLevel = false;
            StartCoroutine(WaitingAtEndOfBattle());
        }
    }

    void SetLevelUp()
    {
        SePoints.text = SePointsToGive.ToString();

        if (SeLevel)
        {
            setUpLevel = true;
            SLevelUpScreen.SetActive(false);
            MLevelUpScreen.SetActive(false);
            SeLevelUpScreen.SetActive(true);
        }
        if (!SeLevel && CLevel)
        {
            setUpLevel = false;
            closerLevel = true;
            CLevelUpScreen.SetActive(true);
            SeLevelUpScreen.SetActive(false);
            MLevelUpScreen.SetActive(false);
            SLevelUpScreen.SetActive(false);
            CloseLevelUp();
        }
    }

    void CloseLevelUp()
    {
        CPoints.text = CPointsToGive.ToString();

        CLevelUpScreen.SetActive(true);
        SLevelUpScreen.SetActive(false);
        MLevelUpScreen.SetActive(false);
        SeLevelUpScreen.SetActive(false);

        if (CLevel)
        {
            setUpLevel = false;
            closerLevel = true;
        }
        if (!CLevel)
        {
            closerLevel = false;
            StartCoroutine(WaitingAtEndOfBattle());
        }
    }
    #endregion

    #region PitchLevelUp
    public void FastballIncrease()
    {
        if (starterLevel)
        {
            if (SPointsToGive > 0)
            {
                GameManager.StarterFast++;
                SPointsToGive--;
                SFSlider.value = GameManager.StarterFast;
                SPoints.text = SPointsToGive.ToString();
            }
            if (SPointsToGive <= 0)
            {
                starterLevel = false;
                MidRelieverLevelUp();
            }
        }

        if (middleLevel)
        { 
            if (MPointsToGive > 0)
            {
                GameManager.MiddleFast++;
                MPointsToGive--;
                MFSlider.value = GameManager.MiddleFast;
                MPoints.text = MPointsToGive.ToString();
            }
            if (MPointsToGive <= 0)
            {
                middleLevel = false;
                SetLevelUp();
            }
        }

        if (setUpLevel)
        {
            if (SePointsToGive > 0)
            {
                GameManager.SetUpFast++;
                SePointsToGive--;
                SeFSlider.value = GameManager.SetUpFast;
                SePoints.text = SePointsToGive.ToString();
            }
            if (SePointsToGive <= 0)
            {
                setUpLevel = false;
                if (!CLevel)
                {
                    setUpLevel = false;
                    StartCoroutine(WaitingAtEndOfBattle());
                }
                else
                    CloseLevelUp();
            }
        }

        if (closerLevel)
        {
            if (CPointsToGive > 0)
            {
                GameManager.CloserFast++;
                CPointsToGive--;
                CFSlider.value = GameManager.CloserFast;
                CPoints.text = CPointsToGive.ToString();
            }
            if (CPointsToGive <= 0)
            {
                closerLevel = false;
                StartCoroutine(WaitingAtEndOfBattle());
            }
        }
    }

    public void SliderIncrease()
    {
        if (starterLevel)
        {
            if (SPointsToGive > 0)
            {
                GameManager.StarterSlid++;
                SPointsToGive--;
                SSSlider.value = GameManager.StarterSlid;
                SPoints.text = SPointsToGive.ToString();
            }
            if (SPointsToGive <= 0)
            {
                starterLevel = false;
                MidRelieverLevelUp();
            }
        }

        if (middleLevel)
        {
            if (MPointsToGive > 0)
            {
                GameManager.MiddleSlid++;
                MPointsToGive--;
                MSSlider.value = GameManager.MiddleSlid;
                MPoints.text = MPointsToGive.ToString();
            }
            if (MPointsToGive <= 0)
            {
                middleLevel = false;
                SetLevelUp();
            }
        }

        if (setUpLevel)
        {
            if (SePointsToGive > 0)
            {
                GameManager.SetUpSlid++;
                SePointsToGive--;
                SeSSlider.value = GameManager.SetUpSlid;
                SePoints.text = SePointsToGive.ToString();
            }
            if (SePointsToGive <= 0)
            {
                setUpLevel = false;
                if (!CLevel)
                {
                    setUpLevel = false;
                    StartCoroutine(WaitingAtEndOfBattle());
                }
                else
                    CloseLevelUp();
            }
        }

        if (closerLevel)
        {
            if (CPointsToGive > 0)
            {
                GameManager.CloserSlid++;
                CPointsToGive--;
                CSSlider.value = GameManager.CloserSlid;
                CPoints.text = CPointsToGive.ToString();
            }
            if (CPointsToGive <= 0)
            {
                closerLevel = false;
                StartCoroutine(WaitingAtEndOfBattle());
            }
        }
    }

    public void CurveballIncrease()
    {
        if (starterLevel)
        {
            if (SPointsToGive > 0)
            {
                GameManager.StarterCurve++;
                SPointsToGive--;
                SCSlider.value = GameManager.StarterCurve;
                SPoints.text = SPointsToGive.ToString();
            }
            if (SPointsToGive <= 0)
            {
                starterLevel = false;
                MidRelieverLevelUp();
            }
        }

        if (middleLevel)
        {
            if (MPointsToGive > 0)
            {
                GameManager.MiddleCurve++;
                MPointsToGive--;
                MCSlider.value = GameManager.MiddleCurve;
                MPoints.text = MPointsToGive.ToString();
            }
            if (MPointsToGive <= 0)
            {
                middleLevel = false;
                SetLevelUp();
            }
        }

        if (setUpLevel)
        {
            if (SePointsToGive > 0)
            {
                GameManager.SetUpCurve++;
                SePointsToGive--;
                SeCSlider.value = GameManager.SetUpCurve;
                SePoints.text = SePointsToGive.ToString();
            }
            if (SePointsToGive <= 0)
            {
                setUpLevel = false;
                if (!CLevel)
                {
                    setUpLevel = false;
                    StartCoroutine(WaitingAtEndOfBattle());
                }
                else
                    CloseLevelUp();
            }
        }

        if (closerLevel)
        {
            if (CPointsToGive > 0)
            {
                GameManager.CloserCurve++;
                CPointsToGive--;
                CCSlider.value = GameManager.CloserCurve;
                CPoints.text = CPointsToGive.ToString();
            }
            if (CPointsToGive <= 0)
            {
                closerLevel = false;
                StartCoroutine(WaitingAtEndOfBattle());
            }
        }
    }

    public void ChangeUpIncrease()
    {
        if (starterLevel)
        {
            if (SPointsToGive > 0)
            {
                GameManager.StarterChange++;
                SPointsToGive--;
                SChSlider.value = GameManager.StarterChange;
                SPoints.text = SPointsToGive.ToString();
            }
            if (SPointsToGive <= 0)
            {
                starterLevel = false;
                MidRelieverLevelUp();
            }
        }

        if (middleLevel)
        {
            if (MPointsToGive > 0)
            {
                GameManager.MiddleChange++;
                MPointsToGive--;
                MChSlider.value = GameManager.MiddleChange;
                MPoints.text = MPointsToGive.ToString();
            }
            if (MPointsToGive <= 0)
            {
                middleLevel = false;
                SetLevelUp();
            }
        }

        if (setUpLevel)
        {
            if (SePointsToGive > 0)
            {
                GameManager.SetUpChange++;
                SePointsToGive--;
                SeChSlider.value = GameManager.SetUpChange;
                SePoints.text = SePointsToGive.ToString();
            }
            if (SePointsToGive <= 0)
            {
                setUpLevel = false;
                if (!CLevel)
                {
                    setUpLevel = false;
                    StartCoroutine(WaitingAtEndOfBattle());
                }
                else
                    CloseLevelUp();
            }
        }

        if (closerLevel)
        {
            if (CPointsToGive > 0)
            {
                GameManager.CloserChange++;
                CPointsToGive--;
                CChSlider.value = GameManager.CloserChange;
                CPoints.text = CPointsToGive.ToString();
            }
            if (CPointsToGive <= 0)
            {
                closerLevel = false;
                StartCoroutine(WaitingAtEndOfBattle());
            }
        }
    }
    public void AgilityIncrease()
    {
        if (starterLevel)
        {
            if (SPointsToGive > 0)
            {
                GameManager.StarterAgil++;
                SPointsToGive--;
                SASlider.value = GameManager.StarterAgil;
                SPoints.text = SPointsToGive.ToString();
            }
            if (SPointsToGive <= 0)
            {
                starterLevel = false;
                MidRelieverLevelUp();
            }
        }

        if (middleLevel)
        {
            if (MPointsToGive > 0)
            {
                GameManager.MiddleAgil++;
                MPointsToGive--;
                MASlider.value = GameManager.MiddleAgil;
                MPoints.text = MPointsToGive.ToString();
            }
            if (MPointsToGive <= 0)
            {
                middleLevel = false;
                SetLevelUp();
            }
        }

        if (setUpLevel)
        {
            if (SePointsToGive > 0)
            {
                GameManager.SetUpAgil++;
                SePointsToGive--;
                SeASlider.value = GameManager.SetUpAgil;
                SePoints.text = SePointsToGive.ToString();
            }
            if (SePointsToGive <= 0)
            {
                setUpLevel = false;
                if (!CLevel)
                {
                    setUpLevel = false;
                    StartCoroutine(WaitingAtEndOfBattle());
                }
                else
                    CloseLevelUp();
            }
        }

        if (closerLevel)
        {
            if (CPointsToGive > 0)
            {
                GameManager.CloserAgil++;
                CPointsToGive--;
                CASlider.value = GameManager.CloserAgil;
                CPoints.text = CPointsToGive.ToString();
            }
            if (CPointsToGive <= 0)
            {
                closerLevel = false;
                StartCoroutine(WaitingAtEndOfBattle());
            }
        }
    }
    #endregion

    IEnumerator WaitingAtEndOfBattle()
    {
        yield return new WaitForSeconds(1.5f);
        //Return to Main Menu
         SceneManager.LoadScene("Concourse");
    }
}
