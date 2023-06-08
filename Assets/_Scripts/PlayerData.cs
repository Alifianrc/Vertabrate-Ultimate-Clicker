using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : StaticInstance<PlayerData>
{
    //Stats
    public int FinalDamage =>
        Mathf.FloorToInt(attack * Crit(critChance, critMultiplier) * upgradeLevel);

    [SerializeField] private int attack;
    [SerializeField, Range(0, 100)] private float critChance;
    [SerializeField, Min(1)] private float critMultiplier;
    
    //Upgrades
    [SerializeField] private int upgradeLevel = 1;
    
    //Target prey
    private UnitBase m_selectedPrey;

    private int m_exp;
    private int m_levelUpRequirement;
    private int m_level;

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
            CameraMovement.Instance.PanCamera(unit.transform.position);
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
        m_selectedPrey = null;
        Destroy(unit.gameObject);
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
}
