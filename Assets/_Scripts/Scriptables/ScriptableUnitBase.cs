using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Keeping all relevant information about a unit on a scriptable means we can gather and show
/// info on the menu screen, without instantiating the unit prefab.
/// </summary>
public abstract class ScriptableUnitBase : ScriptableObject
{
    [SerializeField] private List<AnimationSheet> m_animations;
    [SerializeField] private Stats m_stats;
    [SerializeField, Min(1)] private float m_healthMultiplier = 1;
    public Stats BaseStats => m_stats;
    public List<AnimationSheet> Animations => m_animations;

    // Used in game
    public PreyUnitBase Prefab;
    
    // Used in menus
    public string Description;
    public Sprite MenuSprite;

    private void OnValidate()
    {
        foreach (var sheet in m_animations)
        {
            if (m_animations.Any(other => sheet != other && sheet.Type == other.Type))
            {
                throw new Exception($"Duplicates found! File:{name}. Duplicates:{sheet.Type}");
            }
        }
    }
}

/// <summary>
/// Keeping base stats as a struct on the scriptable keeps it flexible and easily editable.
/// We can pass this struct to the spawned prefab unit and alter them depending on conditions.
/// </summary>
[Serializable]
public struct Stats {
    public int Health;
    public int EscapeTime;
    public float Speed;
}