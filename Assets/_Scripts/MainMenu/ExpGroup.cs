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

    protected override void Awake()
    {
        base.Awake();
        
        PlayerData.ExpLevelUpdate += PlayerDataOnExpLevelUpdate;
    }

    private void PlayerDataOnExpLevelUpdate(int currentExp, int maxExp, int level)
    {
        textExp.text = currentExp + "/" + maxExp;
        textLevel.text = "Lv." + level;
    }
}
