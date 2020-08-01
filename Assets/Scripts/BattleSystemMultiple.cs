using System.Collections;
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

    public Unit[] enemyUnit;
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
    private void Start()
    {
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

        for (int i = 0; i < enemyBattleStationLocations.Count; i++)
        {
            enemyCount++;
            enemyPrefab[i].SetActive(true);
        }

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO1 = Instantiate(StarterPrefab, StarterStation);
        GameObject playerGO2 = Instantiate(MiddleRelievPrefab, MiddleStationStation);
        GameObject playerGO3 = Instantiate(SetUpPrefab, SetUpStation);
        GameObject playerGO4 = Instantiate(CloserPrefab, CloserStation);

        Starter = playerGO1.GetComponent<Unit>();
        MiddleReliever = playerGO2.GetComponent<Unit>();
        SetUp = playerGO3.GetComponent<Unit>();
        Closer = playerGO4.GetComponent<Unit>();

        for (int i = 0; i < enemyBattleStationLocations.Count; i++)
        {
            // Instantiate(enemyPrefab[i], enemyBattleStationLocations[i].position, Quaternion.identity);
            // GameObject enemyGO = enemyPrefab[i];
            GameObject enemyGO = Instantiate(enemyPrefab[i], enemyBattleStationLocations[i]);
            enemyUnit[i] = enemyGO.GetComponent<Unit>();
        }

        yield return new WaitForSeconds(2f);

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
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(enemyUnitSelected <= enemyBattleStationLocations.Count-1)
                    enemyUnitSelected++;
                if (enemyUnitSelected > enemyBattleStationLocations.Count-1)
                    enemyUnitSelected = 0;

                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            }
        }

        print(enemyCount);
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
                    GameManager.StarterEnergy -= Starter.fastballStamina;
                    //UpdateStarterUI;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                }
            }
            if (slider)
            {
                if (Starter.sliderStamina <= GameManager.StarterEnergy)
                {
                    GameManager.StarterEnergy -= Starter.sliderStamina;
                    //UpdateStarterUI;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                }
            }
            if (curveball)
            {
                if (Starter.curveballStamina <= GameManager.StarterEnergy)
                {
                    GameManager.StarterEnergy -= Starter.curveballStamina;
                    //UpdateStarterUI;
                    StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                }
            }
            if (changeup)
            {
                if (Starter.changeupStamina <= GameManager.StarterEnergy)
                {
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
                    GameManager.MidRelivEnergy -= MiddleReliever.fastballStamina;
                    //UpdateStarterUI;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                }
            }
            if (slider)
            {
                if (MiddleReliever.sliderStamina <= GameManager.StarterEnergy)
                {
                    GameManager.MidRelivEnergy -= MiddleReliever.sliderStamina;
                    //UpdateStarterUI;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                }
            }
            if (curveball)
            {
                if (MiddleReliever.curveballStamina <= GameManager.StarterEnergy)
                {
                    GameManager.MidRelivEnergy -= MiddleReliever.curveballStamina;
                    //UpdateStarterUI;
                    MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                }
            }
            if (changeup)
            {
                if (MiddleReliever.changeupStamina <= GameManager.StarterEnergy)
                {
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
                    GameManager.SetUpEnergy -= SetUp.fastballStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                }
            }
            if (slider)
            {
                if (SetUp.sliderStamina <= GameManager.SetUpEnergy)
                {
                    GameManager.SetUpEnergy -= SetUp.sliderStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                }
            }
            if (curveball)
            {
                if (SetUp.curveballStamina <= GameManager.SetUpEnergy)
                {
                    GameManager.SetUpEnergy -= SetUp.curveballStamina;
                    //UpdateStarterUI;
                    SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                }
            }
            if (changeup)
            {
                if (SetUp.changeupStamina <= GameManager.SetUpEnergy)
                {
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
                    GameManager.CloserEnergy -= Closer.fastballStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                }
            }
            if (slider)
            {
                if (Closer.sliderStamina <= GameManager.CloserEnergy)
                {
                    GameManager.CloserEnergy -= Closer.sliderStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                }
            }
            if (curveball)
            {
                if (Closer.curveballStamina <= GameManager.CloserEnergy)
                {
                    GameManager.CloserEnergy -= Closer.curveballStamina;
                    //UpdateStarterUI;
                    CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                }
            }
            if (changeup)
            {
                if (Closer.changeupStamina <= GameManager.CloserEnergy)
                {
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

        if (state == BattleStateMultiple.STARTER)
        {
            if (fastball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.fastballDamage + GameManager.StarterFast);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.sliderDamage + GameManager.StarterSlid);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.curveballDamage + GameManager.StarterCurve);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.changeupDamage + GameManager.StarterChange);
                changeup = false;
            }
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //Destroy(enemyPrefab[enemyUnitSelected]);
                totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                enemyUnitSelected = 0;
                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine(EnemyTurn1());
            }

            if (!isDead)
            {
                //Middle Reliever turn
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine(EnemyTurn1());
            }
        }
    }

    IEnumerator MiddleAttack()
    {

        if (state == BattleStateMultiple.MIDDLE)
        {
            if (fastball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.fastballDamage + GameManager.MiddleFast);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.sliderDamage + GameManager.MiddleSlid);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.curveballDamage + GameManager.MiddleCurve);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.changeupDamage + GameManager.MiddleChange);
                changeup = false;
            }
            //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                enemyUnitSelected = 0;
                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine(EnemyTurn2());
            }

            if (!isDead)
            {
                //Enemy Attack turn
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine(EnemyTurn2());
            }
        }
    }

    IEnumerator SetUpAttack()
    {

        if (state == BattleStateMultiple.SETUP)
        {
            //To Do Start Attack Animation
            if (fastball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.fastballDamage + GameManager.SetUpFast);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.sliderDamage + GameManager.SetUpSlid);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.curveballDamage + GameManager.SetUpCurve);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.changeupDamage + GameManager.SetUpChange);
                changeup = false;
            }
           // enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                enemyUnitSelected = 0;
                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;

                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine(EnemyTurn3());
            }

            if (!isDead)
            {
                //Closer turn
                state = BattleStateMultiple.ENEMYTURN;
                StartCoroutine(EnemyTurn3());
            }
        }

    }

    IEnumerator CloserAttack()
    {
        if (state == BattleStateMultiple.CLOSER)
        {
            if (fastball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.fastballDamage + GameManager.CloserFast);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.sliderDamage + GameManager.CloserSlid);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.curveballDamage + GameManager.CloserCurve);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.changeupDamage + GameManager.CloserChange);
                changeup = false;
            }
            //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                totalExp += enemyUnit[enemyUnitSelected].ExperienceToDistribute;
                enemyCount--;
                enemyBattleStationLocations.Remove(enemyBattleStationLocations[enemyUnitSelected]);
                enemyUnitSelected = 0;
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
    #endregion

    #region Enemy Attack
    IEnumerator EnemyTurn1()
    {
        if (enemyUnit[enemyUnitSelected].currentHP <= 0)
        {
            //Skipping Turn to go to pitcher
            MiddleTurn();
        }
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

        if (WhoToAttack == 0 && !starterDead)
        {

            if (GameManager.StarterAgil >= RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                yield return new WaitForSeconds(.5f);
                dialogueText.text = "Starter Dodges!";

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }

            if (GameManager.StarterAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                yield return new WaitForSeconds(3f);
                if (isDead)
                {
                    starterDead = true;
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.MiddleAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    middleDead = true;
                    state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }

                else
                {
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
            }
        }
        if (WhoToAttack == 2 && !setupDead)
        {

            if (GameManager.SetUpAgil >= RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks SetUp!";
                yield return new WaitForSeconds(.5f);
                dialogueText.text = "SetUp Dodges!";

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.SetUpAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
                bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    setupDead = true;
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }

            if (GameManager.CloserAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
                bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    closerDead = true;
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }

                else
                {
                    state = BattleStateMultiple.MIDDLE;
                    MiddleTurn();
                }
            }
        }

    }

    IEnumerator EnemyTurn2()
    {
        if (enemyUnit[enemyUnitSelected].currentHP <= 0)
        {
            //Skipping Turn to go to pitcher
            SETUPTurn();
        }
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.StarterAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                yield return new WaitForSeconds(3f);
                if (isDead)
                {
                    starterDead = true;
                    state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.MiddleAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    middleDead = true;
                    state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.SetUpAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
                bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    setupDead = true;
                    state = BattleStateMultiple.CLOSER;
                    CloserTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.CloserAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
                bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    closerDead = true;
                    state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }

                else
                {
                    state = BattleStateMultiple.SETUP;
                    SETUPTurn();
                }
            }
        }

    }

    IEnumerator EnemyTurn3()
    {
        if (enemyUnit[enemyUnitSelected].currentHP <= 0)
        {
            //Skipping Turn to go to pitcher
            CloserTurn();
        }
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.StarterAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
                bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
                yield return new WaitForSeconds(3f);
                if (isDead)
                {
                    starterDead = true;
                    state = BattleStateMultiple.CLOSER;
                    CloserTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.MiddleAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
                bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    middleDead = true;
                    state = BattleStateMultiple.CLOSER;
                    CloserTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.SetUpAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
                bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    middleDead = true;
                    state = BattleStateMultiple.CLOSER;
                    CloserTurn();
                }

                else
                {
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

                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
            if (GameManager.CloserAgil < RandomAttack)
            {
                dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
                bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
                GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
                CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
                yield return new WaitForSeconds(1f);
                if (isDead)
                {
                    closerDead = true;
                    state = BattleStateMultiple.STARTER;
                    StarterTurn();
                }

                else
                {
                    state = BattleStateMultiple.CLOSER;
                    CloserTurn();
                }
            }
        }

    }
    #endregion

    #region Player Turns
    void StarterTurn()
    {
        if (starterDead)
        {
            StartCoroutine(EnemyTurn1());
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
        if (middleDead)
        {
            StartCoroutine(EnemyTurn2());
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
        if (setupDead)
        {
            StartCoroutine(EnemyTurn3());
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
        if (closerDead)
        {
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

    /* IEnumerator PlayerHeal()
     {
         playerUnit.Heal(5);
         playerHUD.SetHP(playerUnit.currentHP);
         dialogueText.text = "You feel recharged!";

         yield return new WaitForSeconds(2f);

         state = BattleStateMultiple.ENEMYTURN;
         StartCoroutine(EnemyTurn());
     }*/
    public void OnPitchesButton()
    {
        // if (state != BattleStateMultiple.PLAYERTURN)
        //     return;

        PlayerPitches.SetActive(true);
        PlayerMenu.SetActive(false);
    }
    #region Player Pitches
    /// <summary>
    /// 
    /// </summary>
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
                print("Not enough energy!");
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
        if (state == BattleStateMultiple.WON)
        {
            dialogueText.text = "You won the Battle!";
            EndingMenu.SetActive(true);
            PlayerPitches.SetActive(false);
            PlayerMenu.SetActive(false);

            StarterExpGain.text = "   +" + (totalExp / 4).ToString("F0");
            MRExpGain.text = "   +" + (totalExp / 4).ToString("F0");
            SetUpExpGain.text = "   +" + (totalExp / 4).ToString("F0");
            CloserExpGain.text = "   +" + (totalExp / 4).ToString("F0");

            GameManager.StarterExp = totalExp / 4 + GameManager.StarterExp;
            GameManager.MRExp = totalExp / 4 + GameManager.MRExp;
            GameManager.SetUpExp = totalExp / 4 + GameManager.SetUpExp;
            GameManager.CloserExp = totalExp / 4 + GameManager.CloserExp;

            StartTotalExp.text = GameManager.StarterLevel.ToString("F0");
            MRTotalExp.text = GameManager.MRLevel.ToString("F0");
            SetUpTotalExp.text = GameManager.SetUpLevel.ToString("F0");
            CloserTotalExp.text = GameManager.CloserLevel.ToString("F0");

            AddXP();

            GameManager.Money += enemyUnit[enemyUnitSelected].MoneyToDistribute;
            MoneyText.text = "$ " + enemyUnit[enemyUnitSelected].MoneyToDistribute.ToString("F0");
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
        StarterExp(totalExp/4);
        MidExp(totalExp / 4);
        SetUpExp(totalExp / 4);
        CloserExp(totalExp / 4); 
    }

    void StarterExp(int xp)
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
            StarterExpToNext.text = GameManager.StarterTargetExp.ToString("F0");
            int NewLevelS = GameManager.StarterLevel;
            int Difference = OldLevelS - NewLevelS;
            SPointsToGive = Difference * 3;
        }
    }

    void MidExp(int xp)
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
            MRExpToNext.text = GameManager.MRTargetExp.ToString("F0");
            int NewLevelM = GameManager.MRLevel;
            int Difference = OldLevelM - NewLevelM;
            MPointsToGive = Difference * 3;
        }
    }

    void SetUpExp(int xp)
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
            SetUpExpToNext.text = GameManager.SetupTargetExp.ToString("F0");
            int NewLevelSe = GameManager.SetUpLevel;
            int Difference = OldLevelSe - NewLevelSe;
            SePointsToGive = Difference * 3;
        }

    }

    void CloserExp(int xp)
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
            CloserExpToNext.text = GameManager.CloserTargetExp.ToString("F0");
            int NewLevelC = GameManager.CloserLevel;
            int Difference = OldLevelC - NewLevelC;
            CPointsToGive = Difference * 3;
        }
    }

    public void BattleFinished()
    {
        if (isOver)
        {
            print(SLevel + "   " + MLevel + "   " + SeLevel + "   " + CLevel);

            PlayerStatsScreen.SetActive(true);
            print("why am i here");
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
            MPoints.text = MPointsToGive.ToString();
            SePoints.text = SePointsToGive.ToString();
            CPoints.text = CPointsToGive.ToString();
            if (SLevel)
            {
                SLevelUpScreen.SetActive(true);
                StarterLevelUp();
            }
            if (!SLevel && MLevel)
            {
                MLevelUpScreen.SetActive(true);
                MidRelieverLevelUp();
            }
            if (!SLevel && !MLevel && SeLevel)
            {
                SeLevelUpScreen.SetActive(true);
                SetLevelUp();
            }
            if (!SLevel && !MLevel && !SeLevel && CLevel)
            {
                CLevelUpScreen.SetActive(true);
                CloseLevelUp();
            }
            EndingMenu.SetActive(false);
            /*  if (!SLevel && !MLevel && !SeLevel && !CLevel)
              {
                  StartCoroutine(WaitingAtEndOfBattle());
              }*/
        }
    }

    public void StarterLevelUp()
    {
        if (MLevel)
        {
            MLevelUpScreen.SetActive(true);
            SLevelUpScreen.SetActive(false);
            MidRelieverLevelUp();
        }
        if (!MLevel && SeLevel)
        {
            SeLevelUpScreen.SetActive(true);
            MLevelUpScreen.SetActive(false);
            SetLevelUp();
        }
        if (!SeLevel && CLevel)
        {
            CLevelUpScreen.SetActive(true);
            SeLevelUpScreen.SetActive(false);
            CloseLevelUp();
        }
        if (!MLevel && !SeLevel && !CLevel)
        {
           // StartCoroutine(WaitingAtEndOfBattle());
        }
    }

    public void MidRelieverLevelUp()
    {
        if (SeLevel)
        {
            SeLevelUpScreen.SetActive(true);
            SetLevelUp();
        }
        if (!SeLevel && CLevel)
        {
            CLevelUpScreen.SetActive(true);
            CloseLevelUp();
        }
        if (!SeLevel && !CLevel)
        {
           // StartCoroutine(WaitingAtEndOfBattle());
        }
    }

    public void SetLevelUp()
    {
        if (CLevel)
        {
            CLevelUpScreen.SetActive(true);
            CloseLevelUp();
        }
        if (!CLevel)
        {
           // StartCoroutine(WaitingAtEndOfBattle());
        }
    }

    public void CloseLevelUp()
    {
       // StartCoroutine(WaitingAtEndOfBattle());
    }

    #endregion

    IEnumerator WaitingAtEndOfBattle()
    {
        yield return new WaitForSeconds(1f);
         SceneManager.LoadScene("Concourse");
    }
}
