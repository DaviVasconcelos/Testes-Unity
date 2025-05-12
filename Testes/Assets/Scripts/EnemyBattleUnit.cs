using UnityEngine;

public class EnemyBattleUnit : MonoBehaviour
{
    public string enemyName;
    public int enemyLvl;
    public int damage;
    public int defense;
    public int maxHP;
    public int currentHP;
    public int maxMana;
    public int currentMana;

    public bool MakeDamage(int dmg, PlayerBattleUnit player)
    {
        player.currentHP -= dmg;
        return player.currentHP <= 0;
    }

    private void Awake()
    {
        // Acessa o inimigo do Overworld
        Enemy overworldEnemy = BattleManager.Instance.currentEnemy;

        if (overworldEnemy != null && overworldEnemy.entity != null)
        {
            // Copia os dados da entidade do inimigo da Overworld
            enemyName = overworldEnemy.entity.name;
            maxHP = overworldEnemy.entity.maxHealth;
            currentHP = overworldEnemy.entity.currentHealth;
            damage = overworldEnemy.entity.strength;
            defense = overworldEnemy.entity.defense;
            enemyLvl = overworldEnemy.entity.level;
            currentMana = overworldEnemy.entity.currentMana;
            maxMana = overworldEnemy.entity.maxMana;

            // Atualiza a vida atual para o valor persistido
            currentHP = overworldEnemy.entity.currentHealth;
        }
        else
        {
            Debug.LogError("Inimigo da Overworld não encontrado ou dados da entidade inválidos!");
        }
    }
}