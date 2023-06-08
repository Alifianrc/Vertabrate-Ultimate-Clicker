using System;
using System.Collections.Generic;
using NoSuchStudio.Common;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A class in which you can set a set of sprites as an animation.
/// The animation can be played and stopped. Use CurrentSprite to get the current frame
/// </summary>
[CreateAssetMenu(fileName = "New SpriteSheet")]
public class ScriptableSpriteSheet : ScriptableObjectWithLogger
{
    [SerializeField] private List<Sprite> sprites = new();

    [field: SerializeField]
    [field: Range(0, 60)]
    public int FrameRate { get; private set; } = 24;
    
    [field: SerializeField] public bool Loop { get; private set; }
    
    public Sprite RandomSprite => sprites[Random.Range(0, sprites.Count)];

    public int MaxIndex => sprites.Count - 1;
    
    public bool IsIndexInValid(int spriteIndex) => spriteIndex < 0 || spriteIndex >= sprites.Count;

    public Sprite this[int i]
    {
        get
        {
            if(IsIndexInValid(i)) throw new IndexOutOfRangeException("spriteIndex doesn't exist! filename:" + name);
            return sprites[i];
        }
    }

    public override string ToString()
    {
        return name;
    }
}

[Serializable]
public enum AnimationType
{
    Invalid, Idle, Move, Hurt, Death
}