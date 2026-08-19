using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public string CardName;
    public string CardDescription;
    public int ManaCost;
    public TargetingType TargetingType;
    [Min(0)]
    public int MaxTargets;

    [SerializeReference]
    [SubclassSelector]
    public List<CardEffect> effects = new();
}
