using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Entity entity;

    // Código para o objeto player persistir em mudanças de cenas:
    public static Player Instance; // Singleton

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

        [Header("Regen HP/MP")]
    public bool RegenHpEnable = true;
    public bool RegenMpEnable = true;
    public float HpRegenSeconds = 1f;
    public float MpRegenSeconds = 3f;
    public int HpRegenQuantity = 1;
    public int MpRegenQuantity = 1;

    // Game controller
    [Header("GameController")]
    public GameController controller;

    // Sliders de status da interface
    [Header("PlayerUI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;
    public Slider experience;

    void Start()
    {
        // Game controller
        if (controller == null) // Caso o gamecontroller não for anexado, o o erro acontece e não passa dele
        { 
            Debug.LogError("Anexar GameController no player");
            return;
        }

        // Funções de cálculo
        entity.maxHealth = controller.CalculateHealth(this);
        entity.maxMana = controller.CalculateMana(this);
        entity.maxStamina = controller.CalculateStamina(this);

        // Atributos que começam cheios
        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        // Ligação entre valor máximo da entidade com o máximo do slider
        health.maxValue = entity.maxHealth;
        mana.maxValue = entity.maxMana;
        stamina.maxValue = entity.maxStamina;

        health.value = entity.maxHealth;
        mana.value = entity.maxMana;
        stamina.value = entity.maxStamina;

        // XP começa no 0
        experience.value = 0;

        // Iniciar a regeneração de HP e MP
        StartCoroutine(regenHealth());
        StartCoroutine(regenMana());
    }

    private void Update()
    {
        // Mecânica de atualizar atributos com base no atual
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;

        // Diminui HP ao apertar espaço, mas não diminui se for 0
        if (Input.GetKeyDown(KeyCode.Backspace) && entity.currentHealth > 0)
        {
            entity.currentHealth -= 10;
        }

        if (Input.GetKeyDown(KeyCode.Space) && entity.currentHealth < entity.maxHealth)
        {
            entity.currentHealth += 10;
        }
    }

    IEnumerator regenHealth()
    {
        bool fullHealth = true;

        if (entity.currentHealth >= entity.maxHealth)
        {
            fullHealth = false;
        }
        else
        {
            fullHealth = true;
        }

        if (RegenHpEnable == true)
        {
            while (fullHealth == false) // Loop enquanto a vida não estiver completa
            {
                // Regenera 1 por segundo
                entity.currentHealth += HpRegenQuantity;
                yield return new WaitForSeconds(HpRegenSeconds);
            }
        }
    }

    IEnumerator regenMana()
    {
        bool fullMana = true;

        if (entity.currentMana >= entity.maxMana)
        {
            fullMana = false;
        }
        else
        {
            fullMana = true;
        }

        if (RegenHpEnable == true)
        {
            while (fullMana == false) // Loop enquanto a mana não estiver completa
            {
                // Regenera 1 por segundo
                entity.currentMana += MpRegenQuantity;
                yield return new WaitForSeconds(MpRegenSeconds);
            }
        }
    }
}
