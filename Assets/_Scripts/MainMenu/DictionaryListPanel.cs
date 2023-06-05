using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryListPanel : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject DictionaryGroupPrefab;

    private float StartWidth = 0;
    private Action<float> OnContentWidthChanged;

    private void Awake()
    {
        JSONReader.OnJDataLoaded += OnJDataLoaded;
    }

    public void OnJDataLoaded()
    {
        JSONReader.OnJDataLoaded -= OnJDataLoaded;
        var dictionaryData = JSONReader.Instance.Vertebrate_Data_List.List;
        var groupCount = (int)Math.Ceiling((double)dictionaryData.Length / DictionaryGroup.Length);
        for (int i = 0; i < groupCount; i++)
        {
            var newGroup = Instantiate(DictionaryGroupPrefab, Content.transform);
            var dGroup = newGroup.GetComponent<DictionaryGroup>();
            OnContentWidthChanged += dGroup.SetWidth;
            for(int j = 0; j < DictionaryGroup.Length; j++)
            {
                var index = j + (i * DictionaryGroup.Length);
                if (index < dictionaryData.Length)
                {
                    dGroup.DictionaryButton[j].GetComponent<Button>().onClick.AddListener(() => OpenDictionary(dictionaryData[index].Name));
                    dGroup.DictionaryImage[j].sprite = JSONReader.Instance.GetVertebrateImage(dictionaryData[index].Name);
                }
                else
                {
                    dGroup.DictionaryButton[j].SetActive(false);
                }
            }
        }
    }

    public void OpenDictionary(string name)
    {
        Debug.Log(name);
        var vertebrateData = JSONReader.Instance.GetVertebrateData(name);
        if (vertebrateData == null) return;

        MenuManager.Instance.SetActiveDictionaryPanel(true);
        MenuManager.Instance.DictionaryPanel.GetComponent<DictionaryPanel>().SetData(vertebrateData);
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
