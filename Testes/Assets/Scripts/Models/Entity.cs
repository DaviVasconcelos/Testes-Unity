using UnityEngine;
using System;

[Serializable]

public class Entity
{
    [Header("Game")]

    public int level;

    [Header("Name")]

    public string name;

    [Header("Health")]
    
    public int currentHealth;

    public int maxHealth;

    [Header("Mana")]

    public int currentMana;

    public int maxMana;

    [Header("Stamina")]

    public int currentStamina;

    public int maxStamina;

    [Header("Stats")]

    public int strength = 1;

    public int defense = 1;

    public int magic = 1;

    public float speed = 2f;

}
