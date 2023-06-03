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

    [SerializeField] private Color EnabledColor;
    [SerializeField] private Color DisabledColor;

    public int RewardValue { get; private set; }

    public void SetData(JSONReader.AchevementData jData)
    {
        AchievementText.text = GetAchievemntDesc(jData.Type, jData.Threshold);
        ClaimButtonText.text = jData.Reward.ToString();
        RewardValue = jData.Reward;
        if (jData.State == AchievementState.OnGoing)
        {
            ClaimButtonImage.color = DisabledColor;
        }
        else
        {
            ClaimButtonText.color = EnabledColor;
        }
    }

    private string GetAchievemntDesc(AchievementType type, int threshold)
    {
        switch (type)
        {
            case AchievementType.KillFish:
                return "Kill " + threshold + " Fish";
                break;
            case AchievementType.ReachingLevel:
                return "Reach Level " + threshold;
                break;
            case AchievementType.DoDamage:
                return "Do " + threshold + " damage";
                break;
            default:
                return "Error";
        }
    }
}
