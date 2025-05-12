using UnityEngine;

public class OverworldReferences : MonoBehaviour
{
    // Basicamente, esse script faz o overworld não ser destruido ao entrar na tela de batalha, apenas oculta o GameObject overworldRoot
    public static OverworldReferences Instance;

    public GameObject overworldRoot; // Referência ao GameObject "OverworldRoot"

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}