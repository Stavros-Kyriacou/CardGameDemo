using System;
using UnityEngine;

[Serializable]
public class DamageEffect : CardEffect
{
    [SerializeField] private int _damage;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private HitType _hitType;

    public override void Resolve(CardContext context)
    {
        foreach (var target in context.GetTargets(TargetingConfig))
        {
            DamageInstance instance = new DamageInstance(_hitType, _damageType, _damage);
            int finalDamage = context.DamageCalculator.CalculateDamage(instance, target);
            target.EnemyStats.TakeDamage(finalDamage);
        }
    }
}
