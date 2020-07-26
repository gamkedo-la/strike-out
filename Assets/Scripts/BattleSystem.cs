using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This is the definition of the Game States
public enum BattleState {START, STARTER, MIDDLE, SETUP, CLOSER, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

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
    public GameObject EndingMenu;

    Unit Starter;
    Unit MiddleReliever;
    Unit SetUp;
    Unit Closer;

    public Slider StarterMorale, StarterEnergy;
    public Slider MiddleMorale, MiddleEnergy;
    public Slider SetUpMorale, SetUpEnergy;
    public Slider CloserMorale, CloserEnergy;

    public Text StarterExpGain, MRExpGain, SetUpExpGain, CloserExpGain;
    public Text StartTotalExp, MRTotalExp, SetUpTotalExp, CloserTotalExp;

    public Text MoneyText;

    public GameObject enemySelectionParticle;
    public GameObject playerSelectionParticle;

    public Transform[] enemyBattleStationLocations;
    public GameObject[] enemyPrefab;

    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD enemyHUD;

    //PlayerPitchChoice
    bool fastball;
    bool curveball;
    bool slider;
    bool changeup;

    //Enemy
    bool isDead;
    private void Start()
    {
        state = BattleState.START;

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

        Starter =  playerGO1.GetComponent<Unit>();
        MiddleReliever = playerGO2.GetComponent<Unit>();
        SetUp = playerGO3.GetComponent<Unit>();
        Closer = playerGO4.GetComponent<Unit>();

        for (int i = 0; i < enemyBattleStationLocations.Length; i++)
        {
           // Instantiate(enemyPrefab[i], enemyBattleStationLocations[i].position, Quaternion.identity);
           // GameObject enemyGO = enemyPrefab[i];
            GameObject enemyGO = Instantiate(enemyPrefab[i], enemyBattleStationLocations[i]);
            enemyUnit = enemyGO.GetComponent<Unit>();
        }

        dialogueText.text = "Confronted by: " + enemyUnit.unitName;

        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.STARTER;
        StarterTurn();
    }
    IEnumerator PlayerAttack()
    {
        //To Do Damage Enemy
        if (state == BattleState.STARTER)
        {
            if (fastball)
            {
                isDead = enemyUnit.TakeDamage(Starter.fastballDamage);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit.TakeDamage(Starter.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit.TakeDamage(Starter.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit.TakeDamage(Starter.changeupDamage);
                changeup = false;
            }
           // enemyHUD.SetHP(enemyUnit[].currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleState.WON;
                //Get items,transition out of battle
                EndBattle();
            }

            if(!isDead)
            {
                //Middle Reliever turn
                state = BattleState.MIDDLE;
                MiddleTurn();
            }
        }
    }

    IEnumerator MiddleAttack()
    {

        if (state == BattleState.MIDDLE)
        {
            if (fastball)
            {
                isDead = enemyUnit.TakeDamage(MiddleReliever.fastballDamage);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit.TakeDamage(MiddleReliever.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit.TakeDamage(MiddleReliever.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit.TakeDamage(MiddleReliever.changeupDamage);
                changeup = false;
            }
          //  enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleState.WON;
                //Get items,transition out of battle
                EndBattle();
            }

            if (!isDead)
            {
                //Enemy Attack turn
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator SetUpAttack()
    {

        if (state == BattleState.SETUP)
        {
            if (fastball)
            {
                isDead = enemyUnit.TakeDamage(SetUp.fastballDamage);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit.TakeDamage(SetUp.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit.TakeDamage(SetUp.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit.TakeDamage(SetUp.changeupDamage);
                changeup = false;
            }
           // enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleState.WON;
                //Get items,transition out of battle
                EndBattle();
            }

            if (!isDead)
            {
                //Closer turn
                state = BattleState.CLOSER;
                CloserTurn();
            }
        }

    }

    IEnumerator CloserAttack()
    {
        if (state == BattleState.CLOSER)
        {
            if (fastball)
            {
                isDead = enemyUnit.TakeDamage(Closer.fastballDamage);
                fastball = false;
            }
            if (slider)
            {
                isDead = enemyUnit.TakeDamage(Closer.sliderDamage);
                slider = false;
            }
            if (curveball)
            {
                isDead = enemyUnit.TakeDamage(Closer.curveballDamage);
                curveball = false;
            }
            if (changeup)
            {
                isDead = enemyUnit.TakeDamage(Closer.changeupDamage);
                changeup = false;
            }
          //  enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "The attack is successful!";
            yield return new WaitForSeconds(2f);

            //This checks to see if the Enemy is Dead or has HP remaining
            if (isDead)
            {
                //End Battle
                state = BattleState.WON;
                //Get items,transition out of battle
                EndBattle();
            }

            if (!isDead)
            {
                //Middle Reliever turn
                state = BattleState.STARTER;
                StarterTurn();
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        //Need logic to determine what attack the enemy will do

        //attack animation
        yield return new WaitForSeconds(3f);

        //Choosing Who To Attack
        int WhoToAttack = Random.Range(0, 3);
        if (WhoToAttack == 0)
        {
            dialogueText.text = enemyUnit.unitName + " attacks Starter!";
            bool isDead = Starter.TakeDamage(enemyUnit.enemyDamage);
            GameManager.StarterMorale -= enemyUnit.enemyDamage;
            StarterMorale.value = (GameManager.StarterMorale / GameManager.StarterMoraleMax);
            yield return new WaitForSeconds(3f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleState.LOST;
                EndBattle();
            }

            else
            {
                state = BattleState.SETUP;
                SETUPTurn();
            }
        }
        if (WhoToAttack == 1)
        {
            dialogueText.text = enemyUnit.unitName + " attacks Mid Reliever!";
            bool isDead = MiddleReliever.TakeDamage(enemyUnit.enemyDamage);
            GameManager.MidRelivMorale -= enemyUnit.enemyDamage;
            MiddleMorale.value = (GameManager.MidRelivMorale / GameManager.MidRelivMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleState.LOST;
                EndBattle();
            }

            else
            {
                state = BattleState.SETUP;
                SETUPTurn();
            }
        }
        if (WhoToAttack == 2)
        {
            dialogueText.text = enemyUnit.unitName + " attacks Set Up!";
            bool isDead = SetUp.TakeDamage(enemyUnit.enemyDamage);
            GameManager.SetUpMorale -= enemyUnit.enemyDamage;
            SetUpMorale.value = (GameManager.SetUpMorale / GameManager.SetUpMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleState.LOST;
                EndBattle();
            }

            else
            {
                state = BattleState.SETUP;
                SETUPTurn();
            }
        }
        if (WhoToAttack == 3)
        {
            dialogueText.text = enemyUnit.unitName + " attacks Closer!";
            bool isDead = Closer.TakeDamage(enemyUnit.enemyDamage);
            GameManager.CloserMorale -= enemyUnit.enemyDamage;
            CloserMorale.value = (GameManager.CloserMorale / GameManager.CloserMoraleMax);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                //Come back to this, this is if everyone loses
                state = BattleState.LOST;
                EndBattle();
            }

            else
            {
                state = BattleState.SETUP;
                SETUPTurn();
            }
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the Battle!";
            EndingMenu.SetActive(true);
            PlayerPitches.SetActive(false);
            PlayerMenu.SetActive(false);

            StarterExpGain.text = "   +" + (enemyUnit.ExperienceToDistribute / 4).ToString("F0");
            MRExpGain.text = "   +" + (enemyUnit.ExperienceToDistribute / 4).ToString("F0");
            SetUpExpGain.text = "   +" + (enemyUnit.ExperienceToDistribute / 4).ToString("F0");
            CloserExpGain.text = "   +" + (enemyUnit.ExperienceToDistribute / 4).ToString("F0");

            GameManager.StarterExp = enemyUnit.ExperienceToDistribute / 4 + GameManager.StarterExp;
            GameManager.MRExp = enemyUnit.ExperienceToDistribute / 4 + GameManager.MRExp;
            GameManager.SetUpExp = enemyUnit.ExperienceToDistribute / 4 + GameManager.SetUpExp;
            GameManager.CloserExp = enemyUnit.ExperienceToDistribute / 4 + GameManager.CloserExp;

            StartTotalExp.text = GameManager.StarterExp.ToString("F0");
            MRTotalExp.text = GameManager.MRExp.ToString("F0");
            SetUpTotalExp.text = GameManager.SetUpExp.ToString("F0");
            CloserTotalExp.text = GameManager.CloserExp.ToString("F0");

            GameManager.Money += enemyUnit.MoneyToDistribute;
            MoneyText.text = "$ " + enemyUnit.MoneyToDistribute.ToString("F0");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost the battle...";
        }
    }

    void StarterTurn()
    {
        dialogueText.text = "Starter: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;
    }

    void MiddleTurn()
    {
        dialogueText.text = "Middle Reliever: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;
    }

    void SETUPTurn()
    {
        dialogueText.text = "Set Up: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;
    }

    void CloserTurn()
    {
        dialogueText.text = "Closer: Choose an Action.";

        fastball = false;
        slider = false;
        changeup = false;
        curveball = false;
    }

   /* IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel recharged!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }*/
    public void OnPitchesButton()
    {
       // if (state != BattleState.PLAYERTURN)
       //     return;

        PlayerPitches.SetActive(true);
        PlayerMenu.SetActive(false);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void OnFastballButton()
    {
        if (state == BattleState.STARTER)
        {
            if (Starter.fastballStamina <= GameManager.StarterEnergy)
            {
                GameManager.StarterEnergy -= Starter.fastballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);

                StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(PlayerAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.MIDDLE)
        {
            if (MiddleReliever.fastballStamina <= GameManager.MidRelivEnergy)
            {
                GameManager.MidRelivEnergy -= MiddleReliever.fastballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(MiddleAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.SETUP)
        {
            if (SetUp.fastballStamina <= GameManager.SetUpEnergy)
            {
                GameManager.SetUpEnergy -= SetUp.fastballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(SetUpAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.CLOSER)
        {
            if (Closer.fastballStamina <= GameManager.CloserEnergy)
            {
                GameManager.CloserEnergy -= Closer.fastballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(CloserAttack());
            }
            else
                print("Not enough energy!");
        }

        //if (state != BattleState.PLAYERTURN)
        //    return;


    }

    public void OnSliderButton()
    {
        if (state == BattleState.STARTER)
        {
            if (Starter.sliderStamina <= GameManager.StarterEnergy)
            {
                GameManager.StarterEnergy -= Starter.sliderStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(PlayerAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.MIDDLE)
        {
            if (MiddleReliever.sliderStamina <= GameManager.MidRelivEnergy)
            {
                GameManager.MidRelivEnergy -= MiddleReliever.sliderStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(MiddleAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.SETUP)
        {
            if (SetUp.sliderStamina <= GameManager.SetUpEnergy)
            {
                GameManager.SetUpEnergy -= SetUp.sliderStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(SetUpAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.CLOSER)
        {
            if (Closer.sliderStamina <= GameManager.CloserEnergy)
            {
                GameManager.CloserEnergy -= Closer.sliderStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(CloserAttack());
            }
            else
                print("Not enough energy!");
        }

        //if (state != BattleState.PLAYERTURN)
        //    return;

    }

    public void OnCurveballButton()
    {
        if (state == BattleState.STARTER)
        {
            if (Starter.curveballStamina <= GameManager.StarterEnergy)
            {
                GameManager.StarterEnergy -= Starter.curveballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(PlayerAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.MIDDLE)
        {
            if (MiddleReliever.curveballStamina <= GameManager.MidRelivEnergy)
            {
                GameManager.MidRelivEnergy -= MiddleReliever.curveballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(MiddleAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.SETUP)
        {
            if (SetUp.curveballStamina <= GameManager.SetUpEnergy)
            {
                GameManager.SetUpEnergy -= SetUp.curveballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(SetUpAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.CLOSER)
        {
            if (Closer.curveballStamina <= GameManager.CloserEnergy)
            {
                GameManager.CloserEnergy -= Closer.curveballStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(CloserAttack());
            }
            else
                print("Not enough energy!");
        }

        //if (state != BattleState.PLAYERTURN)
        //    return;

    }

    public void OnChangeUpButton()
    {
        if (state == BattleState.STARTER)
        {
            if (Starter.changeupStamina <= GameManager.StarterEnergy)
            {
                GameManager.StarterEnergy -= Starter.changeupStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                StarterEnergy.value = GameManager.StarterEnergy / GameManager.StarterEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(PlayerAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.MIDDLE)
        {
            if (MiddleReliever.changeupStamina <= GameManager.MidRelivEnergy)
            {
                GameManager.MidRelivEnergy -= MiddleReliever.changeupStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                MiddleEnergy.value = GameManager.MidRelivEnergy / GameManager.MidRelievEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(MiddleAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.SETUP)
        {
            if (SetUp.changeupStamina <= GameManager.SetUpEnergy)
            {
                GameManager.SetUpEnergy -= SetUp.changeupStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                SetUpEnergy.value = GameManager.SetUpEnergy / GameManager.SetUpEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(SetUpAttack());
            }
            else
                print("Not enough energy!");
        }

        if (state == BattleState.CLOSER)
        {
            if (Closer.changeupStamina <= GameManager.CloserEnergy)
            {
                GameManager.CloserEnergy -= Closer.changeupStamina;
                //UpdateStarterUI;
                fastball = true;

                PlayerMenu.SetActive(true);
                PlayerPitches.SetActive(false);
                CloserEnergy.value = GameManager.CloserEnergy / GameManager.CloserEnergyMax;
                //To Do Start Attack Animation
                StartCoroutine(CloserAttack());
            }
            else
                print("Not enough energy!");
        }

        //if (state != BattleState.PLAYERTURN)
        //    return;

    }

    public void OnCancelButton()
    {
        if (state != BattleState.STARTER)
            return;
        if (state != BattleState.SETUP)
            return;
        if (state != BattleState.MIDDLE)
            return;
        if (state != BattleState.CLOSER)
            return;

        PlayerMenu.SetActive(true);
        PlayerPitches.SetActive(false);
    }

  /*  public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        //To Do Start Attack Animation
        StartCoroutine(PlayerHeal());
    }*/

    public void BattleFinished()
    {
        SceneManager.LoadScene("Concourse");
    }
}
