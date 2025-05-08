using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Controller")]
    public Entity entity = new Entity(); // Recebe a mesma entidade do player
    public Animator animator;
    public GameController controller;
    // Código para o objeto enemy persistir em mudanças de cenas:
    public static Enemy Instance; // Singleton

    [Header("Patrol")]
    public Transform[] waypointList;
    public float arrivalDistance = 0.5f;
    public float waitTime = 5f;

    [Header("Combat")]
    public bool inCombat = false;
    public GameObject target;

    // Privadas
    private Transform targetWaipoint;
    private int currentWaipoint = 0;
    private float lastDistanceToTarget = 0f;
    private float currentWaitTime = 0f;

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
}
