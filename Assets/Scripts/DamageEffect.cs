using System;
using UnityEngine;

[Serializable]
public class DamageEffect : CardEffect
{
    public int damage;

    public override void Resolve(CardContext context)
    {
        foreach (var target in context.Targets)
        {
            target.TakeDamage(damage);
        }
    }
}
