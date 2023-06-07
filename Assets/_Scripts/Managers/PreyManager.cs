using System;
using System.Collections;
using Kit;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// An example of a scene-specific manager grabbing resources from the resource system
/// Scene-specific managers are things like grid managers, unit managers, environment managers etc
/// </summary>
public class PreyManager : StaticInstance<PreyManager>
{
    [SerializeField] private Bounds m_spawnArea;
    [SerializeField] private int m_preyCount;

    public Bounds SpawnArea => m_spawnArea;
    
    public void PopulateArea() {
        for (int i = 0; i < m_preyCount; i++)
        {
            SpawnRandomUnit(RandomPositionWithin(m_spawnArea));
        }
    }

    private void SpawnRandomUnit(Vector2 position)
    {
        var data = ResourceSystem.Instance.GetRandomPrey();
        InstantiateUnit(data, position);
    }

    private void SpawnUnit(PreyType t, Vector2 position)
    {
        var data = ResourceSystem.Instance.GetPrey(t);
        InstantiateUnit(data, position);
    }

    private void InstantiateUnit(ScriptablePrey data, Vector2 pos)
    {
        if (data == null || data.Prefab == null) throw new ArgumentException("Prey data is null or there is no prefab assigned!");
        var spawned = Instantiate(data.Prefab, pos, Quaternion.identity, transform);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = data.BaseStats;
        stats.Health += 20;
        stats.EscapeTime += 5;

        spawned.Init(stats, data.Animations);
    }

    public Vector2 RandomPositionWithin() => RandomPositionWithin(m_spawnArea);

    private static Vector2 RandomPositionWithin(Bounds area)
    {
        var randX = Random.Range(area.min.x, area.max.x);
        var randY = Random.Range(area.min.y, area.max.y);

        return new(randX, randY);
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtend.DrawBounds(m_spawnArea, Color.green);
    }
}