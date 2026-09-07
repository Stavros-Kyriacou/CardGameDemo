[System.Serializable]
public class DamageInstance
{
    private HitType _hitType;
    private DamageType _damageType;
    private int _amount;

    public HitType HitType => _hitType;
    public DamageType DamageType => _damageType;
    public int Amount => _amount;

    public DamageInstance(HitType hitType, DamageType damageType, int amount)
    {
        _hitType = hitType;
        _damageType = damageType;
        _amount = amount;
    }
}
