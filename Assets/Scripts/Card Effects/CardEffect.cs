using System;

[Serializable]
public abstract class CardEffect
{
    public TargetingConfig TargetingConfig;
    public abstract void Resolve(CardContext context);
}
