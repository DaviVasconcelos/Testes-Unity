using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Int32 CalculateHealth(Entity entity)
    {
        // Fórmula: (level * 4) + 50
        Int32 result = entity.currentHealth;
        //Debug.LogFormat("CalculateHealth: {0}", result);
        return result;
    }

    public Int32 CalculateMana(Entity entity)
    {
        // Fórmula: (magic * 10) + (level * 4) + 5
        Int32 result = entity.currentMana;
        //Debug.LogFormat("CalculateMana: {0}", result);
        return result;
    }

    public Int32 CalculateStamina(Entity entity)
    {
        // Fórmula: (level * 5) + 40
        Int32 result = (entity.level * 5) + 40;
        //Debug.LogFormat("CalculateStamina: {0}", result);
        return result;
    }

    // Não imlementado ainda:
    public Int32 CalculateDamage(Entity entity, int weaponDamage)
    {
        // Fórmula: (strength * 2) + weaponDamage + (level * 3) + random (1-10)
        System.Random rnd = new System.Random();
        Int32 result = (entity.strength * 2) + weaponDamage + (entity.level * 3) + rnd.Next(1, 10);
        //Debug.LogFormat("CalculateDamage: {0}", result);
        return result;
    }

    public Int32 CalculateDefense(Entity entity, int armorDefense)
    {
        // Fórmula: (defense * 2) + armorDefense + (level * 3)
        System.Random rnd = new System.Random();
        Int32 result = (entity.defense * 2) + armorDefense + (entity.level * 3);
        //Debug.LogFormat("CalculateDefense: {0}", result);
        return result;
    }
}
/*
public Int32 CalculateMana(Entity entity)
{
    // Fórmula: (magic * 10) + (level * 4) + 5
    Int32 result = (entity.magic * 10) + (entity.level * 4) + 5;
    Debug.LogFormat("CalculateMana: {0}", result);
    return result;
}
*/