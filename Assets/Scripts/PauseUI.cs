using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

    //Need to add a reference to the players actual Stats.
    [SerializeField]
    private CharacterStatistics characterStatistics;

    [Header("PauseStats")]
    public int playerClassID;
    public Image playerSprite;
   
    public Text _displayPlayerClass;
    public Text _displayDamage; // The character's base damage
    public Text _displayCurHealth; // The character's maximum health
    public Text _displayMaxHealth; // The character's maximum health
    public Text _displayDefense; // The character's defense against enemy attacks
    public Text _displayCrit; // The character's critical hit damage multiplier
    public Text _displayCritChance; // The character's chance to land a critical hit
    public Text _displayImmunity; // The duration of the character's invincibility frames after being hit       
    public Text moveSpeed; // The character's current movement speed
    public Text rollSpeed; // The speed/distance of the character's dodge roll
    //[SerializeField] private Stats stats;
    //[SerializeField] private Stats[] playerClasses;


    private void Start()
    {
        //  Debug.Log(stats.playerClass);

    }
    public void PauseStatDisplay()
    {
        _displayPlayerClass.text = "Class: " + characterStatistics.playerClassName ;
        _displayDamage.text = "Damage: " + characterStatistics.damage ;
        _displayCurHealth.text = "Current Health: " + characterStatistics.health;
        _displayMaxHealth.text = "Max Health: " + characterStatistics.maxHealth ;
        _displayDefense.text = "Defense: " + characterStatistics.defense ;
        _displayCrit.text = "Crit Damage: " + characterStatistics.crit;
        _displayCritChance.text = "Crit Chance: " + characterStatistics.critChance;
        _displayImmunity.text = "Immunity: " + characterStatistics.immunity ;
        moveSpeed.text = "Move Speed: " + characterStatistics.moveSpeed ;
        rollSpeed.text = "Roll Speed: " + characterStatistics.rollSpeed;
        playerSprite.sprite = characterStatistics.stats.characterSprite;
        //Need to add Reference to the hero's Sprite
        //DO SOMETHING WHEN PAUSED

    }




}
