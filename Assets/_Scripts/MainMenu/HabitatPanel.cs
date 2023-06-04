using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HabitatPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextSea;
    [SerializeField] private TextMeshProUGUI TextLand;
    [SerializeField] private TextMeshProUGUI TextSky;
    [SerializeField] private TextMeshProUGUI TextSpace;

    [SerializeField] private Image ImageSea;
    [SerializeField] private Image ImageLand;
    [SerializeField] private Image ImageSky;
    [SerializeField] private Image ImageSpace;

    [SerializeField] Color EnabledTextColor;
    [SerializeField] Color DisabledTextColor;

    [SerializeField] Color EnabledImageColor;
    [SerializeField] Color DisabledImageColor;

    private void Start()
    {
        TextLand.color = DisabledTextColor;
        TextSky.color = DisabledTextColor;
        TextSpace.color = DisabledTextColor;

        ImageLand.color = DisabledImageColor;
        ImageSky.color = DisabledImageColor;
        ImageSpace.color = DisabledImageColor;
    }
}
