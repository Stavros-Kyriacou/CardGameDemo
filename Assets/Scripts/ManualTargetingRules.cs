using UnityEngine;

[System.Serializable]
public class ManualTargetingRules
{
    [Min(1)] public int MaxTargets;
    public bool AllowDuplicates;
}
