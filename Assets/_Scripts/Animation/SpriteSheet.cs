using System;
using NoSuchStudio.Common;
using UnityEngine;

[Serializable]
public class SpriteSheet : ClassWithLogger//, IEquatable<SpriteSheet>
{
    public ScriptableSpriteSheet Data { get; }
    public bool Loop => Data.Loop;
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
            throw new IndexOutOfRangeException($"spriteIndex {spriteIndex} doesn't exist! filename: {Data.name}");
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

    public override string ToString()
    {
        return $"{Data.name} (SpriteSheet)";
    }

    // public bool Equals(SpriteSheet other)
    // {
    //     if (ReferenceEquals(null, other)) return false;
    //     if (ReferenceEquals(this, other)) return true;
    //     return Equals(Data, other.Data);
    // }
    //
    // public override bool Equals(object obj)
    // {
    //     if (ReferenceEquals(null, obj)) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != this.GetType()) return false;
    //     return Equals((SpriteSheet)obj);
    // }
    //
    // public override int GetHashCode()
    // {
    //     return (Data != null ? Data.GetHashCode() : 0);
    // }
}
