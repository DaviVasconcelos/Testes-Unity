using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] private string battleSceneName = "TurnBattle";
    private BoxCollider2D enemyCollider;

    private void Start()
    {
        // Pega o colisor do inimigo (colocar no inspector)
        enemyCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Colisão do tipo trigger com o player
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

            // DESATIVAÇÃO DO OVERWORLDROOT, para focar na cena de batalha
            OverworldReferences.Instance.overworldRoot.SetActive(false);

            // Atribui o inimigo atual ANTES de iniciar a batalha
            BattleManager.Instance.currentEnemy = GetComponent<Enemy>();

            // Esconde o inimigo imediatamente
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Salva posição do player e inicia batalha
            PlayerPositionManager.SavePlayerPosition();
            BattleManager.Instance.StartBattle(battleSceneName);

            // Desativa o Sprite e o Controle do Player
            Player.Instance.GetComponent<SpriteRenderer>().enabled = false;
            Player.Instance.GetComponent<PlayerController>().enabled = false;
        }
    }
}