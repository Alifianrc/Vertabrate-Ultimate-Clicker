using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchPopupText : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string TextToPop;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Get the touch position
        Vector2 touchPosition = eventData.position;

        // Show Text
        MenuManager.Instance.ShowPopupText(TextToPop, touchPosition);
    }
}
