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
    //End screen experience
    public Text StarterExpGain, MRExpGain, SetUpExpGain, CloserExpGain;
    public Text StartTotalExp, MRTotalExp, SetUpTotalExp, CloserTotalExp;

    public Text MoneyText;
    //Particle systems for selection
    public GameObject enemySelectionParticle;
    public GameObject playerSelectionParticle;
    //determining enemy selection
    public int enemyUnitSelected;
    public Transform[] enemyBattleStationLocations;
    public GameObject[] enemyPrefab;

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
    int playerCount;
    bool allPlayersDead;
    bool starterDown, middleDown, setupDown, closerDown;

    //BattleHUD enemyHUD;

    //
    bool enemySelect;
    private void Start()
    {
        state = BattleStateMultiple.START;

        foreach (GameObject player in playerStations)
        {
            playerCount++;
            print(playerCount);
        }

        StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
        MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
        SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
        CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);

        StarterEnergy.value = (GameManager.StarterEnergy / GameManager.StarterEnergyMax);
        MiddleEnergy.value = (GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax);
        SetUpEnergy.value = (GameManager.SetUpEnergy / GameManager.SetUpEnergyMax);
        CloserEnergy.value = (GameManager.CloserEnergy / GameManager.CloserEnergyMax);

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

        for (int i = 0; i < enemyBattleStationLocations.Length; i++)
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
                    enemyUnitSelected = enemyBattleStationLocations.Length-1;
                }

                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(enemyUnitSelected <= enemyBattleStationLocations.Length-1)
                    enemyUnitSelected++;
                if (enemyUnitSelected > enemyBattleStationLocations.Length-1)
                    enemyUnitSelected = 0;

                enemySelectionParticle.transform.position = enemyBattleStationLocations[enemyUnitSelected].transform.position;
            }
        }

        if (playerCount == 0)
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
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.fastballDamage);
                print(enemyUnit[enemyUnitSelected].currentHP);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Starter.changeupDamage);
                changeup = false;
            }
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleStateMultiple.WON;
                //Get items,transition out of battle
                EndBattle();
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
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.fastballDamage);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(MiddleReliever.changeupDamage);
                changeup = false;
            }
            //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleStateMultiple.WON;
                //Get items,transition out of battle
                EndBattle();
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
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.fastballDamage);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(SetUp.changeupDamage);
                changeup = false;
            }
           // enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleStateMultiple.WON;
                //Get items,transition out of battle
                EndBattle();
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
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.fastballDamage);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit[enemyUnitSelected].TakeDamage(Closer.changeupDamage);
                changeup = false;
            }
            //enemyHUD.SetHP(enemyUnit[enemyUnitSelected].currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleStateMultiple.WON;
                //Get items,transition out of battle
                EndBattle();
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
        //Need logic to determine what attack the enemy will do

        //attack animation
        yield return new WaitForSeconds(1.5f);

        //Choosing Who To Attack
        int WhoToAttack = Random.Range(0, 3);
        if (WhoToAttack == 0)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
            bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
            yield return new WaitForSeconds(3f);
            if (isDead)
            {
                starterDown = true;
                playerCount--;
                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }

            else
            {
                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
        }
        if (WhoToAttack == 1)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
            bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                middleDown = true;
                playerCount--;
                state = BattleStateMultiple.SETUP;
                SETUPTurn();
            }

            else
            {
                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
        }
        if (WhoToAttack == 2)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
            bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                setupDown = true;
                playerCount--;
                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }

            else
            {
                state = BattleStateMultiple.MIDDLE;
                MiddleTurn();
            }
        }
        if (WhoToAttack == 3)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
            bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                closerDown = true;
                playerCount--;
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

    IEnumerator EnemyTurn2()
    {
        //Need logic to determine what attack the enemy will do

        //attack animation
        yield return new WaitForSeconds(1.5f);

        //Choosing Who To Attack
        int WhoToAttack = Random.Range(0, 3);
        if (WhoToAttack == 0)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
            bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
            yield return new WaitForSeconds(3f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.SETUP;
                SETUPTurn();
            }
        }
        if (WhoToAttack == 1)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
            bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.SETUP;
                SETUPTurn();
            }
        }
        if (WhoToAttack == 2)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
            bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.SETUP;
                SETUPTurn();
            }
        }
        if (WhoToAttack == 3)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
            bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.SETUP;
                SETUPTurn();
            }
        }

    }

    IEnumerator EnemyTurn3()
    {
        //Need logic to determine what attack the enemy will do

        //attack animation
        yield return new WaitForSeconds(1.5f);

        //Choosing Who To Attack
        int WhoToAttack = Random.Range(0, 3);
        if (WhoToAttack == 0)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Starter!";
            bool isDead = Starter.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.StarterMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
            yield return new WaitForSeconds(3f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.CLOSER;
                CloserTurn();
            }
        }
        if (WhoToAttack == 1)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Mid Reliever!";
            bool isDead = MiddleReliever.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.MidRelivMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.CLOSER;
                CloserTurn();
            }
        }
        if (WhoToAttack == 2)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Set Up!";
            bool isDead = SetUp.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.SetUpMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.CLOSER;
                CloserTurn();
            }
        }
        if (WhoToAttack == 3)
        {
            dialogueText.text = enemyUnit[enemyUnitSelected].unitName + " attacks Closer!";
            bool isDead = Closer.TakeDamage(enemyUnit[enemyUnitSelected].enemyDamage);
            GameManager.CloserMorale -= enemyUnit[enemyUnitSelected].enemyDamage;
            CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleStateMultiple.LOST;
                EndBattle();
            }

            else
            {
                state = BattleStateMultiple.CLOSER;
                CloserTurn();
            }
        }

    }
    #endregion

    #region Player Turns
    void StarterTurn()
    {
        PlayerMenu.SetActive(true);
        dialogueText.text = "Starter: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;

    }

    void MiddleTurn()
    {
        PlayerMenu.SetActive(true);
        dialogueText.text = "Middle Reliever: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;
    }

    void SETUPTurn()
    {
        PlayerMenu.SetActive(true);
        dialogueText.text = "Set Up: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;
    }

    void CloserTurn()
    {
        PlayerMenu.SetActive(true);
        dialogueText.text = "Closer: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;
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
        if (state != BattleStateMultiple.STARTER)
            return;
        if (state != BattleStateMultiple.SETUP)
            return;
        if (state != BattleStateMultiple.MIDDLE)
            return;
        if (state != BattleStateMultiple.CLOSER)
            return;

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

            StarterExpGain.text = "   +" + (enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4).ToString("F0");
            MRExpGain.text = "   +" + (enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4).ToString("F0");
            SetUpExpGain.text = "   +" + (enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4).ToString("F0");
            CloserExpGain.text = "   +" + (enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4).ToString("F0");

            GameManager.StarterExp = enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4 + GameManager.StarterExp;
            GameManager.MRExp = enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4 + GameManager.MRExp;
            GameManager.SetUpExp = enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4 + GameManager.SetUpExp;
            GameManager.CloserExp = enemyUnit[enemyUnitSelected].ExperienceToDistribute / 4 + GameManager.CloserExp;

            StartTotalExp.text = GameManager.StarterExp.ToString("F0");
            MRTotalExp.text = GameManager.MRExp.ToString("F0");
            SetUpTotalExp.text = GameManager.SetUpExp.ToString("F0");
            CloserTotalExp.text = GameManager.CloserExp.ToString("F0");

            GameManager.Money += enemyUnit[enemyUnitSelected].MoneyToDistribute;
            MoneyText.text = "$ " + enemyUnit[enemyUnitSelected].MoneyToDistribute.ToString("F0");
        }
        else if (state == BattleStateMultiple.LOST)
        {
            dialogueText.text = "You lost the battle...";
        }
    }
    public void BattleFinished()
    {
        SceneManager.LoadScene("Concourse");
    }
    #endregion
}
