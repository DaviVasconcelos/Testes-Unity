using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    // Verifica alterações nos dados e passa para UI
    public void SetHUD(EnemyBattleUnit unit)
    {
        nameText.text = unit.enemyName;
        levelText.text = "Lvl " + unit.enemyLvl;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }
}
