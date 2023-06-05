using DG.Tweening;
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

    public const float TransitionTime = 0.2f;

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

    public void SetActiveSettingPanel(bool isActive)
    {
        if ((SettingPanel.activeSelf && isActive) || 
            (!SettingPanel.activeSelf && !isActive)) return;

        if (SettingPanel.activeSelf)
        {
            SettingPanel.transform.localScale = Vector3.one;
            SettingPanel.transform.DOLocalMoveX(SettingPanel.GetComponent<RectTransform>().rect.width / -2, TransitionTime);
            SettingPanel.transform.DOLocalMoveY(SettingPanel.GetComponent<RectTransform>().rect.height / 2, TransitionTime);
            SettingPanel.transform.DOScale(Vector3.zero, TransitionTime).SetEase(Ease.OutQuad).OnComplete(() => SettingPanel.SetActive(false));
        }
        else
        {
            // Set Start Pos
            SettingPanel.transform.DOLocalMoveX(SettingPanel.GetComponent<RectTransform>().rect.width / -2, 0);
            SettingPanel.transform.DOLocalMoveY(SettingPanel.GetComponent<RectTransform>().rect.height / 2, 0);

            SettingPanel.transform.localScale = Vector3.zero;
            SettingPanel.SetActive(true);
            SettingPanel.transform.DOLocalMoveX(0, TransitionTime);
            SettingPanel.transform.DOLocalMoveY(0, TransitionTime);
            SettingPanel.transform.DOScale(Vector3.one, TransitionTime).SetEase(Ease.OutQuad);
        }
    }

    public void SetActiveHabitatPanel(bool isActive)
    {
        if ((HabitatPanel.activeSelf && isActive) ||
            (!HabitatPanel.activeSelf && !isActive)) return;

        if (HabitatPanel.activeSelf)
        {
            HabitatPanel.transform.localScale = Vector3.one;
            HabitatPanel.transform.DOLocalMoveX(HabitatPanel.GetComponent<RectTransform>().rect.width / 2, TransitionTime);
            HabitatPanel.transform.DOLocalMoveY(HabitatPanel.GetComponent<RectTransform>().rect.height / -2, TransitionTime);
            HabitatPanel.transform.DOScale(Vector3.zero, TransitionTime).SetEase(Ease.OutQuad).OnComplete(() => HabitatPanel.SetActive(false));
        }
        else
        {
            // Set Start Pos
            HabitatPanel.transform.DOLocalMoveX(HabitatPanel.GetComponent<RectTransform>().rect.width / 2, 0);
            HabitatPanel.transform.DOLocalMoveY(HabitatPanel.GetComponent<RectTransform>().rect.height / -2, 0);

            HabitatPanel.transform.localScale = Vector3.zero;
            HabitatPanel.SetActive(true);
            HabitatPanel.transform.DOLocalMoveX(0, TransitionTime);
            HabitatPanel.transform.DOLocalMoveY(0, TransitionTime);
            HabitatPanel.transform.DOScale(Vector3.one, TransitionTime).SetEase(Ease.OutQuad);
        }
    }

    public void SetActiveDictionaryPanel(bool isActive)
    {
        if ((DictionaryPanel.activeSelf && isActive) ||
            (!DictionaryPanel.activeSelf && !isActive)) return;

        if (DictionaryPanel.activeSelf)
        {
            //DictionaryPanel.transform.localScale = Vector3.one;
            //HabitatPanel.transform.DOLocalMoveX(DictionaryPanel.GetComponent<RectTransform>().rect.width / 2, TransitionTime);
            //DictionaryPanel.transform.DOLocalMoveY(DictionaryPanel.GetComponent<RectTransform>().rect.height / -2, TransitionTime);
            //DictionaryPanel.transform.DOScale(Vector3.zero, TransitionTime).SetEase(Ease.OutQuad).OnComplete(() => DictionaryPanel.SetActive(false));
            DictionaryPanel.SetActive(false);
        }
        else
        {
            // Set Start Pos
            //HabitatPanel.transform.DOLocalMoveX(DictionaryPanel.GetComponent<RectTransform>().rect.width / 2, 0);
            //DictionaryPanel.transform.DOLocalMoveY(DictionaryPanel.GetComponent<RectTransform>().rect.height / -2, 0);

            //DictionaryPanel.transform.localScale = Vector3.zero;
            //DictionaryPanel.SetActive(true);
            //DictionaryPanel.transform.DOLocalMoveX(0, TransitionTime);
            //DictionaryPanel.transform.DOLocalMoveY(0, TransitionTime);
            //DictionaryPanel.transform.DOScale(Vector3.one, TransitionTime).SetEase(Ease.OutQuad);
            DictionaryPanel.SetActive(true);
        }
    }
}
