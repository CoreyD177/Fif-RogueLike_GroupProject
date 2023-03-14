using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat//The types of Stats a unit may have
{
    test,
    test2
}

[CreateAssetMenu(menuName = "Unit Stats")]
public class Stats : ScriptableObject
{
    [Header("Stats")]
    public string playerClass;
    //  public int points = 0; // The amount of points the player has
    //  public int health = 1; // The character's current health
    public int damage = 1; // The character's base damage
    public int maxHealth = 1; // The character's maximum health
    public int defense = 1; // The character's defense against enemy attacks

    public int crit = 2; // The character's critical hit damage multiplier
    public float critChance = 1; // The character's chance to land a critical hit
    public float immunity = 0.1f; // The duration of the character's invincibility frames after being hit
  //  public float maxMoveSpeed = 1; // The character's maximum movement speed
    public float moveSpeed; // The character's current movement speed
    public float rollSpeed = 2; // The speed/distance of the character's dodge roll
    public Sprite characterSprite;
    
}