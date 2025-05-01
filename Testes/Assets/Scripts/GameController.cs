using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Int32 CalculateHealth(Player player)
    {
        // Fórmula: (resistance * 10) + (level * 4) + 10
        Int32 result = (player.entity.resistance * 10) + (player.entity.level * 4) + 10;
        Debug.LogFormat("CalculateHealth: {0}", result);
        return result;
     }

}
