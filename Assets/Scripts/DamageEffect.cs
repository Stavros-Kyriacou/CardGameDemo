using System;

[Serializable]
public class DamageEffect : CardEffect
{
    public int damage;
    public int targetingIndex;

    public override void Resolve(CardContext context)
    {
        if (context.Card.Data.TargetingRules.TargetingType == TargetingType.Manual)
        {
            if (targetingIndex + 1 > context.Targets.Count)
                return;

            context.Targets[targetingIndex].TakeDamage(damage);
        }
        else
        {
            foreach (var target in context.Targets)
            {   
                target.TakeDamage(damage);
            }
        }
    }
}
