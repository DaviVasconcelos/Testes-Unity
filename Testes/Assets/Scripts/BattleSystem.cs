using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Declara��o dos estados da batalha
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, PROCESSING }
public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public Transform PlayerBattleStation;
    public Transform EnemyBattleStation;

    PlayerBattleUnit playerUnit;
    EnemyBattleUnit enemyUnit;

    public TextMeshProUGUI enemyName;

    public PlayerBattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;

    public TextMeshProUGUI dialogText;

    public BattleState state;
    void Start()
    {
        // quando a batalha come�a, o battlestate � definido como start
        state = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle() // para ser IEnumerator, ou seja, uma coroutine (estudar mais depois) precisa do yield e do start coroutine
    {
        // Pega o GameObject do player baseado na instancia��o do player
        GameObject PlayerGO = Instantiate(PlayerPrefab, PlayerBattleStation);
        playerUnit = PlayerGO.GetComponent<PlayerBattleUnit>();

        // Pega o GameObject do enemy baseado na instancia��o do enemy
        GameObject EnemyGO = Instantiate(EnemyPrefab, EnemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<EnemyBattleUnit>();

        enemyName.text = enemyUnit.enemyName;

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

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
            playerHUD.setHp(playerUnit.currentHP);
            playerHUD.setMana(playerUnit.currentMana);
            dialogText.text = "Voc� consumiu " + playerUnit.HealPrice + " MP para curar " + HealPower + " HP";

            yield return new WaitForSeconds(1f);  // Espera para mostrar a mensagem

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogText.text = "Voc� n�o possui mana o suficiente!";
            yield return new WaitForSeconds(1f);  // Espera para mostrar a mensagem

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator PlayerAttack()
    {
        // verifica se o inimigo est� morto e faz o dano ser causado
        bool isDead = playerUnit.MakeDamage(playerUnit.damage, enemyUnit);

        // passa os dados da batalha para a hud
        enemyHUD.setHp(enemyUnit.currentHP);
        dialogText.text = "Ataque bem sucedido";

        yield return new WaitForSeconds(2f);

        playerHUD.setMana(playerUnit.currentMana);

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
        playerHUD.setHp(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
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
        } else if (state == BattleState.LOST)
        {
            dialogText.text = "Voc� morreu";
        }
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN) // se n�o for o turno do player, ele somente retorna
        {
            return;
        } else
        {
            state = BattleState.PROCESSING; // Impede novos ataques durante o processamento
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
            state = BattleState.PROCESSING; // Impede novos ataques durante o processamento
            StartCoroutine(PlayerHeal());
        }
    }
}
