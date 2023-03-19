using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [Header("Level Up")]
    // Stat || + # (This is the player's initial stats)
    [SerializeField] int lucDamage;
    [SerializeField] int lucMaxHealth;
    [SerializeField] int lucDefense;
    [SerializeField] int lucCrit;
    [SerializeField] float lucCritChance;
    [SerializeField] float lucImmunity;
    [SerializeField] float lucMaxMoveSpeed;
    [SerializeField] float lucRollSpeed;
    // Stat # + || (This is the numbers that stats are going to be increased by)
    [SerializeField] float lufDamage;
    [SerializeField] float lufMaxHealth;
    [SerializeField] float lufDefense;
    [SerializeField] float lufCrit;
    [SerializeField] float lufCritChance;
    [SerializeField] float lufImmunity;
    [SerializeField] float lufMaxMoveSpeed;
    [SerializeField] float lufRollSpeed;
    // Stat text
    [SerializeField] TextMeshProUGUI lutDamage;
    [SerializeField] TextMeshProUGUI lutMaxHealth;
    [SerializeField] TextMeshProUGUI lutDefense;
    [SerializeField] TextMeshProUGUI lutCrit;
    [SerializeField] TextMeshProUGUI lutCritChance;
    [SerializeField] TextMeshProUGUI lutImmunity;
    [SerializeField] TextMeshProUGUI lutMaxMoveSpeed;
    [SerializeField] TextMeshProUGUI lutRollSpeed;
    // Given random stats
    [SerializeField] float gDamage;
    [SerializeField] float gMaxHealth;
    [SerializeField] float gDefense;
    [SerializeField] float gCrit;
    [SerializeField] float gCritChance;
    [SerializeField] float gImmunity;
    [SerializeField] float gMaxMoveSpeed;
    [SerializeField] float gRollSpeed;

    [Header("EndResult")]
    // Stat || (This is the combination of the players initial stats + numbers to increase by)
    [SerializeField] float erfDamage;
    [SerializeField] float erfMaxHealth;
    [SerializeField] float erfDefense;
    [SerializeField] float erfCrit;
    [SerializeField] float erfCritChance;
    [SerializeField] float erfImmunity;
    [SerializeField] float erfMaxMoveSpeed;
    [SerializeField] float erfRollSpeed;
    // Stat text
    [SerializeField] TextMeshProUGUI ertDamage;
    [SerializeField] TextMeshProUGUI ertMaxHealth;
    [SerializeField] TextMeshProUGUI ertDefense;
    [SerializeField] TextMeshProUGUI ertCrit;
    [SerializeField] TextMeshProUGUI ertCritChance;
    [SerializeField] TextMeshProUGUI ertImmunity;
    [SerializeField] TextMeshProUGUI ertMaxMoveSpeed;
    [SerializeField] TextMeshProUGUI ertRollSpeed;

    [Header("Buttons")]
    [SerializeField] GameObject bDamage;
    [SerializeField] GameObject bMaxHealth;
    [SerializeField] GameObject bDefense;
    [SerializeField] GameObject bCrit;
    [SerializeField] GameObject bCritChance;
    [SerializeField] GameObject bImmunity;
    [SerializeField] GameObject bMaxMoveSpeed;
    [SerializeField] GameObject bRollSpeed;


    [Header("Misc")]
    [SerializeField] int points;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] Toggle heal;
    CharacterStatistics player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterStatistics>();

        lucDamage = player.damage;
        lucMaxHealth = player.maxHealth;
        lucDefense = player.defense;
        lucCrit = player.crit;
        lucCritChance = player.critChance;
        lucImmunity = player.immunity;
        lucMaxMoveSpeed = player.maxMoveSpeed;
        lucRollSpeed = player.rollSpeed;

        points = player.points;
        player.points = 0;

        // Random stat increases
        lufDamage = Mathf.RoundToInt(Random.Range(1, 5));
        lufMaxHealth = Mathf.RoundToInt(Random.Range(1, 5));
        lufDefense = Mathf.RoundToInt(Random.Range(1, 5));
        lufCrit = Mathf.RoundToInt(Random.Range(0, 3));
        lufCritChance = Mathf.RoundToInt(Random.Range(1, 5));
        lufImmunity = Mathf.RoundToInt(Random.Range(0, 3));
        lufImmunity /= 10;
        lufMaxMoveSpeed = Mathf.RoundToInt(Random.Range(1, 2));
        lufRollSpeed = Mathf.RoundToInt(Random.Range(1, 2));

        // Remember what the random stat increases were
        gDamage = lufDamage;
        gMaxHealth = lufMaxHealth;
        gDefense = lufDefense;
        gCrit = lufCrit;
        gCritChance = lufCritChance;
        gImmunity = lufImmunity;
        gMaxMoveSpeed = lufMaxMoveSpeed;
        gRollSpeed = lufRollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        TextUpdate();
        CanSubtract();
    }

    void TextUpdate()
    {
        #region LevelUp
        // Formular: luc + luf = erf
        // Text: lut = "Stat: " + luc + " + " + luf
        lutDamage.text = "Damage: " + lucDamage + " + " + lufDamage;
        lutMaxHealth.text = "Max Health: " + lucMaxHealth + " + " + lufMaxHealth;
        lutDefense.text = "Defense: " + lucDefense + " + " + lufDefense;
        lutCrit.text = "Critical Damage: " + lucCrit + " + " + lufCrit;
        lutCritChance.text = "Critical Chance: " + lucCritChance + " + " + lufCritChance;
        lutImmunity.text = "Immunity: " + lucImmunity + " + " + lufImmunity;
        lutMaxMoveSpeed.text = "Move Speed: " + lucMaxMoveSpeed + " + " + lufMaxMoveSpeed;
        lutRollSpeed.text = "Roll Distance: " + lucRollSpeed + " + " + lufRollSpeed;
        #endregion

        #region EndResult
        //Formular: erf = luc + luf
        erfDamage = lucDamage + lufDamage;
        erfMaxHealth = lucMaxHealth + lufMaxHealth;
        erfDefense = lucDefense + lufDefense;
        erfCrit = lucCrit + lufCrit;
        erfCritChance = lucCritChance + lufCritChance;
        erfImmunity = lucImmunity + lufImmunity;
        erfMaxMoveSpeed = lucMaxMoveSpeed + lufMaxMoveSpeed;
        erfRollSpeed = lucRollSpeed + lufRollSpeed;

        // Text: ert = "Stat: " luc + luf
        ertDamage.text = "Damage: " + erfDamage;
        ertMaxHealth.text = "Max Health: " + erfMaxHealth;
        ertDefense.text = "Defense: " + erfDefense;
        ertCrit.text = "Critical Damage: " + erfCrit;
        ertCritChance.text = "Critical Chance: " + erfCritChance;
        ertImmunity.text = "Immunity: " + erfImmunity;
        ertMaxMoveSpeed.text = "Move Speed: " + erfMaxMoveSpeed;
        ertRollSpeed.text = "Roll Distance: " + erfRollSpeed;
        #endregion

        #region Points
        pointsText.text = "Points: " + points;
        #endregion
    }

    void CanSubtract()
    {
        if (lufDamage <= gDamage)
            bDamage.SetActive(false);
        else
            bDamage.SetActive(true);
        if (lufMaxHealth <= gMaxHealth)
            bMaxHealth.SetActive(false);
        else
            bMaxHealth.SetActive(true);
        if (lufDefense <= gDefense)
            bDefense.SetActive(false);
        else
            bDefense.SetActive(true);
        if (lufCrit <= gCrit)
            bCrit.SetActive(false);
        else
            bCrit.SetActive(true);
        if (lufCritChance <= gCritChance)
            bCritChance.SetActive(false);
        else
            bCritChance.SetActive(true);
        if (lufImmunity <= gImmunity)
            bImmunity.SetActive(false);
        else
            bImmunity.SetActive(true);
        if (lufMaxMoveSpeed <= gMaxMoveSpeed)
            bMaxMoveSpeed.SetActive(false);
        else
            bMaxMoveSpeed.SetActive(true);
        if (lufRollSpeed <= gRollSpeed)
            bRollSpeed.SetActive(false);
        else
            bRollSpeed.SetActive(true);
    }

    public void AddStatChange(string stat)
    {
        if(points > 0)
        {
            switch (stat)
            {
                case "damage":
                    lufDamage++;
                    points--;
                    break;

                case "maxhealth":
                    lufMaxHealth++;
                    points--;
                    break;

                case "defense":
                    lufDefense++;
                    points--;
                    break;

                case "criticaldamage":
                    lufCrit++;
                    points--;
                    break;

                case "criticalchance":
                    lufCritChance++;
                    points--;
                    break;

                case "immunity":
                    lufImmunity += 0.1f;
                    points--;
                    break;

                case "movespeed":
                    lufMaxMoveSpeed++;
                    points--;
                    break;

                case "rolldistance":
                    lufRollSpeed++;
                    points--;
                    break;

                default:
                    break;
            }
        }
    }
    
    public void SubtractStatChange(string stat)
    {
        switch (stat)
        {
            case "damage":
                if(lufDamage > gDamage)
                {
                    lufDamage--;
                    points++;
                }
                break;

            case "maxhealth":
                if (lufMaxHealth > gMaxHealth)
                {
                    lufMaxHealth--;
                    points++;
                }
                break;

            case "defense":
                if (lufDefense > gDefense)
                {
                    lufDefense--;
                    points++;
                }
                break;

            case "criticaldamage":
                if (lufCrit > gCrit)
                {
                    lufCrit--;
                    points++;
                }
                break;

            case "criticalchance":
                if (lufCritChance > gCritChance)
                {
                    lufCritChance--;
                    points++;
                }
                break;

            case "immunity":
                if (lufImmunity > gImmunity)
                {
                    lufImmunity -= 0.1f;
                    points++;
                }
                break;

            case "movespeed":
                if (lufMaxMoveSpeed > gMaxMoveSpeed)
                {
                    lufMaxMoveSpeed--;
                    points++;
                }
                break;

            case "rolldistance":
                if (lufRollSpeed > gRollSpeed)
                {
                    lufRollSpeed--;
                    points++;
                }
                break;

            default:
                break;
        }
    }

    public void Heal()
    {
        if (heal.isOn)
            points--;
        else
            points++;
    }

    public void Continue()
    {
        player.damage = Mathf.RoundToInt(erfDamage);
        player.maxHealth = Mathf.RoundToInt(erfMaxHealth);
        player.defense = Mathf.RoundToInt(erfDefense);
        player.crit = Mathf.RoundToInt(erfCrit);
        player.critChance = Mathf.RoundToInt(erfCritChance);
        player.immunity = (erfImmunity);
        player.maxMoveSpeed = Mathf.RoundToInt(erfMaxMoveSpeed);
        player.rollSpeed = Mathf.RoundToInt(erfRollSpeed);

        if (heal.isOn)
        {
            player.health = player.maxHealth;
        }

        Destroy(gameObject);
    }


}
