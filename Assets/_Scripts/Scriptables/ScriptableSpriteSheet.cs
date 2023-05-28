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
    [SerializeField, Range(0, 60)] private int frameRate = 24;
    [SerializeField] private bool loop;

    public int CurrentSpriteIndex { get; private set; }
    //private Timer _timer;
    public Sprite CurrentSprite => sprites[CurrentSpriteIndex];
    public Sprite RandomSprite => sprites[Random.Range(0, sprites.Count)];
    public bool IsPlaying { get; private set; }
    public event Action<Sprite> OnSpriteIndexChanged;

    public void Play(bool fromBeginning = false)
    {
        Log("Play " + name);
        IsPlaying = true;
        if(fromBeginning) Seek(0);
        MonoHelper.Instance.InvokeRepeat(NextFrame, 1f / frameRate);
    }

    public void Stop()
    {
        Log("Stop " + name);
        MonoHelper.Instance.StopAll();
        IsPlaying = false;
    }

    public void Seek(int spriteIndex)
    {
        //Log("start seek" + spriteIndex + IsIndexInValid(spriteIndex));
        if (IsIndexInValid(spriteIndex))
        {
            throw new IndexOutOfRangeException("spriteIndex doesn't exist! filename:" + name);
            // LogError("spriteIndex doesn't exist");
            // return;
        }

        if (CurrentSpriteIndex == spriteIndex) return;
        CurrentSpriteIndex = spriteIndex;
        //Log($"Seek to {CurrentSpriteIndex}");
    }

    private bool IsIndexInValid(int spriteIndex)
    {
        return spriteIndex < 0 || spriteIndex >= sprites.Count;
    }

    private void NextFrame()
    {
        var prev = CurrentSpriteIndex;
        CurrentSpriteIndex++;
        if (IsIndexInValid(CurrentSpriteIndex))
        {
            Seek(loop ? 0 : sprites.Count - 1);
        }
        
        if(prev != CurrentSpriteIndex) OnSpriteIndexChanged?.Invoke(CurrentSprite);
    }

    private void OnDestroy()
    {
        Stop();
    }
}

[Serializable]
public enum AnimationType
{
    Invalid, Idle, Move, Hurt, Death
}

[Serializable]
public struct AnimationSheet : IEquatable<AnimationSheet>
{
    public AnimationType Type;
    public ScriptableSpriteSheet SpriteSheet;

    public static bool operator ==(AnimationSheet a, AnimationSheet b)
    {
        return a.Type == b.Type && a.SpriteSheet == b.SpriteSheet;
    }

    public static bool operator !=(AnimationSheet a, AnimationSheet b)
    {
        return !(a == b);
    }

    public bool Equals(AnimationSheet other)
    {
        return Type == other.Type && Equals(SpriteSheet, other.SpriteSheet);
    }

    public override bool Equals(object obj)
    {
        return obj is AnimationSheet other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Type, SpriteSheet);
    }
}