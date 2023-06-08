using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerData : StaticInstance<PlayerData>
{
    private int FinalDamage =>
        Mathf.FloorToInt(attack * Crit(critChance, critMultiplier) * upgradeLevel);

    [Header("Damage Setting")]
    [SerializeField] private int attack;
    [SerializeField, Range(0, 100)] private float critChance;
    [SerializeField, Min(1)] private float critMultiplier;
    [SerializeField] private int upgradeLevel = 1;

    [SerializeField] private Vector3 cameraOffset;
    private UnitBase m_selectedPrey;

    private int m_exp;
    private int m_levelUpRequirement = 3;
    private int m_level;
    private int m_coins;

    public static event Action LevelUp;
    public static event Action<int, int, int> ExpLevelUpdate;
    public static event Action<int> GoldUpdate;

    private void Start()
    {
        ExpLevelUpdate?.Invoke(m_exp, m_levelUpRequirement, m_level);
        AddCoins(0);
    }

    private void Update()
    {
        if(m_selectedPrey != null) CameraMovement.Instance.PanCamera(m_selectedPrey.transform.position + cameraOffset);
    }

    public void OnClickPrey(UnitBase unit)
    {
        if (m_selectedPrey == null)
        {
            m_selectedPrey = unit;
            PreyManager.Instance.SetAllVisibility(false);
            unit.StopMovement();
            unit.SetVisible(true);
            unit.Dead += PreyOnDead;
            CameraMovement.Instance.ZoomIn();
        }
        else if (m_selectedPrey == unit)
        {
            unit.TakeDamage(FinalDamage);
        }
    }

    private void PreyOnDead(UnitBase unit)
    {
        PreyManager.Instance.SetAllVisibility(true);
        CameraMovement.Instance.ZoomOut();
        AddExp(unit.Stats.exp);
        AddCoins(unit.Stats.coin);
        m_selectedPrey = null;
        Destroy(unit.gameObject);
    }

    private void AddExp(int exp)
    {
        var initialLevel = m_level;
        m_exp += exp;
        while (m_exp >= m_levelUpRequirement)
        {
            m_exp -= m_levelUpRequirement;
            m_level++;
            m_levelUpRequirement = m_level * 3 + 3;
        }
        if(m_level != initialLevel)
        {
            AddCoins(50 * m_level);
            LevelUp?.Invoke();
        }
        ExpLevelUpdate?.Invoke(m_exp, m_levelUpRequirement, m_level);
    }

    private static float Crit(float chance, float multiplier)
    {
        if (Random.Range(0, 100) <= chance)
        {
            //Do stuff on crit
            return multiplier;
        }
        return 1;
    }

    public static void UpgradeAttack()
    {
        Instance.upgradeLevel++;
    }

    public bool TryReduceCoins(int cost)
    {
        if (m_coins < cost) return false;
        
        m_coins -= cost;
        return true;
    }

    private void AddCoins(int amount)
    {
        m_coins += amount;
        GoldUpdate?.Invoke(m_coins);
    }
}
