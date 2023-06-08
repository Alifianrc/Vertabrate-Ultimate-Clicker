using System;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NoSuchStudio.Common;
using UnityEngine;

[Serializable]
internal class UnitAnimationHandler : ClassWithLogger
{
    private List<AnimationSheet> m_animations;
    private SpriteSheet m_selectedAnimation;
    private readonly SpriteRenderer m_spriteRenderer;
    private Sprite m_currentSprite;
    private Tween m_colorTween;
    private bool m_isDisabled;

    public UnitAnimationHandler(List<AnimationSheet> animations, SpriteRenderer renderer)
    {
        m_animations = animations;
        m_spriteRenderer = renderer;
    }

    public void Disable() => m_isDisabled = true;
    public void Enable() => m_isDisabled = false;

    public void ChangeColorTemporary(Color initialColor, Color newColor, float time)
    {
        if (m_isDisabled) return;
        m_colorTween?.Kill();
        m_spriteRenderer.color = newColor;
        m_colorTween = m_spriteRenderer.DOColor(initialColor, time);
    }

    public void SetVisible(float alpha)
    {
        if (m_isDisabled) return;
        m_spriteRenderer.DOFade(alpha, 1);
    }
        
    public void Play(AnimationType type, bool fromBeginning = false)
    {
        var anim = GetAnimation(type);
        SelectAnimation(anim);
        anim.Play(fromBeginning);
    }

    public void PlayTemporary(AnimationType type, float time, bool fromBeginning = false)
    {
        var current = GetType(m_selectedAnimation);
        if(current == type) return;
        Play(type, fromBeginning);
        MonoHelper.Instance.Run(() => Play(current, fromBeginning), time);
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
        if (m_isDisabled) return;
        //Log("Change sprite to " + newSprite);
        m_spriteRenderer.sprite = newSprite;
        m_currentSprite = newSprite;
    }

    private void SelectAnimation(SpriteSheet animation)
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

    private SpriteSheet GetAnimation(AnimationType type)
    {
        foreach (var anim in m_animations)
        {
            if (anim.type == type)
            {
                return anim.Sheet;
            }
        }

        //return null;
        throw new($"Animation with type = {type} not found");
    }

    private AnimationType GetType(SpriteSheet spriteSheet)
    {
        foreach (var anim in m_animations)
        {
            if (anim.SheetData == spriteSheet.Data)
            {
                return anim.type;
            }
        }

        throw new($"Animation {spriteSheet} not found");
    }
}