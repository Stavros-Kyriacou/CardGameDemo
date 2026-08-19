using System;
using System.Collections.Generic;

public class TargetingSystem : Singleton<TargetingSystem>
{
    private BattleManager _battleManager;
    void Start()
    {
        _battleManager = BattleManager.Instance;
    }
    public event Action<List<Enemy>> TargetsSelected;
    public void BeginTargeting(CardView card)
    {
        var targets = new List<Enemy>();
        switch (card.Data.TargetingType)
        {
            case TargetingType.None:

                break;
            case TargetingType.Self:

                break;
            case TargetingType.Enemy:

                break;
            case TargetingType.AllEnemies:
                TargetsSelected?.Invoke(_battleManager.Enemies);
                break;
            case TargetingType.RandomEnemy:

                break;

            default:
                break;
        }

    }
}
