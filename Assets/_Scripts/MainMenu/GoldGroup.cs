using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldGroup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGold;

    public void SetGoldValue(int value)
    {
        textGold.text = value.ToString();
    }
}
