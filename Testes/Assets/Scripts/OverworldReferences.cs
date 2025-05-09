using UnityEngine;

public class OverworldReferences : MonoBehaviour
{
    public static OverworldReferences Instance;

    public GameObject overworldRoot; // Refer�ncia ao GameObject "OverworldRoot"

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