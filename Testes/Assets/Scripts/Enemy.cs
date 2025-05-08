using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [Header("Controller")]
    public Entity entity = new Entity(); // Recebe a mesma entidade do player
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

    [Header("XP Reward")]
    public int experience = 0;
    public int lootMoneyMin = 100;
    public int lootMoneyMax = 400;

    [Header("Respawn")]
    public GameObject prefab;
    public bool respawn = false;
    public float respawnTime = 10f;

    // Privadas
    private Transform targetWaypoint;
    private int currentWaypoint = 0;
    private float lastDistanceToTarget = 0f;
    private float currentWaitTime = 0f;

    private Rigidbody2D rig2d;
    Animator animator;

    private void Start()
    {
        rig2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Procura o controller a partir do nome dele
        controller = GameObject.Find("GameController").GetComponent<GameController>();

        // Funções de cálculo
        entity.maxHealth = controller.CalculateHealth(entity);
        entity.maxMana = controller.CalculateMana(entity);
        entity.maxStamina = controller.CalculateStamina(entity);

        // Atributos que começam cheios
        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        // Mecânica de andar sozinho (ainda não sei muito bem como funciona)
        currentWaitTime = waitTime;

        if (waypointList.Length > 0)
        {
            targetWaypoint = waypointList[currentWaypoint];
            lastDistanceToTarget = Vector2.Distance(transform.position, targetWaypoint.position);
        }
    }

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
