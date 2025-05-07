using UnityEngine;

public class EnemyBattleUnit : MonoBehaviour
{
    public string enemyName;
    public int enemyLvl;

    public int damage;
    public int defense;

    public int maxHP;
    public int currentHP;

    public bool MakeDamage(int dmg, PlayerBattleUnit player) // verifica se o hp do jogador já chegou a 0
    {
        player.currentHP -= dmg;

        if (player.currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
