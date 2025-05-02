using System;
using UnityEngine;

public class PlayerBattleUnit : MonoBehaviour
{

    public string playerName;
    public int enemyLvl;
    public int damage;
    public int defense;
    public int maxHP;
    public int currentHP;

    private void Start()
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

            // Debug dos dados iniciais
            Debug.Log("=== DADOS DO JOGADOR NA BATALHA ===");
            Debug.Log($"Nome: {playerName}");
            Debug.Log($"Level: {Player.Instance.entity.level}");
            Debug.Log($"HP: {currentHP}/{maxHP}");
            Debug.Log($"Força: {damage}");
            Debug.Log($"Defesa: {defense}");
            Debug.Log($"Velocidade: {Player.Instance.entity.speed}");
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
