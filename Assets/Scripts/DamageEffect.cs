using System;

[Serializable]
public class DamageEffect : CardEffect
{
    public int damage;
    public int targetingIndex;

    public override void Resolve(CardContext context)
    {
        context.Targets[targetingIndex].TakeDamage(damage);
    }
}
