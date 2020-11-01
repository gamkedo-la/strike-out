using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This is the definition of the Game States
public enum BattleStateMultiple { START, STARTER, MIDDLE, SETUP, CLOSER, ENEMYTURN, WON, LOST }

public class BattleSystemMultiple : MonoBehaviour
{
    public bool isConcourse;
    public bool isHoE;

    public static bool inBattle; 
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
    public GameObject Camera, cutsceneCam;
    public Transform starterCam, middleCam, setupCam, closerCam, enemyCam, battleCam, enemyCamTarget;

    //bools to determine enemy count
    public bool Boss;
    int enemyStartCount = 0;

    //cutscene cam anim 
    public Animator cutSceneCamAnim;
    public bool announcer;
    public GameObject cutSceneCamSecond;

    GameObject InventoryManage;
    public Text InventoryItemPostBattle;

    //Boss Bool Triggers
    public bool McGee, Announcer, Umpire, TheBabe;

    //Damage UI against players
    public Text StarterDamageUI, MiddleDamageUI, SetUpDamageUI, CloserDamageUI;

    //Announcer Cut Scene Cam
    public Transform CutSceneCamTarget;

    //Win / Lose Audio
    public AudioEventGeneric victory;
    public AudioEventGeneric defeat;
    public MusicHandler battleMusic;

    //New Battle Structure
    public enum CharacterIdentifier
    {        Starter, Middle, SetUp, Closer, Enemy1, Enemy2, Enemy3, Enemy4, Enemy5    };

    public List<CharacterIdentifier> playerTurnOrder = new List<CharacterIdentifier>();
    public List<CharacterIdentifier> enemyTurnOrder = new List<CharacterIdentifier>();


    public bool isPlayerTurn;

    public bool isHoEBattle;

    //Turn Order Display
    public Text ondeck, inhole;

    //babe
    public bool Babe;

    //movingBall
    public GameObject movingBall;

    bool gameOverToPreventDuplicates;
    bool preventingAddXPDup;

    private void Start()
    {
        inBattle = true;
        InventoryManage = GameObject.Find("Inventory");

        StarterDamageUI.text = "".ToString();
        MiddleDamageUI.text = "".ToString();
        SetUpDamageUI.text = "".ToString();
        CloserDamageUI.text = "".ToString();

        playerTurnOrder.Add(CharacterIdentifier.Starter);
        playerTurnOrder.Add(CharacterIdentifier.Middle);
        playerTurnOrder.Add(CharacterIdentifier.SetUp);
        playerTurnOrder.Add(CharacterIdentifier.Closer);

        if (Announcer)
        {
            Camera.transform.position = CutSceneCamTarget.transform.position;
            Camera.transform.rotation = CutSceneCamTarget.transform.rotation;
        }
        else if (isHoEBattle)
        {
            Camera.SetActive(false);
            //battleCam.transform.position = new Vector3(-14.45f, 3.71f, -14.82f);
        }

        else
        {
            Camera.transform.position = battleCam.transform.position;
            Camera.transform.LookAt(enemyCamTarget.transform.position);
        }
        state = BattleStateMultiple.START;

        print(enemyBattleStationLocations.Count);

        if (GameManager.StarterMorale <= 0)
        {
            GameManager.StarterMorale = 1;
        }
        if (GameManager.MidRelivMorale <= 0)
        {
            GameManager.MidRelivMorale = 1;
        }
        if (GameManager.SetUpMorale <= 0)
        {
            GameManager.SetUpMorale = 1;
        }
        if (GameManager.CloserMorale <= 0)
        {
            GameManager.CloserMorale = 1;
        }

        if (Boss)
        {
            enemyStartCount = 1;
        }

        else
        {
            int RandRangeEnemySpawn = Random.Range(0, 100);

            if (RandRangeEnemySpawn < 14)
            {
                enemyStartCount = 1;
            }
            else if (RandRangeEnemySpawn <= 39)
            {
                enemyStartCount = 2;
            }
            else if (RandRangeEnemySpawn <= 69)
            {
                enemyStartCount = 3;
            }
            else if (RandRangeEnemySpawn <= 89)
            {
                enemyStartCount = 4;
            }
            else
            {
                enemyStartCount = 5;
            }
        }

        enemyTurnOrder.Add(CharacterIdentifier.Enemy1);

        if (enemyStartCount >= 2)
        {
            enemyTurnOrder.Add(CharacterIdentifier.Enemy2);
        }
        if (enemyStartCount >= 3)
        {
            enemyTurnOrder.Add(CharacterIdentifier.Enemy3);
        }
        if (enemyStartCount >= 4)
        {
            enemyTurnOrder.Add(CharacterIdentifier.Enemy4);
        }
        if (enemyStartCount >= 5)
        {
            enemyTurnOrder.Add(CharacterIdentifier.Enemy5);
        }



        for (int i = 0; i < enemyStartCount; i++)
        {
            enemyCount++;
            //enemyPrefab[i].SetActive(true);

        }

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


        for (int i = 0; i < enemyStartCount; i++)
        {
            int RandEnemy = Random.Range(0, enemyPrefab.Count);
            GameObject enemyGO = Instantiate(enemyPrefab[RandEnemy], enemyBattleStationLocations[i]);
            enemyUnit.Add(enemyGO.GetComponent<Unit>());
            enemyAnim.Add(enemyGO.GetComponentInChildren<Animator>());
        }
        enemyUnit[0].GetComponent<Unit>().myEnumValue = CharacterIdentifier.Enemy1;
        if (enemyStartCount >= 2)
        {
            enemyUnit[1].GetComponent<Unit>().myEnumValue = CharacterIdentifier.Enemy2;
        }
        if (enemyStartCount >= 3)
        {
            enemyUnit[2].GetComponent<Unit>().myEnumValue = CharacterIdentifier.Enemy3;
        }
        if (enemyStartCount >= 4)
        {
            enemyUnit[3].GetComponent<Unit>().myEnumValue = CharacterIdentifier.Enemy4;
        }
        if (enemyStartCount >= 5)
        {
            enemyUnit[4].GetComponent<Unit>().myEnumValue = CharacterIdentifier.Enemy5;
        }

        if (Announcer)
        {
            yield return new WaitForSeconds(6f);
        }
        else if (isHoE)
        {
            yield return new WaitForSeconds(4f);
            Camera.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(4f);
        }
        cutsceneCam.SetActive(false);



        if (GameManager.enemyAttackedPlayer)
        {
            isPlayerTurn = true;
        }
        else
        {
           // state = BattleStateMultiple.STARTER;
           // StarterTurn();
            isPlayerTurn = false;
        }
        NextTurn();
    }

    private void Update()
    {
        //Cheat Code to Win Battle
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheatToInstantlyWin();
            }
        }


        if (GameManager.StarterMorale <= 0)
        {
            playerTurnOrder.Remove(CharacterIdentifier.Starter);
        }

        if (GameManager.MidRelivMorale <= 0)
        {
            playerTurnOrder.Remove(CharacterIdentifier.Middle);
        }

        if (GameManager.SetUpMorale <= 0)
        {
            playerTurnOrder.Remove(CharacterIdentifier.SetUp);
        }

        if (GameManager.CloserMorale <= 0)
        {
            playerTurnOrder.Remove(CharacterIdentifier.Closer);
        }
        //Adding
        if (GameManager.StarterMorale >= 0 && starterDead)
        {
            StarterAnim.SetBool("isDead", false);
            starterDead = false;
            playerTurnOrder.Add(CharacterIdentifier.Starter);
        }

        if (GameManager.MidRelivMorale >= 0 && middleDead)
        {
            MidRelAnim.SetBool("isDead", false);
            middleDead = false;
            playerTurnOrder.Add(CharacterIdentifier.Middle);
        }

        if (GameManager.SetUpMorale >= 0 && setupDead)
        {
            SetUpAnim.SetBool("isDead", false);
            setupDead = false;
            playerTurnOrder.Add(CharacterIdentifier.SetUp);
        }

        if (GameManager.CloserMorale >= 0 && closerDead)
        {
            CloserAnim.SetBool("isDead", false);
            closerDead = false;
            playerTurnOrder.Add(CharacterIdentifier.Closer);
        }
        
        #region Select Enemy
        if ((state == BattleStateMultiple.STARTER || state == BattleStateMultiple.MIDDLE || state == BattleStateMultiple.SETUP || state == BattleStateMultiple.CLOSER) && enemySelect)
        {
            //SelectionProcess
            enemySelectionParticle.SetActive(true);
            enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            if (Input.GetKeyDown(KeyCode.A) && enemyStartCount > 1)
            {
                enemyUnitSelected--;
                if (enemyUnitSelected < 0)
                {
                    enemyUnitSelected = enemyBattleStationLocations.Count - (6 - enemyStartCount);
                }

                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
                Camera.transform.LookAt(enemyBattleStationLocations[enemyUnitSelected]);
            }
            if (Input.GetKeyDown(KeyCode.D) && enemyStartCount > 1)
            {
                enemyUnitSelected++;
                if (enemyUnitSelected >= enemyStartCount)
                {
                    enemyUnitSelected = 0;
                }

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

        #endregion
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

        #endregion
    }

    private void DebugPrintList(List<CharacterIdentifier> forList)
    {
        string listStr = "";
        for (int i = 0; i < forList.Count; i++)
        {
            listStr += forList[i].ToString() + ", ";
        }
        Debug.Log(listStr);
    }

    void NextTurn()
      {
        isPlayerTurn = !isPlayerTurn;

        CharacterIdentifier upRightNow;
        if (isPlayerTurn)
        {
            DebugPrintList(playerTurnOrder);
            upRightNow = playerTurnOrder[0];
            playerTurnOrder.RemoveAt(0);
            playerTurnOrder.Add(upRightNow);
        }

        else 
        {
            upRightNow = enemyTurnOrder[0];
            enemyTurnOrder.RemoveAt(0);
            enemyTurnOrder.Add(upRightNow);
        }


      //  Debug.Log("NextTurnCalled: " + upRightNow);

        switch(upRightNow)
        {
            case CharacterIdentifier.Starter:
         //       print("Starter");
                StarterTurn();
                state = BattleStateMultiple.STARTER;
                break;
            case CharacterIdentifier.Middle:
         //       print("Middle");
                MiddleTurn();
                state = BattleStateMultiple.MIDDLE;
                break;
            case CharacterIdentifier.SetUp:
         //       print("SetUp");
                SETUPTurn();
                state = BattleStateMultiple.SETUP;
                break;
            case CharacterIdentifier.Closer:
         //       print("Closer");
                CloserTurn();
                state = BattleStateMultiple.CLOSER;
                break;
            case CharacterIdentifier.Enemy1:
          //      print("Enemy1");
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine("EnemyTurn", 0);
                break;
            case CharacterIdentifier.Enemy2:
         //       print("Enemy2");
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine("EnemyTurn", 1);
                break;
            case CharacterIdentifier.Enemy3:
           //     print("Enemy3");
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine("EnemyTurn", 2);
                break;
            case CharacterIdentifier.Enemy4:
          //      print("Enemy4");
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine("EnemyTurn", 3);
                break;
            case CharacterIdentifier.Enemy5:
            //    print("Enemy5");
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine("EnemyTurn", 4);
                break;
         }
        if (isPlayerTurn)
        {
            ondeck.text = "On Deck:  " + ReturnNameOfEnemy(enemyTurnOrder[0]);
            inhole.text = "In the Hole:  " + playerTurnOrder[0].ToString();
        }

        if (!isPlayerTurn)
        {
            ondeck.text = "On Deck:  " + playerTurnOrder[0].ToString();
            inhole.text = "In the Hole:  " + ReturnNameOfEnemy(enemyTurnOrder[0]);
        }

    }

    private string ReturnNameOfEnemy(CharacterIdentifier forID)
    {
        for (int i = 0; i < enemyUnit.Count; i++)
        {
            if (enemyUnit[i].myEnumValue == forID)
            {
                string nameToRemoveCloneFrom = enemyUnit[i].name;
                nameToRemoveCloneFrom = nameToRemoveCloneFrom.Replace("(Clone)", "");
                return nameToRemoveCloneFrom;
            }
        }
        return "Error: No Match Found";
    }


    public void CallTime()
    {
        if (state == BattleStateMultiple.STARTER)
        {
            GameManager.StarterEnergy += 5;
            GameManager.StarterMorale += 5;

            if (GameManager.StarterEnergy >= GameManager.StarterEnergyMax)
            {
                GameManager.StarterEnergy = GameManager.StarterEnergyMax;
            }
            if (GameManager.StarterMorale >= GameManager.StarterMoraleMax)
            {
                GameManager.StarterMorale = GameManager.StarterMoraleMax;
            }

            for (int i = 0; i < enemyUnit.Count; i++)
            {
                if (enemyUnit[i].currentHP > 0)
                {
                    enemyUnit[i].TakeDamage(-5);
                }
            }
        }

        if (state == BattleStateMultiple.MIDDLE)
        {
            GameManager.MidRelivEnergy += 5;
            GameManager.MidRelivMorale += 5;

            if (GameManager.MidRelivEnergy >= GameManager.MidRelievEnergyMax)
            {
                GameManager.MidRelivEnergy = GameManager.MidRelievEnergyMax;
            }
            if (GameManager.MidRelivMorale >= GameManager.MidRelivMoraleMax)
            {
                GameManager.MidRelivMorale = GameManager.MidRelivMoraleMax;
            }

            for (int i = 0; i < enemyUnit.Count; i++)
            {
                if (enemyUnit[i].currentHP > 0)
                {
                    enemyUnit[i].TakeDamage(-5);
                }
            }
        }

        if (state == BattleStateMultiple.SETUP)
        {
            GameManager.SetUpEnergy += 5;
            GameManager.SetUpMorale += 5;

            if (GameManager.SetUpEnergy >= GameManager.SetUpEnergyMax)
            {
                GameManager.SetUpEnergy = GameManager.SetUpEnergyMax;
            }
            if (GameManager.SetUpMorale >= GameManager.SetUpMoraleMax)
            {
                GameManager.SetUpMorale = GameManager.SetUpMoraleMax;
            }

            for (int i = 0; i < enemyUnit.Count; i++)
            {
                if (enemyUnit[i].currentHP > 0)
                {
                    enemyUnit[i].TakeDamage(-5);
                }
            }
        }

        if (state == BattleStateMultiple.CLOSER)
        {
            GameManager.CloserEnergy += 5;
            GameManager.CloserMorale += 5;

            if (GameManager.CloserEnergy >= GameManager.CloserEnergyMax)
            {
                GameManager.CloserEnergy = GameManager.CloserEnergyMax;
            }
            if (GameManager.CloserMorale >= GameManager.CloserMoraleMax)
            {
                GameManager.CloserMorale = GameManager.CloserMoraleMax;
            }

            for (int i = 0; i < enemyUnit.Count; i++)
            {
                if (enemyUnit[i].currentHP > 0)
                {
                    enemyUnit[i].TakeDamage(-5);
                }
            }
        }
        AdvanceTurn();
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
                totalExp += enemyUnit[i].ExperienceToDistribute;
                enemyCount--;
                RemoveCurrentEnemy();
            }

            if (enemyCount > 0)
            {
                AdvanceTurn();
            }
            else
            {
                backButtonItem.SetActive(false);
                PlayerMenu.SetActive(false);
                PlayerPitches.SetActive(false);
                ItemMenu.transform.localPosition = new Vector3(233, -900, 0);

                state = BattleStateMultiple.WON;
                EndBattle();
            }
        }
    }

    public void ScoutingReportItem()
    {
        isDead = enemyUnit[enemyUnitSelected].TakeDamage(20);

            if (isDead)
            {
                totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
            RemoveCurrentEnemy();
            enemyUnitSelected = 0;
                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            }
        if (enemyCount > 0)
        {
            AdvanceTurn();
        }
        else
        {
            backButtonItem.SetActive(false);
            PlayerMenu.SetActive(false);
            PlayerPitches.SetActive(false);
            ItemMenu.transform.localPosition = new Vector3(233, -900, 0);

            state = BattleStateMultiple.WON;
            EndBattle();
        }
    }

    public void ItemMenuButton()
    {
        ItemMenu.transform.localPosition = new Vector3(233, 20, 0);
        print(ItemMenu.name);
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

        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);

        StarterEnergy.value = (GameManager.StarterEnergy / GameManager.StarterEnergyMax);
        MiddleEnergy.value = (GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax);
        SetUpEnergy.value = (GameManager.SetUpEnergy / GameManager.SetUpEnergyMax);
        CloserEnergy.value = (GameManager.CloserEnergy / GameManager.CloserEnergyMax);

        if (GameManager.StarterMorale > (GameManager.StarterMoraleMax * .2))
        {
            StarterAnim.SetBool("isInjured", false);
        }     
        //MR
        if (GameManager.MidRelivMorale > (GameManager.MidRelivMoraleMax * .2))
        {
            MidRelAnim.SetBool("isInjured", false);
        }        
        //SetUp
        if (GameManager.SetUpMorale > (GameManager.SetUpMoraleMax * .2))
        {
            SetUpAnim.SetBool("isInjured", false);
        }       
        //Closer
        if (GameManager.CloserMorale > (GameManager.CloserMoraleMax * .2))
        {
            CloserAnim.SetBool("isInjured", false);
        }
        NextTurn();
    }

    IEnumerator CloserToStarterWait()
    {
        yield return new WaitForSeconds(0.5f);
        StarterTurn();
    }
    #endregion

    public void CallBullpen()
    {
        if (Babe || Announcer || McGee || Umpire)
        {
            dialogueText.text = "You can't run from this fight!";
            StartCoroutine(WaitingForCall());
        }
        else
        {
            int Rand = Random.Range(0, 2);
            if (Rand == 0)
            {
                dialogueText.text = "No one is warming up! You can't leave!";
                PlayerMenu.SetActive(false);
                NextTurn();
            }
            if (Rand == 1)
            {
                dialogueText.text = "The Manager is signaling a replacement, you get away!";
                StartCoroutine(WaitingAtEndOfBattle());
            }
        }

    }

    public void ConfirmAttack()
    {
        //Starter
        if (state == BattleStateMultiple.STARTER)
        {
            StartCoroutine(PlayerAttack());
        }
        //Middle
        if (state == BattleStateMultiple.MIDDLE)
        {
            StartCoroutine(MiddleAttack());
        }
        //setup
        if (state == BattleStateMultiple.SETUP)
        {
            StartCoroutine(SetUpAttack());
        }
        //Closer
        if (state == BattleStateMultiple.CLOSER)
        {
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

    void RemoveCurrentEnemy()
    {
        enemyTurnOrder.Remove(enemyUnit[enemyUnitSelected].myEnumValue);
        totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
    }

    #region PlayerAttack - animations and Damage
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    GameManager.StarterEnergy -= Starter.fastballStamina;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                    StarterAnim.Play("StarterWindup");
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(Starter.fastballDamage + GameManager.StarterFast);

                    fastball = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                   //     totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    GameManager.StarterEnergy -= Starter.sliderStamina;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                    StarterAnim.Play("StarterWindup");
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(Starter.sliderDamage + GameManager.StarterSlid);
                    slider = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                      //  totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    GameManager.StarterEnergy -= Starter.curveballStamina;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                    StarterAnim.Play("StarterWindup");
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(Starter.curveballDamage + GameManager.StarterCurve);
                    curveball = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
                        enemyCount--;
                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    GameManager.StarterEnergy -= Starter.changeupStamina;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                    StarterAnim.Play("StarterWindup");
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(Starter.changeupDamage + GameManager.StarterChange);
                    changeup = false;
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                       // totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
                    }
                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    MidRelAnim.Play("StarterWindup");
                    GameManager.MidRelivEnergy -= MiddleReliever.fastballStamina;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(MiddleReliever.fastballDamage + GameManager.MiddleFast);
                    fastball = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                       // totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                        }
                    }

                NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    MidRelAnim.Play("StarterWindup");
                    GameManager.MidRelivEnergy -= MiddleReliever.sliderStamina;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(MiddleReliever.sliderDamage + GameManager.MiddleSlid);
                    slider = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                    //    totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    MidRelAnim.Play("StarterWindup");
                    GameManager.MidRelivEnergy -= MiddleReliever.curveballStamina;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(MiddleReliever.curveballDamage + GameManager.MiddleCurve);
                    curveball = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                      //  totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    MidRelAnim.Play("StarterWindup");
                    GameManager.MidRelivEnergy -= MiddleReliever.changeupStamina;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(MiddleReliever.changeupDamage + GameManager.MiddleChange);
                    changeup = false;
                    //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                     //   totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }
                NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    SetUpAnim.Play("StarterWindup");
                    GameManager.SetUpEnergy -= SetUp.fastballStamina;

                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(SetUp.fastballDamage + GameManager.SetUpFast);
                    fastball = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                    //    totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                       enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    SetUpAnim.Play("StarterWindup");
                    GameManager.SetUpEnergy -= SetUp.sliderStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(SetUp.sliderDamage + GameManager.SetUpSlid);
                    slider = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                     //   totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    SetUpAnim.Play("StarterWindup");
                    GameManager.SetUpEnergy -= SetUp.curveballStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(SetUp.curveballDamage + GameManager.SetUpCurve);
                    curveball = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                      //  totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    SetUpAnim.Play("StarterWindup");
                    GameManager.SetUpEnergy -= SetUp.changeupStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(SetUp.changeupDamage + GameManager.SetUpChange);
                    changeup = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);


                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                    //    totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    CloserAnim.Play("StarterWindup");
                    GameManager.CloserEnergy -= Closer.fastballStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageFast(Closer.fastballDamage + GameManager.CloserFast);
                    fastball = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                      //  totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    CloserAnim.Play("StarterWindup");
                    GameManager.CloserEnergy -= Closer.sliderStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageSlid(Closer.sliderDamage + GameManager.CloserSlid);
                    slider = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                     //   totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    CloserAnim.Play("StarterWindup");
                    GameManager.CloserEnergy -= Closer.curveballStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageCurve(Closer.curveballDamage + GameManager.CloserCurve);
                    curveball = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                      //  totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
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
                    PlayerPitches.SetActive(true);
                    PlayerMenu.SetActive(false);
                }
                else
                {
                    CloserAnim.Play("StarterWindup");
                    GameManager.CloserEnergy -= Closer.changeupStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                    yield return new WaitForSeconds(2f);
                    isDead = enemyUnit[enemyUnitSelected].TakeDamageChange(Closer.changeupDamage + GameManager.CloserChange);
                    changeup = false;

                    dialogueText.text = "The attack is successful!";
                    yield return new WaitForSeconds(2f);

                    //This checks to see if the Enemy is Dead or has HP remaining
                    if (isDead)
                    {
                        RemoveCurrentEnemy();

                     //   totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                        enemyCount--;

                        enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                    }

                    NextTurn();
                }
            }
           
        }
    }
    #endregion

    #region Enemy Turn
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

    IEnumerator EnemyTurn(int enemyIndex)
    {
        Camera.transform.position = enemyCam.transform.position;
        Camera.transform.LookAt(Starter.transform.position);
       // GameManager.Instance.DebugBall.transform.position = enemyUnit[enemyIndex].transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (enemyUnit[enemyIndex].currentHP <= 0)
        {
           // NextPlayerTurnAfterEnemyTurn(enemyIndex);
        }
        else
        {
            if (starterDead && middleDead && setupDead && closerDead)
            {
                EndBattle();
            }

            enemyUnit[enemyUnitSelected].DetermineAttack();

            if (Unit.attackAll)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Everyone with " + enemyUnit[enemyUnitSelected].attackName + "!";

                enemyAnim[enemyIndex].Play("Armature|Swing");

                if (Announcer)
                {
                    yield return new WaitForSeconds(3.5f);
                }
                else
                { 
                yield return new WaitForSeconds(2f);
                }

                bool isDead1 = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                bool isDead2 = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                bool isDead3 = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                bool isDead4 = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);

                if (isDead1)
                {
                    GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                    starterDead = true;
                    StarterDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    //
                    playerTurnOrder.Remove(CharacterIdentifier.Starter);
                    Debug.Log("Removing Starter");

                    //
                    StarterAnim.SetBool("isDead", true);
                }
                if (isDead2)
                {
                    GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                    middleDead = true;
                    MiddleDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();

                    playerTurnOrder.Remove(CharacterIdentifier.Middle);
                    Debug.Log("Removing Middle");


                    MidRelAnim.SetBool("isDead", true);

                }
                if (isDead3)
                {
                    GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                    setupDead = true;
                    SetUpDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();

                    playerTurnOrder.Remove(CharacterIdentifier.SetUp);
                    Debug.Log("Removing SetUp");


                    SetUpAnim.SetBool("isDead", true);
                }
                if (isDead4)
                {
                    GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                    closerDead = true;
                    CloserDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();

                    playerTurnOrder.Remove(CharacterIdentifier.Closer);
                    Debug.Log("Removing Closer");


                    CloserAnim.SetBool("isDead", true);

                }
                DebugPrintList(playerTurnOrder);
                if (!isDead1)
                {
                    yield return new WaitForSeconds(.5f);
                    StarterAnim.Play("Armature|Oof");

                    GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    StarterDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                }

                if (!isDead2)
                {
                    yield return new WaitForSeconds(.5f);
                    MidRelAnim.Play("Armature|Oof");

                    GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    MiddleDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                }

                if (!isDead3)
                {
                    yield return new WaitForSeconds(.5f);
                    SetUpAnim.Play("Armature|Oof");

                    GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    SetUpDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                }

                if (!isDead4)
                {
                    yield return new WaitForSeconds(.5f);
                    CloserAnim.Play("Armature|Oof");

                    GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                    CloserDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                }
                Unit.attackAll = false;
                yield return new WaitForSeconds(2f);
                StartCoroutine(TurnOffDamageUI());
               // NextPlayerTurnAfterEnemyTurn(enemyIndex);
            }

            else if (Unit.energyAll)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " tires the Pitchers with " + enemyUnit[enemyUnitSelected].attackName + "!";

                enemyAnim[enemyIndex].Play("Armature|Swing");

                yield return new WaitForSeconds(1f);

                bool isDead1 = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                bool isDead2 = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                bool isDead3 = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                bool isDead4 = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);

                if (!isDead1)
                {
                    yield return new WaitForSeconds(.5f);
                    StarterAnim.Play("Armature|Oof");

                    GameManager.StarterEnergy -= enemyUnit[enemyUnitSelected].enemyDamage;
                    StarterDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    StarterEnergy.value = (GameManager.StarterEnergy / GameManager.StarterEnergyMax);
                }

                if (!isDead2)
                {
                    yield return new WaitForSeconds(.5f);
                    MidRelAnim.Play("Armature|Oof");

                    GameManager.MidRelivEnergy -= enemyUnit[enemyUnitSelected].enemyDamage;
                    MiddleDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    MiddleEnergy.value = (GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax);
                }

                if (!isDead3)
                {
                    yield return new WaitForSeconds(.5f);
                    SetUpAnim.Play("Armature|Oof");

                    GameManager.SetUpEnergy -= enemyUnit[enemyUnitSelected].enemyDamage;
                    SetUpDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    SetUpEnergy.value = (GameManager.SetUpEnergy / GameManager.SetUpEnergyMax);
                }

                if (!isDead4)
                {
                    yield return new WaitForSeconds(.5f);
                    CloserAnim.Play("Armature|Oof");

                    GameManager.CloserEnergy -= enemyUnit[enemyUnitSelected].enemyDamage;
                    CloserDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                    CloserEnergy.value = (GameManager.CloserEnergy / GameManager.CloserEnergyMax);
                }
                Unit.energyAll = false;
                yield return new WaitForSeconds(1f);
                StartCoroutine(TurnOffDamageUI());
                //NextPlayerTurnAfterEnemyTurn(enemyIndex);
            }


            else
            {

                int RandomAttack = Random.Range(0, 100);

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

                if (Announcer)
                {
                    yield return new WaitForSeconds(1.75f);
                }
                else
                {
                    yield return new WaitForSeconds(.5f);
                }


                if (WhoToAttack == 0 && !starterDead)
                {
                    enemyUnit[enemyIndex].transform.LookAt(Starter.transform.position);
                    Instantiate(movingBall, enemyUnit[enemyIndex].transform.position, enemyUnit[enemyIndex].transform.rotation);

                    Camera.transform.LookAt(Starter.transform.position);

                    if (GameManager.StarterAgil >= RandomAttack)
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter with " + enemyUnit[enemyUnitSelected].attackName + "!";
                        yield return new WaitForSeconds(.5f);
                        dialogueText.text = "Starter Dodges!";
                        yield return new WaitForSeconds(1f);

                    }

                    else 
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter with " + enemyUnit[enemyUnitSelected].attackName + "!";

                        yield return new WaitForSeconds(2f);

                        bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);

                        print(isDead + " Starter");

                        if (isDead)
                        {
                            GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                            starterDead = true;

                            playerTurnOrder.Remove(CharacterIdentifier.Starter);

                            Debug.Log("Removing Starter");
                            DebugPrintList(playerTurnOrder);

                            StarterAnim.SetBool("isDead", true);
                            yield return new WaitForSeconds(3f);

                        }

                        else
                        {
                            yield return new WaitForSeconds(.5f);
                            StarterAnim.Play("Armature|Oof");
                            StarterDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                            GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                            yield return new WaitForSeconds(2f);

                        }
                    }
                    StartCoroutine(TurnOffDamageUI());
                }
                else if (WhoToAttack == 1 && !middleDead)
                {
                    enemyUnit[enemyIndex].transform.LookAt(MiddleReliever.transform.position);
                    Instantiate(movingBall, enemyUnit[enemyIndex].transform.position, enemyUnit[enemyIndex].transform.rotation);

                    Camera.transform.LookAt(MiddleReliever.transform.position);

                    if (GameManager.MiddleAgil >= RandomAttack)
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever with " + enemyUnit[enemyUnitSelected].attackName + "!";
                        yield return new WaitForSeconds(.5f);
                        dialogueText.text = "Mid Reliever Dodges!";
                        yield return new WaitForSeconds(1f);

                    }
                    else
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever with " + enemyUnit[enemyUnitSelected].attackName + "!";

                        yield return new WaitForSeconds(1f);
                        bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                        print(isDead + " Middle");
                        if (isDead)
                        {
                            GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                            middleDead = true;

                            playerTurnOrder.Remove(CharacterIdentifier.Middle);

                            Debug.Log("Removing Middle");
                            DebugPrintList(playerTurnOrder);

                            MidRelAnim.SetBool("isDead", true);
                            yield return new WaitForSeconds(3f);

                        }

                        else
                        {
                            yield return new WaitForSeconds(.5f);
                            MidRelAnim.Play("Armature|Oof");
                            MiddleDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                            GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                            yield return new WaitForSeconds(2f);

                        }
                    }

                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(TurnOffDamageUI());
                }
                else if (WhoToAttack == 2 && !setupDead)
                {
                    enemyUnit[enemyIndex].transform.LookAt(SetUp.transform.position);
                    Instantiate(movingBall, enemyUnit[enemyIndex].transform.position, enemyUnit[enemyIndex].transform.rotation);
                    Camera.transform.LookAt(SetUp.transform.position);

                    if (GameManager.SetUpAgil >= RandomAttack)
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks SetUp with " + enemyUnit[enemyUnitSelected].attackName + "!";
                        yield return new WaitForSeconds(.5f);
                        dialogueText.text = "SetUp Dodges!";
                        yield return new WaitForSeconds(1f);

                    }
                    else
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up with " + enemyUnit[enemyUnitSelected].attackName + "!";

                        yield return new WaitForSeconds(1f);
                        bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                        print(isDead + " SetUp");
                        if (isDead)
                        {
                            GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                            SetUpAnim.SetBool("isDead", true);
                            setupDead = true;

                            playerTurnOrder.Remove(CharacterIdentifier.SetUp);

                            Debug.Log("Removing SetuP");
                            DebugPrintList(playerTurnOrder);

                            yield return new WaitForSeconds(3f);

                        }

                        else
                        {
                            SetUpAnim.Play("Armature|Oof");
                            SetUpDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                            GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                            yield return new WaitForSeconds(2f);

                        }
                    }
                    StartCoroutine(TurnOffDamageUI());
                }
                else if(WhoToAttack == 3 && !closerDead)
                {
                    enemyUnit[enemyIndex].transform.LookAt(Closer.transform.position);
                    Instantiate(movingBall, enemyUnit[enemyIndex].transform.position, enemyUnit[enemyIndex].transform.rotation);
                    Camera.transform.LookAt(Closer.transform.position);

                    if (GameManager.CloserAgil >= RandomAttack)
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer with " + enemyUnit[enemyUnitSelected].attackName + "!";
                        yield return new WaitForSeconds(.5f);
                        dialogueText.text = "Closer Dodges!";
                        yield return new WaitForSeconds(1f);

                    }

                    else
                    {
                        dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer with " + enemyUnit[enemyUnitSelected].attackName + "!";

                        yield return new WaitForSeconds(1f);
                        bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                        print(isDead + " Closer");
                        if (isDead)
                        {
                            GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                            CloserAnim.SetBool("isDead", true);
                            closerDead = true;

                            playerTurnOrder.Remove(CharacterIdentifier.Closer);

                            Debug.Log("Removing Closer");
                            DebugPrintList(playerTurnOrder);

                            yield return new WaitForSeconds(3f);

                        }

                        else
                        {
                            CloserAnim.Play("Armature|Oof");
                            CloserDamageUI.text = "-" + enemyUnit[enemyUnitSelected].enemyDamage.ToString();
                            GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                            CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                            yield return new WaitForSeconds(2f);

                        }
                    }
                    StartCoroutine(TurnOffDamageUI());
                }
            }
        }
        NextTurn();
    }

    IEnumerator TurnOffDamageUI()
    {
        yield return new WaitForSeconds(1f);
        StarterDamageUI.text = "".ToString();
        MiddleDamageUI.text = "".ToString();
        SetUpDamageUI.text = "".ToString();
        CloserDamageUI.text = "".ToString();
    }

    #endregion

    #region Player Turns (button select)
    void StarterTurn()
    {
        Camera.transform.position = starterCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget);
      //  GameManager.Instance.DebugBall.transform.position = Starter.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (starterDead)
        {
            NextTurn();
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
    //    GameManager.Instance.DebugBall.transform.position = MiddleReliever.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (middleDead)
        {
            NextTurn();

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
     //   GameManager.Instance.DebugBall.transform.position = SetUp.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (SetUpAnim.GetBool("isDead"))
        {
            state = BattleStateMultiple.ENEMYTURN;
            StartCoroutine("EnemyTurn", 2);
        }

        if (setupDead)
        {
            NextTurn();

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
       // GameManager.Instance.DebugBall.transform.position = Closer.transform.position + Vector3.up * GameManager.Instance.DebugBallHeight;
        if (CloserAnim.GetBool("isDead"))
        {
            state = BattleStateMultiple.STARTER;
            StarterTurn();
        }
        if (closerDead)
        {
            NextTurn();
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
    #region Player Pitch Selection (opens up confirm menu)
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

    }

    public void OnCancelButton()
    {
        PlayerMenu.SetActive(true);
        PlayerPitches.SetActive(false);
    }
    #endregion

    #region End Battle Conditions
    void EndBattle()
    {
        if (isHoEBattle)
        {
            battleCam.transform.position = new Vector3(-14.45f, 3.71f, -14.82f);
        }

        Camera.transform.position = battleCam.transform.position;
        Camera.transform.LookAt(enemyCamTarget);
        if (state == BattleStateMultiple.WON)
        {
            if (Umpire)
            {
                HOEGameManager.UmpireDefeated = true;
            }

            if (Babe)
            {
                StartCoroutine(WaitingAtEndBabeBattle());
            }

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

            if (!isOver && !preventingAddXPDup)
            {
                AddXP();
                isOver = true;
            }

            isOver = true;

            victory.PlayEvent();
            battleMusic.controller.StopAll();
            battleMusic.controller.PlayRandom(battleMusic.OutMusic);
        }
        else if (state == BattleStateMultiple.LOST)
        {
            if (!gameOverToPreventDuplicates)
            {
                dialogueText.text = "You lost the battle...";
                GameManager.isGameOver = true;
                StartCoroutine(WaitingAtEndOfBattleForTraining());
                starterDead = false;
                middleDead = false;
                setupDead = false;
                closerDead = false;
                defeat.PlayEvent();
                battleMusic.controller.StopAll();
                battleMusic.controller.PlayRandom(battleMusic.OutMusic);
            }
        }
        gameOverToPreventDuplicates = true;
    }
    public void AddXP()
    {
        preventingAddXPDup = true;

        for (int i = 0; i < enemyUnit.Count; i++)
        {
            GameManager.Money += enemyUnit[i].MoneyToDistribute;
            MoneyText.text = GameManager.Money.ToString();
        }

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

        ChooseItem();

    }

    void ChooseItem()
    {
        int RandInt = Random.Range(0, 100);

        if (RandInt < 30)
        {
            InventoryItemPostBattle.text = "No Item Reward".ToString();
        }
        else if (RandInt < 50)
        {
            InventoryManage.GetComponent<InventoryManager>().StamUp20();
            InventoryItemPostBattle.text = "Sports Drink".ToString();
        }
        else if (RandInt < 65)
        {
            InventoryManage.GetComponent<InventoryManager>().EnUp10();
            InventoryItemPostBattle.text = "Granola Bar".ToString();
        }
        else if (RandInt < 75)
        {
            InventoryManage.GetComponent<InventoryManager>().EnUpAll10();
            InventoryItemPostBattle.text = "Sunflower Seeds".ToString();
        }
        else if (RandInt < 83)
        {
            InventoryManage.GetComponent<InventoryManager>().StamUpAll20();
            InventoryItemPostBattle.text = "Grandma's Cookies".ToString();
        }
        else if (RandInt < 90)
        {
            InventoryManage.GetComponent<InventoryManager>().EnemyHealthDown20();
            InventoryItemPostBattle.text = "Scouting Report".ToString();
        }
        else if (RandInt < 100)
        {
            InventoryManage.GetComponent<InventoryManager>().EnemyHealthDownAll20();
            InventoryItemPostBattle.text = "Defensive Shift".ToString();
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

                GameManager.StarterEnergyMax += 5;
                GameManager.StarterMoraleMax += 5;
                GameManager.StarterEnergy += 5;
                GameManager.StarterMorale += 5;

                SLevel = true;
                SLevelUp.SetActive(true);
                GameManager.StarterTargetExp *= 1.25f;
                //add training points
                StarterExpToNext.text = (GameManager.StarterTargetExp - GameManager.StarterExp).ToString("F0");
                int NewLevelS = GameManager.StarterLevel;
                int Difference = NewLevelS - OldLevelS;
                SPointsToGive = (Difference * 3);

                GameManager.StarterMorale = GameManager.StarterMoraleMax;
                GameManager.StarterEnergy = GameManager.StarterEnergyMax;
            }
            StarterExpToNext.text = (GameManager.StarterTargetExp - GameManager.StarterExp).ToString("F0");
            StartTotalExp.text = GameManager.StarterLevel.ToString("F0");
        }
        else
        {
            MidExp(totalExp / 4);
        }
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

                GameManager.MidRelievEnergyMax += 5;
                GameManager.MidRelivMoraleMax += 5;
                GameManager.MidRelivEnergy += 5;
                GameManager.MidRelivMorale += 5;

                MLevel = true;
                MLevelUp.SetActive(true);
                GameManager.MRTargetExp *= 1.5f;
                //add training points
                int NewLevelM = GameManager.MRLevel;
                int Difference = NewLevelM - OldLevelM;
                MPointsToGive = (Difference * 3) + 1;
            }
            MRExpToNext.text = (GameManager.MRTargetExp - GameManager.MRExp).ToString("F0");
            MRTotalExp.text = GameManager.MRLevel.ToString("F0");
        }
        else
        {
            SetUpExp(totalExp / 4);
        }
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

                GameManager.SetUpEnergyMax += 5;
                GameManager.SetUpMoraleMax += 5;
                GameManager.SetUpMorale += 5;
                GameManager.SetUpEnergy += 5;

                SeLevel = true;
                SetUpLevelUp.SetActive(true);
                GameManager.SetupTargetExp *= 1.75f;
                //add training points

                int NewLevelSe = GameManager.SetUpLevel;
                int Difference = NewLevelSe - OldLevelSe;
                SePointsToGive = (Difference * 3) + 1;
            }
            SetUpExpToNext.text = (GameManager.SetupTargetExp - GameManager.SetUpExp).ToString("F0");
            SetUpTotalExp.text = GameManager.SetUpLevel.ToString("F0");
        }
        else
        {
            CloserExp(totalExp / 4);
        }
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

                GameManager.CloserEnergyMax += 5;
                GameManager.CloserMoraleMax += 5;
                GameManager.CloserMorale += 5;
                GameManager.CloserEnergy += 5;

                CLevel = true;
                CloserLevelUp.SetActive(true);
                GameManager.CloserTargetExp *= 2f;
                //add training points
                int NewLevelC = GameManager.CloserLevel;
                int Difference = NewLevelC - OldLevelC;
                CPointsToGive = (Difference * 3) + 1;
            }
            CloserExpToNext.text = (GameManager.CloserTargetExp - GameManager.CloserExp).ToString("F0");
            CloserTotalExp.text = GameManager.CloserLevel.ToString("F0");
        }
        else
        {
            StartCoroutine(WaitingAtEndOfBattle());
        }
    }

    void CheatToInstantlyWin()
    {

        for (int i = 0; i < enemyUnit.Count; i++)
        {
            enemyUnit[i].TakeDamage(10000);
            Debug.Log("Cheat Activated");
        }
        state = BattleStateMultiple.WON;
        EndBattle();
        Debug.Log("Attempted To Cheat To Win");
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
        GameManager.MidRelivMorale = GameManager.MidRelivMoraleMax;
        GameManager.MidRelivEnergy = GameManager.MidRelievEnergyMax;
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
        GameManager.SetUpMorale = GameManager.SetUpMoraleMax;
        GameManager.SetUpEnergy = GameManager.SetUpEnergyMax;
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
        GameManager.CloserMorale = GameManager.CloserMoraleMax;
        GameManager.CloserEnergy = GameManager.CloserEnergyMax;
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
        GameManager.enemyAttackedPlayer = false;
        if (McGee)
        {
            ConcourseGameManager.McGeeKilled = true;
        }
        if (Announcer)
        {
            ConcourseGameManager.AnnouncerKilled = true;
        }
        PlayerMovement.hasAttackedEnemy = false;
        inBattle = false;
        yield return new WaitForSeconds(1.5f);
        state = BattleStateMultiple.START;
        //Return to Main Menu
        if (isConcourse)
        {
            SceneManager.LoadScene("Concourse");
        }
        if (isHoE)
        {
            SceneManager.LoadScene("ClubHouse");
        }
        if (state == BattleStateMultiple.WON)
        {
            if (Babe)
            {
                StartCoroutine(WaitingAtEndOfBattle());
            }
        }
        else if (state == BattleStateMultiple.STARTER || state == BattleStateMultiple.MIDDLE || state == BattleStateMultiple.SETUP || state == BattleStateMultiple.CLOSER)
        {
            SceneManager.LoadScene("ClubHouse");
        }
    }

    IEnumerator WaitingAtEndOfBattleForTraining()
    {
        PlayerMovement.hasAttackedEnemy = false;
        inBattle = false;
        yield return new WaitForSeconds(1.5f);
        state = BattleStateMultiple.START;
        //Return to Main Menu
        SceneManager.LoadScene("TrainingArea");
    }

    IEnumerator WaitingAtEndBabeBattle()
    {
        yield return new WaitForSeconds(1.5f);
        state = BattleStateMultiple.START;
        SceneManager.LoadScene("WinScene");
    }

    IEnumerator WaitingForCall()
    {
        yield return new WaitForSeconds(2f);
        dialogueText.text = "Please choose an action.";

    }
}
