using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Shop Item")]
public class ScriptableShopItem : ScriptableObject
{
    [field: SerializeField] public int BaseCost { get; private set; }
    [field: SerializeField] public float CostMultiplier { get; private set; }
    [field: SerializeField] public UnityEvent WhatToUpgrade { get; private set; }
}
