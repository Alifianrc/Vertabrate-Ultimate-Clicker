using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] public GameObject DictionaryPanel;
    [SerializeField] public GameObject SettingPanel;
    [SerializeField] public GameObject HabitatPanel;

    private void Start()
    {
        DictionaryPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HabitatPanel.SetActive(false);
    }
}
