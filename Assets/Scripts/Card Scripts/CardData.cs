using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public string CardName;
    public string CardDescription;
    public int ManaCost;
    public TargetingRules TargetingRules;

    [SerializeReference]
    [SubclassSelector]
    public List<CardEffect> effects = new();
}