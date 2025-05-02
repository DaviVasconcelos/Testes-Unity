using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    // Dados do inimigo atual
    private EnemyData currentEnemyData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicatas
        }
    }

    // Define os dados do inimigo para a batalha
    public void SetEnemyData(EnemyData data)
    {
        currentEnemyData = data;
    }

    // Retorna os dados do inimigo atual
    public EnemyData GetEnemyData()
    {
        return currentEnemyData;
    }

    // Método para iniciar a batalha
    public void StartBattle(string battleSceneName)
    {
        SceneManager.LoadScene(battleSceneName);
    }
}