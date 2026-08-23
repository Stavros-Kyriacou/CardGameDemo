using System;

[Serializable]
public class DamageEffect : CardEffect
{
    public int damage;
    public int targetingIndex;

    public override void Resolve(CardContext context)
    {
        if (targetingIndex + 1 > context.Targets.Count)
            return;

        context.Targets[targetingIndex].TakeDamage(damage);
    }
}
