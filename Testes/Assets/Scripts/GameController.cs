using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Int32 CalculateHealth(Player player)
    {
        // Fórmula: (level * 4) + 50
        Int32 result = (player.entity.level * 4) + 50;
        Debug.LogFormat("CalculateHealth: {0}", result);
        return result;
    }

    public Int32 CalculateMana(Player player)
    {
        // Fórmula: (magic * 10) + (level * 4) + 5
        Int32 result = (player.entity.magic * 10) + (player.entity.level * 4) + 5;
        Debug.LogFormat("CalculateMana: {0}", result);
        return result;
    }

    public Int32 CalculateStamina(Player player)
    {
        // Fórmula: (level * 5) + 40
        Int32 result = (player.entity.level * 5) + 40;
        Debug.LogFormat("CalculateStamina: {0}", result);
        return result;
    }

    // Não imlementado ainda:
    public Int32 CalculateDamage(Player player, int weaponDamage)
    {
        // Fórmula: (strength * 2) + weaponDamage + (level * 3) + random (1-10)
        System.Random rnd = new System.Random();
        Int32 result = (player.entity.strength * 2) + weaponDamage + (player.entity.level * 3) + rnd.Next(1, 10);
        Debug.LogFormat("CalculateDamage: {0}", result);
        return result;
    }

    public Int32 CalculateDefense(Player player, int armorDefense)
    {
        // Fórmula: (defense * 2) + armorDefense + (level * 3)
        System.Random rnd = new System.Random();
        Int32 result = (player.entity.defense * 2) + armorDefense + (player.entity.level * 3);
        Debug.LogFormat("CalculateDefense: {0}", result);
        return result;
    }
}
