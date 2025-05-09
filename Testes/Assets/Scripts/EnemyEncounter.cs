using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] private string battleSceneName = "TurnBattle";
    private BoxCollider2D enemyCollider;

    private void Start()
    {
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica se o inimigo ainda não foi destruído
            if (GetComponent<Enemy>() == null || GetComponent<Enemy>().entity == null)
            {
                Debug.LogError("Inimigo ou dados da entidade inválidos!");
                return;
            }

            // Garantir que o BattleManager está pronto
            if (!BattleManager.Instance.gameObject.activeInHierarchy)
            {
                BattleManager.Instance.gameObject.SetActive(true);
            }

            // DESATIVAÇÃO DO OVERWORLDROOT (e todos os filhos, incluindo Grid e Canvas)
            OverworldReferences.Instance.overworldRoot.SetActive(false);

            // Atribui o inimigo atual ANTES de iniciar a batalha
            BattleManager.Instance.currentEnemy = GetComponent<Enemy>();

            // Opcional: Esconde o inimigo imediatamente
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Salva posição e inicia batalha
            PlayerPositionManager.SavePlayerPosition();
            BattleManager.Instance.StartBattle(battleSceneName);

            // Desativa o Sprite e o Controle do Player
            Player.Instance.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}