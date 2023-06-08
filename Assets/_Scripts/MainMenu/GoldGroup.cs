using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldGroup : Singleton<GoldGroup>
{
    [SerializeField] private TextMeshProUGUI textGold;

    protected override void Awake()
    {
        base.Awake();

        PlayerData.GoldUpdate += SetGoldValue;
    }

    private void SetGoldValue(int value)
    {
        textGold.text = value.ToString();
    }
}
