using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Declara��o dos estados da batalha
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, PROCESSING }
public class BattleSystem : MonoBehaviour
{
    // Carrega os prefabs de batalha do player e do inimigo, mudar no inspector para alterar individualmente cada inimigo
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    // Posi��es de onde o sprite do player e do inimigo v�o ficar
    public Transform PlayerBattleStation;
    public Transform EnemyBattleStation;
    // Unidade em si do player e do inimigo, cada um com seus pr�prios atributos
    PlayerBattleUnit playerUnit;
    EnemyBattleUnit enemyUnit;
    // Dados para passar para o HUD
    public TextMeshProUGUI enemyName;
    public PlayerBattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;
    public TextMeshProUGUI dialogText;
    // Vari�vel para armazenar o estado atual do combate
    public BattleState state;

    void Start()
    {
        // Quando a batalha come�a, o battlestate � definido como start
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle() // para ser IEnumerator, ou seja, uma coroutine (estudar mais depois) precisa do yield e do start coroutine
    {
        // Instancia o playerUnit baseado no prefab de batalha que recebe do inspector e o PlayerBattleStation coloca ele nessa posi��o
        GameObject PlayerGO = Instantiate(PlayerPrefab, PlayerBattleStation);
        playerUnit = PlayerGO.GetComponent<PlayerBattleUnit>();

        // Pega os dados do inimigo do overworld e passa para o objeto de batalha
        Enemy overworldEnemy = BattleManager.Instance.currentEnemy;

        // Verifica se � nulo
        if (overworldEnemy == null)
        {
            Debug.LogError("Inimigo n�o encontrado no BattleManager!");
            yield break;
        }

        GameObject EnemyGO = Instantiate(EnemyPrefab, EnemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<EnemyBattleUnit>();

        // Copia dados do inimigo e passa para o setHUD
        enemyUnit.enemyName = overworldEnemy.entity.name;
        enemyUnit.maxHP = overworldEnemy.entity.maxHealth;
        enemyUnit.currentHP = overworldEnemy.entity.currentHealth;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f); // Fica 2 segundos no estado de START

        // Ap�s acabar o estado de START, muda para o turno do player
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // Aparece na interface o di�logo:
    void PlayerTurn()
    {
        dialogText.text = "Escolha sua a��o";
    }

    IEnumerator PlayerHeal()
    {
        int HealPower = 30;
        // Verifica mana ANTES de gastar
        if (playerUnit.currentMana >= playerUnit.HealPrice)
        {
            playerUnit.currentMana -= playerUnit.HealPrice;
            playerUnit.Heal(HealPower);

            // Atualiza HUD primeiro
            playerHUD.SetHp(playerUnit.currentHP);
            playerHUD.SetMana(playerUnit.currentMana);
            dialogText.text = "Voc� consumiu " + playerUnit.HealPrice + " MP para curar " + HealPower + " HP";

            yield return new WaitForSeconds(1f);  // Espera para mostrar a mensagem

            // Ao curar, passa o state para o turno do inimigo
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else // N�o tem mana suficiente
        {
            dialogText.text = "Voc� n�o possui mana o suficiente!";
            yield return new WaitForSeconds(1f);  // Espera para mostrar a mensagem

            // Se n�o tem mana, o turno continua sendo do player
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator PlayerAttack()
    {
        // Verifica se o inimigo est� morto e faz o dano ser aplicado
        bool isDead = playerUnit.MakeDamage(playerUnit.damage, enemyUnit);

        // Passa os dados da batalha para a HUD
        enemyHUD.SetHp(enemyUnit.currentHP);
        dialogText.text = "Ataque bem sucedido!";

        yield return new WaitForSeconds(2f);

        playerHUD.SetMana(playerUnit.currentMana);

        // checa se o inimigo est� morto

        if (isDead)
        {
            // termina a batalha em vit�ria
            state = BattleState.WON;
            EndBattle();
        } else
        {
            // turno do inimigo
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        // muda o state a depender do que aconteceu
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.enemyName + " ataca!";
        // verifica se o player est� morto e faz o dano ser causado
        bool isDead = enemyUnit.MakeDamage(enemyUnit.damage, playerUnit);
        yield return new WaitForSeconds(1f);

        // passa os dados da batalha para a hud
        playerHUD.SetHp(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead) // Se o player morreu, passa para o state LOST
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            dialogText.text = "Golpe recebido!";
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogText.text = "Inimigo est� morto";
            StartCoroutine(ReturnToOverworld());
        } else if (state == BattleState.LOST)
        {
            dialogText.text = "Voc� morreu";
            StartCoroutine(GameOver());
        }
    }

    IEnumerator ReturnToOverworld()
    {
        yield return new WaitForSeconds(2f);
        // Verifica��o refor�ada
        if (BattleManager.Instance != null)
        {
            // Usar StartCoroutine diretamente no Instance
            yield return BattleManager.Instance.StartCoroutine(
                BattleManager.Instance.EndBattle(true)
            );
        }
        else
        {
            // Fallback seguro
            SceneManager.LoadScene("OverworldScene");
        }

        // Destr�i o pr�prio BattleSystem ap�s conclus�o (n�o vi necessidade, mas pode ser �til)
        // Destroy(gameObject);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(BattleManager.Instance.EndBattle(false));
        SceneManager.LoadScene("GameOverScene"); // Criar cena de game over e colocar no inspector
        // Reativa controles do Player
        // Player.Instance.GetComponent<SpriteRenderer>().enabled = true;
        // Player.Instance.GetComponent<PlayerController>().enabled = true;
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) // se n�o for o turno do player, ele somente retorna
        {
            return;
        }
        else
        {
            state = BattleState.PROCESSING; // Impede bug de atacar v�rias vezes seguidas
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN) // se n�o for o turno do player, ele somente retorna
        {
            return;
        }
        else
        {
            state = BattleState.PROCESSING; // Impede bug de curar v�rias vezes seguidas
            StartCoroutine(PlayerHeal());
        }
    }
}
