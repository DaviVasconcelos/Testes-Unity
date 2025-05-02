using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData; // Dados do inimigo
    [SerializeField] private string battleSceneName = "TurnBattle";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Desativa o Sprite e o Controle do Player
            Player player = other.GetComponent<Player>();
            SpriteRenderer playerSprite = other.GetComponent<SpriteRenderer>();
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerSprite != null) playerSprite.enabled = false;
            if (playerController != null) playerController.enabled = false;

            // Passa os dados do inimigo para o BattleManager
            BattleManager.Instance.SetEnemyData(enemyData);

            // Salva a posição do jogador antes da batalha
            PlayerPositionManager.SavePlayerPosition();

            // Inicia a batalha
            BattleManager.Instance.StartBattle(battleSceneName);
        }
    }
}