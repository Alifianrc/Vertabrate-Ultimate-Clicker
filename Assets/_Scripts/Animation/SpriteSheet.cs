using System;
using System.Collections;
using System.Collections.Generic;
using NoSuchStudio.Common;
using UnityEngine;

public class SpriteSheet : ClassWithLogger
{
    public ScriptableSpriteSheet Data { get; }
    [field: SerializeField] public bool Loop { get; private set; }
    public int CurrentSpriteIndex { get; private set; }
    public Sprite CurrentSprite => Data[CurrentSpriteIndex];
    public Sprite RandomSprite => Data.RandomSprite;
    public bool IsPlaying { get; private set; }
    public event Action<Sprite> OnSpriteIndexChanged;
    
    public SpriteSheet(ScriptableSpriteSheet data)
    {
        Data = data;
    }

    public void Play(bool fromBeginning = false)
    {
        Log("Play " + Data.name);
        IsPlaying = true;
        if(fromBeginning) Seek(0);
        MonoHelper.Instance.RunRepeat(NextFrame, 1f / Data.FrameRate);
    }

    public void Stop()
    {
        Log("Stop " + Data.name);
        MonoHelper.Instance.StopAll();
        IsPlaying = false;
    }

    public void Seek(int spriteIndex)
    {
        //Log("start seek" + spriteIndex + IsIndexInValid(spriteIndex));
        if (Data.IsIndexInValid(spriteIndex))
        {
            throw new IndexOutOfRangeException("spriteIndex doesn't exist! filename:" + Data.name);
            // LogError("spriteIndex doesn't exist");
            // return;
        }

        if (CurrentSpriteIndex == spriteIndex) return;
        CurrentSpriteIndex = spriteIndex;
        //Log($"Seek to {CurrentSpriteIndex}");
    }

    private void NextFrame()
    {
        var prev = CurrentSpriteIndex;
        CurrentSpriteIndex++;
        if (Data.IsIndexInValid(CurrentSpriteIndex))
        {
            Seek(Loop ? 0 : Data.MaxIndex);
        }
        
        if(prev != CurrentSpriteIndex) OnSpriteIndexChanged?.Invoke(CurrentSprite);
    }
}
