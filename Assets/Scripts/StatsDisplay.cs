using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private Stats[] playerClasses;

    [Header("Stats")]
    public Text _displayPlayerClass;
    public Text _displayDamage; // The character's base damage
    public Text _displayMaxHealth; // The character's maximum health
    public Text _displayDefense; // The character's defense against enemy attacks
    public Text _displayCrit; // The character's critical hit damage multiplier
    public Text _displayCritChance; // The character's chance to land a critical hit
    public Text _displayImmunity; // The duration of the character's invincibility frames after being hit       
    public Text moveSpeed; // The character's current movement speed
    public Text rollSpeed; // The speed/distance of the character's dodge roll
    public Image playerSprite;

    private void Start()
    {
        Debug.Log(stats.playerClass);
    }

    public void SelectedClassInfo(int i) //Which playerclass it is.
    {
        stats = playerClasses[i];
        _displayPlayerClass.text = "Class: " +stats.playerClass;
        _displayDamage.text = "Damage: " + stats.damage;
        _displayMaxHealth.text = "Max Health: " + stats.maxHealth;
        _displayDefense.text = "Defense: " + stats.defense;
        _displayCrit.text = "Crit Damage: " + stats.crit;
        _displayCritChance.text = "Crit Chance: " + stats.critChance;
        _displayImmunity.text = "Immunity: " + stats.immunity;
        moveSpeed.text = "Move Speed: " +stats.moveSpeed;
        rollSpeed.text = "Roll Speed: " + stats.rollSpeed;
        playerSprite.sprite = stats.characterSprite;
        
    }

    #region Testing
  public void RandomSelection()
    {
        int v = Random.Range(0, playerClasses.Length);
        SelectedClassInfo(v);
    }
    #endregion
}
