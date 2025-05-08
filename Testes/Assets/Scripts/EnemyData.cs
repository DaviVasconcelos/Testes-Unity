using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "RPG/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int enemyLvl;
    public int damage;
    public int defense;
    public int maxHP;
    public int currentHP;
}