using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanel : Singleton<AchievementPanel>
{
    [SerializeField] private GameObject AchievementGroupPrefrab;
    [SerializeField] private GameObject Content;

    private void Awake()
    {
        base.Awake();
        JSONReader.OnJDataLoaded += OnJDataLoaded;
    }

    private void OnJDataLoaded()
    {
        JSONReader.OnJDataLoaded -= OnJDataLoaded;
        var achievementData = JSONReader.Instance.Achievement_Data_List.List;

        foreach(var achievement in achievementData)
        {
            var newGroup = Instantiate(AchievementGroupPrefrab, Content.transform);
            var achiev = newGroup.GetComponent<AchievementGroup>();
            achiev.SetData(achievement);
        }
    }
}
