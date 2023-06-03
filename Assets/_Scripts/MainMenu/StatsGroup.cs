using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsGroup : MonoBehaviour
{
    [SerializeField] private GameObject BoxGroup;
    [SerializeField] private Transform FirstStatsBoxPos;
    [SerializeField] private GameObject StatsBoxPrefab;
    [SerializeField] private GameObject UpgradeButton;
    [SerializeField] private TextMeshProUGUI UpgradeCost;

    [SerializeField] private List<string> BoxesHexColor;

    [SerializeField] private Color EnableColor; 
    [SerializeField] private Color DisabledColor;

    public const int BoxesCount = 10;
    public const float StatsBoxMargin = 15.0f;

    public Image[] Boxes { get; private set; }

    public int MainLevelsCount { get; set; } = 0;
    public int SubLevelCount { get; set; } = 0;

    private void Start()
    {
        Boxes = new Image[BoxesCount];
        for (int i = 0; i < BoxesCount; i++)
        {
            // Create boxes
            var newBox = Instantiate(StatsBoxPrefab, BoxGroup.transform);
            var transPoint = (new Vector3(FirstStatsBoxPos.transform.position.x, FirstStatsBoxPos.transform.position.y, 0));
            newBox.transform.position = new Vector2(transPoint.x + ((newBox.GetComponent<RectTransform>().rect.width * 2 + StatsBoxMargin) * i), transPoint.y);

            Boxes[i] = newBox.GetComponent<Image>();

            // Set box color
            if (i < SubLevelCount) Boxes[i].color = ConvertHexToColor(BoxesHexColor[MainLevelsCount + 1]);
            else Boxes[i].color = ConvertHexToColor(BoxesHexColor[MainLevelsCount]);
        }

        // Set Button Enabled
        SetEnabled(false);
    }

    public void UpdateBoxesColor()
    {
        for (int i = 0; i < BoxesCount; i++)
        {
            if (i < SubLevelCount) Boxes[i].color = ConvertHexToColor(BoxesHexColor[MainLevelsCount + 1]);
            else Boxes[i].color = ConvertHexToColor(BoxesHexColor[MainLevelsCount]);
        }
    }

    public void UpdateUpgradeCost(int value)
    {
        UpgradeCost.text = value.ToString();
    }

    public void IncreaseSubLevel()
    {
        SubLevelCount++;
        if(SubLevelCount >= BoxesCount)
        {
            SubLevelCount = 0;
            MainLevelsCount++;
        }

        // Update Color
        UpdateBoxesColor();
    }

    public void SetEnabled(bool enabled)
    {
        if (enabled)
        {
            UpgradeButton.GetComponent<Image>().color = EnableColor;
        }
        else
        {
            UpgradeButton.GetComponent<Image>().color = DisabledColor;
        }
    }

    private Color ConvertHexToColor(string hexColor)
    {
        ColorUtility.TryParseHtmlString(hexColor, out var color);
        if(color == null) color = Color.white;
        return color;
    }
}
