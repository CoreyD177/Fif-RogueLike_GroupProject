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


    [Header("Misc")]
    [SerializeField] int points;
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
    }

    // Update is called once per frame
    void Update()
    {
        TextUpdate();
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
    }

    public void StatChange(string stat, float amount, int pointsRequired)
    {
        switch (stat)
        {
            case "damage":
                lufDamage += amount;
                points += pointsRequired;
                break;

            case "maxhealth":
                lufMaxHealth += amount;
                points += pointsRequired;
                break;

            default:
                break;
        }
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
