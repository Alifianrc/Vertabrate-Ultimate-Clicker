using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsGroup : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private GameObject BoxGroup;
    [SerializeField] private Transform FirstStatsBoxPos;
    [SerializeField] private GameObject StatsBoxPrefab;
    [SerializeField] private GameObject UpgradeButton;
    [SerializeField] private TMP_Text UpgradeCost;

    [SerializeField] private Color EnableColor; 
    [SerializeField] private Color DisabledColor;

    [SerializeField] private Color EnableTextColor;
    [SerializeField] private Color DisabledTextColor;

    [SerializeField] private Color[] BoxesHexColor;
    private ScriptableShopItem m_shopItemData;

    public const int BoxesCount = 10;
    public const float StatsBoxMargin = 15.0f;

    public Image[] Boxes { get; private set; }

    public int MainLevelsCount { get; private set; }
    public int SubLevelCount { get; private set; }

    public int Cost => Mathf.FloorToInt(m_shopItemData.CostMultiplier * (SubLevelCount + MainLevelsCount * BoxesCount) +
                                        m_shopItemData.BaseCost);

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
            if (i < SubLevelCount) Boxes[i].color = BoxesHexColor[MainLevelsCount + 1];
            else Boxes[i].color = BoxesHexColor[MainLevelsCount];
        }

        // Set Button Enabled
        SetEnabled(false);

        m_shopItemData = ResourceSystem.Instance.GetRandomShopItem();
        title.text = m_shopItemData.name;
        UpdateUpgradeCost();
    }

    public void UpdateBoxesColor()
    {
        for (int i = 0; i < BoxesCount; i++)
        {
            if (i < SubLevelCount) Boxes[i].color = BoxesHexColor[MainLevelsCount + 1];
            else Boxes[i].color = BoxesHexColor[MainLevelsCount];
        }
    }

    public void UpdateUpgradeCost()
    {
        UpgradeCost.text = Cost.ToString();
    }

    public void IncreaseSubLevel()
    {
        if(!PlayerData.Instance.TryReduceCoins(Cost)) return;
        
        SubLevelCount++;
        if(SubLevelCount >= BoxesCount)
        {
            SubLevelCount = 0;
            MainLevelsCount++;
        }

        // Update Color
        UpdateBoxesColor();
        UpdateUpgradeCost();
    }

    public void SetEnabled(bool enabled)
    {
        if (enabled)
        {
            UpgradeButton.GetComponent<Image>().color = EnableColor;
            UpgradeCost.color = EnableTextColor;
        }
        else
        {
            UpgradeButton.GetComponent<Image>().color = DisabledColor;
            UpgradeCost.color = DisabledTextColor;
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
