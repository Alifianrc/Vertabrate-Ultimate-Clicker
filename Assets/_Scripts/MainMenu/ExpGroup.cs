using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpGroup : Singleton<ExpGroup>
{
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textExp;
    [SerializeField] private Slider slider;

    public void SetLevel(int level)
    {
        textLevel.text = "Lv." + level;
    }
    public void SetExp(int currentExp, int maxExp)
    {
        textExp.text = currentExp + "/" + maxExp;
    }
}
