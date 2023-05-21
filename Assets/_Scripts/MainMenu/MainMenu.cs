using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Obejct in this main menu groub
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject MainMenuButton;

    [SerializeField] private GameObject MenuPanelGroub;
    [SerializeField] private GameObject StatsPanel;
    [SerializeField] private GameObject AchievementPanel;
    [SerializeField] private GameObject DictionaryPanel;

    // Public Variable
    public const float Slide_Duration = 0.1f;

    private void Start()
    {
        // Always close main menu first
        CloseMainMenu(0);
        // Got to stats panel first
        GoToMenuPanel(0);
    }

    // Open Close Main Menu ---------------------------------------------------------------------------
    private float MainMenuPanelHeight => MainMenuPanel.GetComponent<RectTransform>().rect.height;
    private float MainMenuButtonHeight => MainMenuButton.GetComponent<RectTransform>().rect.height;
    private bool m_mainMenuIsOpen = true;
    public void OpenMainMenu(float duration =  Slide_Duration)
    {
        if (m_mainMenuIsOpen) return;
        OpenCloseMainMenu(duration);
    }
    public void CloseMainMenu(float duration = Slide_Duration)
    {
        if (!m_mainMenuIsOpen) return;
        OpenCloseMainMenu(duration);
    }
    public void OpenCloseMainMenu(float duration)
    {
        m_mainMenuIsOpen = !m_mainMenuIsOpen;

        if(m_mainMenuIsOpen)
            MainMenuPanel.transform.DOMoveY(MainMenuPanel.transform.position.y + (MainMenuPanelHeight * 2) - MainMenuButtonHeight, duration);
        else
            MainMenuPanel.transform.DOMoveY(MainMenuPanel.transform.position.y - (MainMenuPanelHeight * 2) + MainMenuButtonHeight, duration);
    }

    // Slide Panel Menu -------------------------------------------------------------------------------
    private bool m_panelMenuIsMoving;
    private int m_panelMenuIndex = 0;
    private const float PanelMenuOffset = 20.0f;
    public void GoToMenuPanel(int index)
    {
        if (m_panelMenuIndex == index) return;
        m_panelMenuIsMoving = true;

        if(index < 0 || index > 2)
        {
            index = 0;
            if(m_panelMenuIndex == index)
            {
                m_panelMenuIsMoving = false;
                return;
            }
        }

        m_panelMenuIndex = index;
        StartCoroutine(StartGoToMenuPanel(index, Slide_Duration));
    }
    private IEnumerator StartGoToMenuPanel(int index, float duration)
    {
        Vector2 pointA, pointB;

        pointA = MenuPanelGroub.transform.position;
        pointB = new Vector2((-Screen.width * index) + (Screen.width / 2) - (PanelMenuOffset * (index + 1)), MenuPanelGroub.transform.position.y);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            MenuPanelGroub.transform.position = Vector3.Lerp(pointA, pointB, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the destination exactly
        MenuPanelGroub.transform.position = pointB;
        m_panelMenuIsMoving = false;
    }

}
