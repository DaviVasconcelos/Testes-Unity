using System;
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

    private void Awake() // Usar awake no lugar de start é melhor, porque no start ele inicializa primeiro a HUD, ficando sem os dados do jogador
    {
        // Pega os dados do objeto player para utilizar em batalha
        if (Enemy.Instance != null && Enemy.Instance.entity != null)
        {
            // Acesse os dados da entidade
            enemyName = Enemy.Instance.entity.name;
            maxHP = Enemy.Instance.entity.maxHealth;
            currentHP = Enemy.Instance.entity.currentHealth;
            damage = Enemy.Instance.entity.strength;
            defense = Enemy.Instance.entity.defense;
            enemyLvl = Enemy.Instance.entity.level;
            currentMana = Enemy.Instance.entity.currentMana;
            maxMana = Enemy.Instance.entity.maxMana;

            /*
            // Debug dos dados iniciais
            Debug.Log("=== DADOS DO JOGADOR NA BATALHA ===");
            Debug.Log($"Nome: {playerName}");
            Debug.Log($"Level: {Player.Instance.entity.level}");
            Debug.Log($"HP: {currentHP}/{maxHP}");
            Debug.Log($"Força: {damage}");
            Debug.Log($"Defesa: {defense}");
            Debug.Log($"Velocidade: {Player.Instance.entity.speed}");
            */
        }
        else
        {
            Debug.LogError("Enemy ou entity não encontrado!");
        }
    } 
}
