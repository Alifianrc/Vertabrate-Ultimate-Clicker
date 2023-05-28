using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementGroup : MonoBehaviour
{
    [SerializeField] private GameObject ClaimButton;
    [SerializeField] private TextMeshProUGUI AchievementText;

    public int RewardValue { get; private set; }

    public void SetAchievementText(string text)
    {
        AchievementText.text = text;
    }
    public void SetRewardValue(int value) 
    { 
        RewardValue = value;
    }
}
