using System;
using System.Collections.Generic;
using UnityEngine;

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
                targets = GetRandomEnemy();
                TargetsSelected?.Invoke(targets);
                break;

            default:
                break;
        }

    }
    private List<Enemy> GetRandomEnemy()
    {
        var targets = new List<Enemy>();
        var randomIndex = UnityEngine.Random.Range(0, _battleManager.Enemies.Count);
        targets.Add(_battleManager.Enemies[randomIndex]);
        return targets;
    }
}
