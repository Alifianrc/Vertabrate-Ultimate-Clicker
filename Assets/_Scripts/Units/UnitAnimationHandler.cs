using System;
using System.Collections.Generic;
using System.Timers;
using NoSuchStudio.Common;
using UnityEngine;

[Serializable]
internal class UnitAnimationHandler : ClassWithLogger
{
    private List<AnimationSheet> m_animations;

    [SerializeField, ShowOnly] private ScriptableSpriteSheet m_selectedAnimation;
    
    private readonly SpriteRenderer m_spriteRenderer;

    [SerializeField, ShowOnly] private Sprite m_currentSprite;

    public UnitAnimationHandler(List<AnimationSheet> animations, SpriteRenderer renderer)
    {
        m_animations = animations;
        m_spriteRenderer = renderer;
    }
        
    public void Play(AnimationType type, bool fromBeginning = false)
    {
        var anim = GetAnimation(type);
        SelectAnimation(anim);
        anim.Play(fromBeginning);
    }

    /// <summary>
    /// Stops the currently playing animation
    /// </summary>
    public void Stop()
    {
        if(m_selectedAnimation == null) return;
        m_selectedAnimation.Stop();
    }

    private void ChangeSprite(Sprite newSprite)
    {
        Debug.Log("Change sprite to " + newSprite);
        m_spriteRenderer.sprite = newSprite;
        m_currentSprite = newSprite;
    }

    private void SelectAnimation(ScriptableSpriteSheet animation)
    {
        DeselectAnimation();

        m_selectedAnimation = animation;
        m_selectedAnimation.OnSpriteIndexChanged += ChangeSprite;
    }

    private void DeselectAnimation()
    {
        if (m_selectedAnimation != null)
        {
            m_selectedAnimation.OnSpriteIndexChanged -= ChangeSprite;
            m_selectedAnimation = null;
        }
    }

    private ScriptableSpriteSheet GetAnimation(AnimationType type)
    {
        foreach (var anim in m_animations)
        {
            if (anim.Type == type)
            {
                return anim.SpriteSheet;
            }
        }

        //return null;
        throw new Exception($"Animation with type = {type} not found");
    }
}