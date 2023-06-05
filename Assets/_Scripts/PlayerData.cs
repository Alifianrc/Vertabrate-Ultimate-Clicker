using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : StaticInstance<PlayerData>
{
    //Stats
    public int FinalDamage => Mathf.FloorToInt(attack * (Random.Range(0, 100) <= critChance ? critDamage : 1) * upgradeLevel);
    [SerializeField] private int attack;
    [SerializeField, Range(0, 100)] private float critChance;
    [SerializeField, Min(1)] private float critDamage;
    
    //Upgrades
    [SerializeField] private int upgradeLevel = 1;
    
    //Target prey
    private PreyUnitBase selectedPrey;
}
