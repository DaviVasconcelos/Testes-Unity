using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    // Verifica alterações nos dados e passa para UI
    public void setHUD(PlayerBattleUnit unit)
    {
        nameText.text = unit.playerName;
        levelText.text = "Lvl " + unit.playerLvl;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void setHp(int hp)
    {
        hpSlider.value = hp;
    }
}
