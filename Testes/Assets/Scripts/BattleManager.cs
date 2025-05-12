using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public Enemy currentEnemy;

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

    // Método para iniciar a batalha
    public void StartBattle(string battleSceneName)
    {
        StartCoroutine(LoadBattleSceneAsync(battleSceneName));
    }

    private IEnumerator LoadBattleSceneAsync(string sceneName)
    {
        // Desativa o audio listener do overworld enquanto carrega a batalha
        Camera.main.GetComponent<AudioListener>().enabled = false;

        // Carrega a cena de batalha em cima do overworld, como aditivo
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        // Desativa controles do overworld do player
        Player.Instance.GetComponent<PlayerController>().enabled = false;

        // Carrega 90% da cena → Trava → Libera ativação (allowSceneActivation = true) → Carrega 10% restante → Ativa a cena.
        // Ou seja, evita carregar a cena antes dela estar pronta
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // Remover componentes duplicados após carregar
        /* Medida de segurança para garantir que sistemas globais (EventSystem e AudioListener)
        não entrem em conflito entre a cena principal e a cena de batalha carregada aditivamente. */
        Scene battleScene = SceneManager.GetSceneByName(sceneName);
        foreach (GameObject rootObj in battleScene.GetRootGameObjects())
        {
            EventSystem es = rootObj.GetComponent<EventSystem>();
            if (es != null) Destroy(es.gameObject);

            AudioListener al = rootObj.GetComponent<AudioListener>();
            if (al != null) Destroy(al);
        }

        if (currentEnemy != null)
        {
            currentEnemy.gameObject.SetActive(false);
        }
    }
    public IEnumerator EndBattle(bool playerWon)
    {
        if (Camera.main != null)
        {
            // Reativa o audio listener do overworld
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("TurnBattle");
        yield return asyncUnload;

        if (playerWon && currentEnemy != null)
        {
            Destroy(currentEnemy.gameObject);
            currentEnemy = null;
        }

        // Reativa o overworld
        OverworldReferences.Instance.overworldRoot.SetActive(true);

        // Restaura componentes do Player
        Player.Instance.GetComponent<SpriteRenderer>().enabled = true;
        Player.Instance.GetComponent<PlayerController>().enabled = true;
    }
}