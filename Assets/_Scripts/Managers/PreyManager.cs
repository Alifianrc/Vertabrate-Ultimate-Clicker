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

    private void InstantiateUnit(ScriptablePrey data, Vector2 pos) {

        var spawned = Instantiate(data.Prefab, pos, Quaternion.identity,transform);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = data.BaseStats;
        stats.Health += 20;

        spawned.Init(stats, data.Animations);
    }

    private Vector2 RandomPositionWithin(Bounds spawnArea)
    {
        var randX = Random.Range(m_spawnArea.min.x, m_spawnArea.max.x);
        var randY = Random.Range(m_spawnArea.min.y, m_spawnArea.max.y);

        return new Vector2(randX, randY);
    }

    private void OnDrawGizmosSelected()
    {
        GizmosExtend.DrawBounds(m_spawnArea, Color.green);
    }
}