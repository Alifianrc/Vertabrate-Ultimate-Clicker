using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using NoSuchStudio.Common;
using UnityEngine;
using UnityEngine.Serialization;
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
    private Timer _timer;
    public Sprite CurrentSprite => sprites[CurrentSpriteIndex];
    public Sprite RandomSprite => sprites[Random.Range(0, sprites.Count)];
    public bool IsPlaying { get; private set; }
    public event Action<Sprite> OnSpriteIndexChanged;

    public void Play(bool fromBeginning = false)
    {
        Log("Play");
        IsPlaying = true;
        if(fromBeginning) Seek(0);
        _timer = Helpers.RunEvery(NextFrame, 1000 / frameRate);
    }

    public void Stop()
    {
        _timer?.Stop();
        _timer?.Dispose();
        IsPlaying = false;
    }

    public void Seek(int spriteIndex)
    {
        if (spriteIndex < 0 || spriteIndex >= sprites.Count)
            throw new IndexOutOfRangeException("spriteIndex doesn't exist");
        if(CurrentSpriteIndex == spriteIndex) return;
        CurrentSpriteIndex = spriteIndex;
        OnSpriteIndexChanged?.Invoke(CurrentSprite);
        Log($"Seek to {CurrentSpriteIndex}");
    }

    private void NextFrame()
    {
        CurrentSpriteIndex++;
        if (CurrentSpriteIndex == sprites.Count)
        {
            Seek(loop ? 0 : sprites.Count - 1);
        }
    }

    private void OnDestroy()
    {
        Stop();
    }

    private void OnDisable()
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
public struct AnimationSheet
{
    public AnimationType Type;
    public ScriptableSpriteSheet SpriteSheet;
}