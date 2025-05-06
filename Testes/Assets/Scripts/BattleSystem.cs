using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Declaração dos estados da batalha
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
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

    public BattleState state;
    void Start()
    {
        // quando a batalha começa, o battlestate é definido como start
        state = BattleState.START;

        SetupBattle();
    }

    void SetupBattle()
    {
        // Pega o GameObject do player baseado na instanciação do player
        GameObject PlayerGO = Instantiate(PlayerPrefab, PlayerBattleStation);
        playerUnit = PlayerGO.GetComponent<PlayerBattleUnit>();

        // Pega o GameObject do enemy baseado na instanciação do enemy
        GameObject EnemyGO = Instantiate(EnemyPrefab, EnemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<EnemyBattleUnit>();

        enemyName.text = enemyUnit.enemyName;

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);
    }
}
