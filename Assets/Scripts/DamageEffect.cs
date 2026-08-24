using System;
using UnityEngine;

[Serializable]
public class DamageEffect : CardEffect
{
    [SerializeField] private int _damage;
    public int Damage => _damage;

    public override void Resolve(CardContext context)
    {
        foreach (var target in context.GetTargets(TargetingConfig))
        {
            target.TakeDamage(_damage);
        }
    }
}
