using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Entity entity;

    // C�digo para o objeto player persistir em mudan�as de cenas:
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

    // Mec�nica de regenera��o no overworld
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

    [Header("Combat")]
    public bool inCombat = false;

    // Sliders de status da interface
    [Header("PlayerUI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;
    public Slider experience;

    void Start()
    {
        // Game controller
        if (controller == null) // Caso o gamecontroller n�o for anexado, o o erro acontece e n�o passa dele
        { 
            Debug.LogError("Anexar GameController no player");
            return;
        }

        // Fun��es de c�lculo
        entity.maxHealth = controller.CalculateHealth(entity);
        entity.maxMana = controller.CalculateMana(entity);
        entity.maxStamina = controller.CalculateStamina(entity);

        // Atributos que come�am cheios
        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        // Liga��o entre valor m�ximo da entidade com o m�ximo do slider
        health.maxValue = entity.maxHealth;
        mana.maxValue = entity.maxMana;
        stamina.maxValue = entity.maxStamina;

        health.value = entity.maxHealth;
        mana.value = entity.maxMana;
        stamina.value = entity.maxStamina;

        // XP come�a no 0
        experience.value = 0;

        // Iniciar a regenera��o de HP e MP
        StartCoroutine(RegenHealth());
        StartCoroutine(RegenMana());
    }

    private void Update()
    {
        // Mec�nica de atualizar atributos com base no atual
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;

        // Diminui HP ao apertar espa�o, mas n�o diminui se for 0
        if (Input.GetKeyDown(KeyCode.Backspace) && entity.currentHealth > 0)
        {
            entity.currentHealth -= 10;
        }

        if (Input.GetKeyDown(KeyCode.Space) && entity.currentHealth < entity.maxHealth)
        {
            entity.currentHealth += 10;
        }
    }

    IEnumerator RegenHealth()
    {
        bool fullHealth;

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
            while (fullHealth == false) // Loop enquanto a vida n�o estiver completa
            {
                // Regenera 1 por segundo
                entity.currentHealth += HpRegenQuantity;
                yield return new WaitForSeconds(HpRegenSeconds);
            }
        }
    }

    IEnumerator RegenMana()
    {
        bool fullMana;

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
            while (fullMana == false) // Loop enquanto a mana n�o estiver completa
            {
                // Regenera 1 por segundo
                entity.currentMana += MpRegenQuantity;
                yield return new WaitForSeconds(MpRegenSeconds);
            }
        }
    }

    // Mec�nica de trocar de cenas
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Assina o evento de cena carregada
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Descarrega para evitar vazamentos
    }

    // M�todo chamado toda vez que uma cena � carregada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "OverworldScene")
        {
            StartCoroutine(ReassignUIAfterDelay());
        }
    }

    IEnumerator ReassignUIAfterDelay()
    {
        // Espera 1 frame para garantir que a UI est� ativa
        yield return null;

        health = GameObject.Find("HPslider").GetComponent<Slider>();
        mana = GameObject.Find("MPslider").GetComponent<Slider>();
        stamina = GameObject.Find("STMslider").GetComponent<Slider>();
        experience = GameObject.Find("EXPslider").GetComponent<Slider>();

        // Atualiza os valores
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;
    }
}
