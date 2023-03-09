using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    [Header("Stats")]
    public int health = 1; // The character's current health
    public int defense = 1; // The character's defense against enemy attacks
    public int damage = 1; // The character's base damage
    public int crit = 1; // The character's critical hit damage multiplier
    public float critChance = 1; // The character's chance to land a critical hit
    public float immunity = 0.1f; // The duration of the character's invincibility frames after being hit
    public float moveSpeed = 1; // The character's movement speed
    public float rollSpeed = 2; // The speed/distance of the character's dodge roll

    [Header("Misc")]
    [SerializeField] GameObject hitBox;
    [SerializeField] bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStatistics enemyStats = collision.gameObject.GetComponent<CharacterStatistics>();
        int atkDmg = damage;
        if (isPlayer)
        {
            if (!enemyStats.isPlayer)
            {
                atkDmg -= enemyStats.defense;
                if (atkDmg <= 0)
                    atkDmg = 1;
                enemyStats.health -= atkDmg;
            }
        }
    }
}
