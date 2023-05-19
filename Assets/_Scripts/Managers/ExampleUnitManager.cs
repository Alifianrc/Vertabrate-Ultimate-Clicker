using UnityEngine;

/// <summary>
/// An example of a scene-specific manager grabbing resources from the resource system
/// Scene-specific managers are things like grid managers, unit managers, environment managers etc
/// </summary>
public class ExampleUnitManager : StaticInstance<ExampleUnitManager> {

    public void SpawnHeroes() {
        SpawnUnit(PreyType.Tarodev, new Vector3(1, 0, 0));
    }

    void SpawnUnit(PreyType t, Vector3 pos) {
        var tarodevScriptable = ResourceSystem.Instance.GetPrey(t);

        var spawned = Instantiate(tarodevScriptable.Prefab, pos, Quaternion.identity,transform);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = tarodevScriptable.BaseStats;
        stats.Health += 20;

        spawned.SetStats(stats);
    }
}