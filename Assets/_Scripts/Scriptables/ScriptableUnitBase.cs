using System;
using UnityEngine;

/// <summary>
/// Keeping all relevant information about a unit on a scriptable means we can gather and show
/// info on the menu screen, without instantiating the unit prefab.
/// </summary>
public abstract class ScriptableUnitBase : ScriptableObject
{
    [SerializeField] private ScriptableSpriteSheet moveAnimation;
    [SerializeField] private ScriptableSpriteSheet idleAnimation;
    [SerializeField] private ScriptableSpriteSheet hurtAnimation;
    [SerializeField] private Stats _stats;
    [SerializeField, Min(1)] private float healthMultiplier;
    public Stats BaseStats => _stats;

    // Used in game
    public PreyUnitBase Prefab;
    
    // Used in menus
    public string Description;
    public Sprite MenuSprite;
}

/// <summary>
/// Keeping base stats as a struct on the scriptable keeps it flexible and easily editable.
/// We can pass this struct to the spawned prefab unit and alter them depending on conditions.
/// </summary>
[Serializable]
public struct Stats {
    public int Health;
    public int EscapeTime;
}