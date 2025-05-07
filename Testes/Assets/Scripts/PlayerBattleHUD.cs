using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public Slider manaSlider;

    // Verifica alterações nos dados e passa para UI
    public void setHUD(PlayerBattleUnit player)
    {
        nameText.text = player.playerName;
        levelText.text = "Lvl: " + player.playerLvl;
        hpSlider.maxValue = player.maxHP;
        hpSlider.value = player.currentHP;
        manaSlider.maxValue = player.maxMana;
        manaSlider.value = player.currentMana;
    }

    public void setHp(int hp)
    {
        hpSlider.value = hp;
    }

    public void setMana(int mp)
    {
        manaSlider.value = mp;
    }
}
