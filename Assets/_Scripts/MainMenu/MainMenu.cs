using DG.Tweening;
using NoSuchStudio.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourWithLogger
{
    // Object in this main menu group
    [SerializeField] private RectTransform mainMenuPanel;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private GameObject mainMenuButtonTriangle;

    [SerializeField] private RectTransform menuPanelGroup;
    [SerializeField] private RectTransform statsPanel;
    [SerializeField] private RectTransform achievementPanel;
    [SerializeField] private RectTransform dictionaryListPanel;

    // Public Variable
    public const float Slide_Duration = 0.2f;

    private void Start()
    {
        // Always close main menu first
        CloseMainMenu(0);
        // Got to stats panel first
        GoToMenuPanel(0);
    }

    // Open Close Main Menu ---------------------------------------------------------------------------
    private float MainMenuPanelHeight => mainMenuPanel.rect.height;
    //private float MainMenuButtonHeight => mainMenuButton.GetComponent<RectTransform>().rect.height;
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

        mainMenuButtonTriangle.transform.DORotate(new Vector3(0, 0, m_mainMenuIsOpen ? 0 : 180), 0);

        var targetY = m_mainMenuIsOpen
            ? mainMenuPanel.position.y + MainMenuPanelHeight * 2 + 20// - MainMenuButtonHeight
            : mainMenuPanel.position.y - MainMenuPanelHeight * 2 - 20; // + MainMenuButtonHeight;
        
        mainMenuPanel.DOMoveY(targetY, duration);
    }

    // Slide Panel Menu -------------------------------------------------------------------------------
    private float SubMenuPanelWidth => statsPanel.rect.width;
    private const float SubMenuPanelMargin = 20.0f;
    private int m_subMenuPanelIndex = 0;
    public void GoToMenuPanel(int index)
    {
        // Checking index value
        if (m_subMenuPanelIndex == index) return;
        if(index < 0 || index > 2)
        {
            index = 0;
            if(m_subMenuPanelIndex == index)
            {
                return;
            }
        }
        m_subMenuPanelIndex = index;
        
        // Translate rectTransform to transform
        var xRectPos = -(SubMenuPanelWidth + SubMenuPanelMargin) * index;
        var transformPosition = transform.TransformPoint(new Vector3(xRectPos, 0, 0));
        menuPanelGroup.transform.DOMoveX(transformPosition.x, Slide_Duration);

        // Set Selected Button
        MainMenuBottomButtonGroup.Instance.SetSelectedButton(index);
    }
}
