using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] public GameObject MainMenuPanel;
    [SerializeField] public GameObject DictionaryPanel;
    [SerializeField] public GameObject SettingPanel;
    [SerializeField] public GameObject HabitatPanel;

    [SerializeField] public Canvas MainCanvas;

    [SerializeField] public GameObject TextPopupPrefab;

    private void Start()
    {
        MainMenuPanel.SetActive(true);
        DictionaryPanel.SetActive(false);
        SettingPanel.SetActive(false);
        HabitatPanel.SetActive(false);
    }

    public void ShowPopupText(string text, Vector3 position)
    {
        var textObject = Instantiate(TextPopupPrefab, MainCanvas.transform);
        textObject.GetComponent<TextMeshProUGUI>().text = text;
        textObject.transform.position = position;
    }
}
