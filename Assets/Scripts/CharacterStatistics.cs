using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    #region Player Stats
    [Header("Stats")]
    public int points = 0; // The amount of points the player has
    public int health = 0; // The character's current health
    public int maxHealth = 0; // The character's maximum health
    public int defense = 0; // The character's defense against enemy attacks
    public int damage = 0; // The character's base damage
    public int crit = 0; // The character's critical hit damage multiplier
    public float critChance = 0; // The character's chance to land a critical hit
    public float immunity = 0; // The duration of the character's invincibility frames after being hit
    public float maxMoveSpeed = 0; // The character's maximum movement speed
    public float moveSpeed; // The character's current movement speed
    public float rollSpeed = 0; // The speed/distance of the character's dodge roll
    public string playerClassName;

    private int playerClassNumber;
    [SerializeField] public Stats stats;
   
    [SerializeField] private StatsDisplay statSelect;
    private bool firstPlay = true;
    #endregion


    [Header("Misc")]
    public bool isFainted; // A check for if the character is fainted
    public GameObject hitBox; // A reference to the character's hitbox game object
    PlayerMovement playerMovement; // A reference to the character's PlayerMovement component
    public bool isPlayer; // A boolean to check if the character is a player or an enemy
    public float attackDuration = 0.5f; // The duration of the character's attack
    public bool isAttacking; // A boolean to check if the character is currently attacking

    [Header("Timers")]
    public float attackTimer; // A timer to when the character is no longer attacking

    // Start is called before the first frame update
    void Start()
    {
        
        playerMovement = GetComponent<PlayerMovement>(); // Assign a reference to the PlayerMovement component
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Timers();

        // Ensure health is not greater than maximum health
        if (health > maxHealth)
            health = maxHealth;

        // If the character's health is less than 1, set isFainted to true
        if (health < 1)
            isFainted = true;
    }

    void Attack()
    {
        // If attackTimer is greater than 0 and the game object is not rolling, set the move speed to 0, set isAttacking to true, and activate the hit box
        if (attackTimer > 0)
        {
            if (isPlayer)
            {
                if (!GetComponent<PlayerMovement>().isRolling)
                    moveSpeed = 0;
                isAttacking = true;
                hitBox.SetActive(true);
            }
            else
            {
                moveSpeed = 0;
                isAttacking = true;
                hitBox.SetActive(true);
            }
        }
        // Otherwise, set the move speed to the maximum move speed, set isAttacking to false, and deactivate the hit box
        else
        {
            moveSpeed = maxMoveSpeed;
            isAttacking = false;
            hitBox.SetActive(false);
        }
    }

    public void Upgrade(string stat)
    {
        if (points > 0) // Check if the player has upgrade points to spend
        {
            switch (stat) // Switch statement to determine which stat to upgrade
            {
                case "maxHealth":
                    maxHealth++; // Increase maximum health
                    break;
                case "defense":
                    defense++; // Increase defense
                    break;
                case "damage":
                    damage++; // Increase damage
                    break;
                case "crit":
                    crit++; // Increase critical hit chance
                    break;
                case "critChance":
                    critChance++; // Increase critical hit damage
                    break;
                case "immunity":
                    immunity += 0.1f; // Increase invincibility frames
                    break;
                case "moveSpeed":
                    maxMoveSpeed++; // Increase maximum move speed
                    break;
                case "rollSpeed":
                    rollSpeed++; // Increase roll speed/distance
                    break;
                default:
                    break; // Do nothing if the upgrade stat is invalid
            }
            points--; // Deduct a point from the player's upgrade points
        }
    }

    void Timers()
    {
        #region Attack
        if (attackTimer > 0) // Check if the attack timer is active
        {
            attackTimer -= Time.deltaTime; // Decrement the timer by deltaTime
            isAttacking = true; // Set the attacking to true
        }
        else
            isAttacking = false; // Set the attacking to false if the timer has expired
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStatistics enemyStats = collision.gameObject.GetComponent<CharacterStatistics>(); // Get the CharacterStatistics component from the collided game object
        #region isPlayer
        // If the current object is a player and collided object is an enemy
        if (isPlayer && collision.gameObject.tag == "Enemy")
        {
            int atkDmg = damage; // Initial attack damage value
            float critRate = Mathf.RoundToInt(Random.Range(1, 101 - critChance)); // Generate a random number using critChance
            atkDmg -= enemyStats.defense; // Decrease attack damage value by the enemy's defense value
            if (critRate == 1) // If randomly generated value for critRate was correct
                atkDmg *= crit; // Multiply damage value by crit power
            if (atkDmg < 1) // If the damage value is lower than 1
                atkDmg = 1; // Set damage to 1
            enemyStats.health -= atkDmg; // Decrease enemy's health value by the final damage value
        }
        #endregion

        #region !isPlayer
        if (!isPlayer && collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            int atkDmg = damage; // Initial attack damage value
            atkDmg -= enemyStats.defense; // Decrease attack damage value by the player's defense value
            if (atkDmg < 1) // If the damage value is lower than 1
                atkDmg = 1; // Set damage to 1
            enemyStats.health -= atkDmg; // Decrease player's health value by the final damage value
        }
        #endregion
    }

    public void FirstStart()
    {
        if (firstPlay)
        {
            stats = statSelect.stats;
            playerClassName = statSelect._displayPlayerClass.text;
            damage = stats.damage;
            maxHealth = stats.maxHealth;
            health = maxHealth;
            defense = stats.defense;
            crit = stats.crit;
            critChance = stats.critChance;
            immunity = stats.immunity;
            maxMoveSpeed = stats.moveSpeed;
            moveSpeed = stats.moveSpeed;
            rollSpeed = stats.rollSpeed;
            firstPlay = false;

        }
       //Do something else?
    }
}
