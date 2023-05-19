using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public class ResourceSystem : StaticInstance<ResourceSystem> {
    public List<ScriptablePrey> Preys { get; private set; }
    private Dictionary<PreyType, ScriptablePrey> _preyDict;

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        Preys = Resources.LoadAll<ScriptablePrey>("ExampleHeroes").ToList();
        _preyDict = Preys.ToDictionary(r => r.Type, r => r);
    }

    public ScriptablePrey GetPrey(PreyType t) => _preyDict[t];
    public ScriptablePrey GetRandomPrey() => Preys[Random.Range(0, Preys.Count)];
}   