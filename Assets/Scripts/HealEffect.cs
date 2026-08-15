using System;
using UnityEngine;

[Serializable]
public class HealEffect : CardEffect
{
    public int healAmount;
    public override void Resolve()
    {
        Debug.Log("Recovered " + healAmount + " health");
    }
}
