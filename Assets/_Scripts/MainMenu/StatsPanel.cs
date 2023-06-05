using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : Singleton<StatsPanel>
{
    [SerializeField] private GameObject StatsGroupPrefrab;
    [SerializeField] private GameObject Content;

    private float StartWidth = 0;
    private Action<float> OnContentWidthChanged;

    protected override void Awake()
    {
        base.Awake();
        JSONReader.OnJDataLoaded += OnJDataLoaded;
    }

    private void OnJDataLoaded()
    {
        JSONReader.OnJDataLoaded -= OnJDataLoaded;

        for (int i = 0; i < 5; i++)
        {
            var newGroup = Instantiate(StatsGroupPrefrab, Content.transform);
            var stats = newGroup.GetComponent<StatsGroup>();
            OnContentWidthChanged += stats.SetWidth;
        }
    }

    private void Update()
    {
        var contentWidth = Content.GetComponent<RectTransform>().rect.width;
        if (StartWidth != contentWidth)
        {
            OnContentWidthChanged?.Invoke(contentWidth);
            StartWidth = contentWidth;
        }
    }
}
