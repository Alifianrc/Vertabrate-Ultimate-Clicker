using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpGroup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textExp;
    [SerializeField] private Slider slider;

    private int currentExp;
    private int maxExp;

    public void SetLevel(int level)
    {
        textLevel.text = "Lv." + level.ToString();
    }
    public void SetExp(int value)
    {
        currentExp = value;
        RefreshText();
    }
    public void SetMaxExp(int value)
    {
        maxExp = value;
        RefreshText();
    }
    private void RefreshText()
    {
        textExp.text = currentExp.ToString() + "/" + maxExp.ToString();
    }
}
