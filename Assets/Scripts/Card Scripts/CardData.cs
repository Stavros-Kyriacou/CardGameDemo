using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public string cardName;

    [SerializeReference]
    [SubclassSelector]
    public List<CardEffect> effects = new();


}
