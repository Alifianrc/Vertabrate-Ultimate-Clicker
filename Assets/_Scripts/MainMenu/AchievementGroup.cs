using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementGroup : MonoBehaviour
{
    [SerializeField] private string NameID;
    [SerializeField] private TextMeshProUGUI AchievementText;
    [SerializeField] private TextMeshProUGUI ClaimButtonText;
    [SerializeField] private Image ClaimButtonImage;
    [SerializeField] private Image GoldImage;

    [SerializeField] private Color EnabledColor;
    [SerializeField] private Color DisabledColor;

    [SerializeField] private Color EnabledColorText;
    [SerializeField] private Color DisabledColorText;

    [SerializeField] private Color EnabledGoldColor;
    [SerializeField] private Color DisabledGoldColor;

    public int RewardValue { get; private set; }


    public void SetData(JSONReader.AchevementData jData)
    {
        AchievementText.text = GetAchievemntDesc(jData.Type, jData.Threshold);
        ClaimButtonText.text = jData.Reward.ToString();
        RewardValue = jData.Reward;
        if (jData.State == AchievementState.OnGoing)
        {
            ClaimButtonImage.color = DisabledColor;
            ClaimButtonText.color = DisabledColorText;
            GoldImage.color = DisabledGoldColor;
        }
        else
        {
            ClaimButtonImage.color = EnabledColor;
            ClaimButtonText.color = EnabledColorText;
            GoldImage.color = EnabledGoldColor;
        }
    }

    private string GetAchievemntDesc(AchievementType type, int threshold)
    {
        switch (type)
        {
            case AchievementType.KillFish:
                return "Kill " + threshold + " Fish";
            case AchievementType.ReachingLevel:
                return "Reach Level " + threshold;
            case AchievementType.DoDamage:
                return "Do " + threshold + " damage";
            default:
                return "Error";
        }
    }

    public void SetWidth(float width)
    {
        var rectTransform = GetComponent<RectTransform>();
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.x = width;
        rectTransform.sizeDelta = sizeDelta;
    }
}
