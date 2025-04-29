using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Entity entity;

    // Sliders de status da interface
    [Header("PlayerUI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;
    public Slider experience;

    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.Space) && entity.currentHealth < 100)
        {
            entity.currentHealth += 10;
        }
    }
}
