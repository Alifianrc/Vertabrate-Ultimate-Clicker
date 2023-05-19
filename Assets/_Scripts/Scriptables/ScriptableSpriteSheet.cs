using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A class in which you can set a set of sprites as an animation.
/// The animation can be played and stopped. Use CurrentSprite to get the current frame
/// </summary>
[CreateAssetMenu(fileName = "New SpriteSheet")]
public class ScriptableSpriteSheet : ScriptableObject
{
    [SerializeField] private List<Sprite> sprites = new();
    [SerializeField, Range(0, 60)] private int frameRate = 24;
    [SerializeField] private bool loop;

    public int CurrentSpriteIndex { get; private set; }
    private Timer _timer;
    public Sprite CurrentSprite => sprites[CurrentSpriteIndex];
    public Sprite RandomSprite => sprites[Random.Range(0, sprites.Count)];
    public bool IsPlaying { get; private set; }

    public void Play()
    {
        IsPlaying = true;
        _timer = Helpers.RunEvery(NextFrame, 1000 / frameRate);
    }

    public void Stop()
    {
        _timer.Stop();
        _timer.Dispose();
        IsPlaying = false;
    }

    public void Seek(int spriteIndex)
    {
        if (spriteIndex < 0 || spriteIndex >= sprites.Count)
            throw new IndexOutOfRangeException("spriteIndex doesn't exist");
        CurrentSpriteIndex = spriteIndex;
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
