using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    // Verifica alterações nos dados e passa para UI
    public void setHUD(EnemyBattleUnit unit)
    {
        nameText.text = unit.enemyName;
        levelText.text = "Lvl " + unit.enemyLvl;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void setHp(int hp)
    {
        hpSlider.value = hp;
    }
}
