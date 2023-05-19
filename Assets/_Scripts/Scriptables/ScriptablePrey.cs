using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Create a scriptable hero 
/// </summary>
[CreateAssetMenu(fileName = "New Prey")]
public class ScriptablePrey : ScriptableUnitBase {
    public PreyType Type;
    
}

[Serializable]
public enum PreyType {
    Tarodev = 0,
    Snorlax = 1
}

