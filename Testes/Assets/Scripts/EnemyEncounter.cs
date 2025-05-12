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
        if (other.CompareTag("Player")) // Colis�o do tipo trigger com o player
        {
            // Verifica se o inimigo ainda n�o foi destru�do
            if (GetComponent<Enemy>() == null || GetComponent<Enemy>().entity == null)
            {
                Debug.LogError("Inimigo ou dados da entidade inv�lidos!");
                return;
            }

            // Garantir que o BattleManager est� pronto
            if (!BattleManager.Instance.gameObject.activeInHierarchy)
            {
                BattleManager.Instance.gameObject.SetActive(true);
            }

            // DESATIVA��O DO OVERWORLDROOT, para focar na cena de batalha
            OverworldReferences.Instance.overworldRoot.SetActive(false);

            // Atribui o inimigo atual ANTES de iniciar a batalha
            BattleManager.Instance.currentEnemy = GetComponent<Enemy>();

            // Esconde o inimigo imediatamente
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Salva posi��o do player e inicia batalha
            PlayerPositionManager.SavePlayerPosition();
            BattleManager.Instance.StartBattle(battleSceneName);

            // Desativa o Sprite e o Controle do Player
            Player.Instance.GetComponent<SpriteRenderer>().enabled = false;
            Player.Instance.GetComponent<PlayerController>().enabled = false;
        }
    }
}