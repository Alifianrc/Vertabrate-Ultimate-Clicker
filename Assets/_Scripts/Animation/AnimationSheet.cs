using System;
using UnityEngine;

[Serializable]
public struct AnimationSheet : IEquatable<AnimationSheet>
{
    public AnimationType type;
    [SerializeField] private ScriptableSpriteSheet spriteSheetData;
    private SpriteSheet m_spriteSheet;

    public SpriteSheet Sheet => m_spriteSheet ??= new(spriteSheetData);
    public ScriptableSpriteSheet SheetData => spriteSheetData;

    public static bool operator ==(AnimationSheet a, AnimationSheet b)
    {
        return a.type == b.type && a.spriteSheetData == b.spriteSheetData;
    }

    public static bool operator !=(AnimationSheet a, AnimationSheet b)
    {
        return !(a == b);
    }

    public bool Equals(AnimationSheet other)
    {
        return type == other.type && Equals(spriteSheetData, other.spriteSheetData);
    }

    public override bool Equals(object obj)
    {
        return obj is AnimationSheet other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)type, spriteSheetData);
    }
}