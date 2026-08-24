using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    [SerializeField] private string _cardName;
    [SerializeField] private string _cardDescription;
    [SerializeField] private int _manaCost;
    [SerializeField] private bool _requiresManualTargeting;
    [SerializeField] private ManualTargetingRules _manualTargetingRules;
    [SerializeReference] [SubclassSelector] private List<CardEffect> _effects;


    public string CardName => _cardName;
    public string CardDescription => _cardDescription;
    public int ManaCost => _manaCost;
    public bool RequiresManualTargeting => _requiresManualTargeting;
    public ManualTargetingRules ManualTargetingRules => _manualTargetingRules;
    public List<CardEffect> effects => _effects;
}