using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] public GameObject DictionaryPanel;

    private void Start()
    {
        DictionaryPanel.SetActive(false);
    }
}
