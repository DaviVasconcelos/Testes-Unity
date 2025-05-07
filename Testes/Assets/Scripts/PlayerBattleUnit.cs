using System;
using UnityEngine;

public class PlayerBattleUnit : MonoBehaviour
{

    public string playerName;
    public int playerLvl;
    public int damage;
    public int defense;
    public int maxHP;
    public int currentHP;
    public int currentMana;
    public int maxMana;
    public int HealPrice = 10;

    public bool MakeDamage(int dmg, EnemyBattleUnit enemy) // verifica se o hp do inimigo já chegou a 0
    {
        enemy.currentHP -= dmg;

        if (enemy.currentHP <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        // checa se tem mana o suficiente
        if (currentMana >= HealPrice)
        {
            currentHP += amount;
        } else {
            return;
        }

        // checa se o hp está cheio
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
    }

    private void Awake() // Usar awake no lugar de start é melhor, porque no start ele inicializa primeiro a HUD, ficando sem os dados do jogador
    {
        // Pega os dados do objeto player para utilizar em batalha
        if (Player.Instance != null && Player.Instance.entity != null)
        {
            // Acesse os dados da entidade
            playerName = Player.Instance.entity.name;
            maxHP = Player.Instance.entity.maxHealth;
            currentHP = Player.Instance.entity.currentHealth;
            damage = Player.Instance.entity.strength;
            defense = Player.Instance.entity.defense;
            playerLvl = Player.Instance.entity.level;
            currentMana = Player.Instance.entity.currentMana;
            maxMana = Player.Instance.entity.maxMana;

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
            Debug.LogError("Player ou entity não encontrado!");
        }
    }

    // Muda os dados originais a depender do resultado da batalha
    public void ApplyBattleResults()
    {
        Player.Instance.entity.currentHealth = currentHP;
        // Atualize outros atributos conforme necessário
    }
}
