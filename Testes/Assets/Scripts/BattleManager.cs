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
        Camera.main.GetComponent<AudioListener>().enabled = false;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        Player.Instance.GetComponent<PlayerController>().enabled = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // Remover componentes duplicados após carregar
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
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("TurnBattle");
        yield return asyncUnload;

        // Reativa elementos da cena principal
        //GameObject.Find("Grid").SetActive(true);
        //GameObject.Find("Canvas").SetActive(true);

        if (playerWon && currentEnemy != null)
        {
            Destroy(currentEnemy.gameObject);
            currentEnemy = null;
        }

        OverworldReferences.Instance.overworldRoot.SetActive(true);

        // Restaura componentes do Player
        Player.Instance.GetComponent<SpriteRenderer>().enabled = true;
        Player.Instance.GetComponent<PlayerController>().enabled = true;
    }
}