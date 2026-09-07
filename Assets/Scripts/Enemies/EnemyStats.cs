using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData _enemyData;
    private int _currentHP;
    private int _currentArmour;
    private int _currentMagicResist;
    private EnemyVisuals _enemyVisuals;

    public int CurrentHP => _currentHP;
    public int CurrentArmour => _currentArmour;
    public int CurrentMagicResist => _currentMagicResist;
    public EnemyData EnemyData => _enemyData;
    void Awake()
    {
        _enemyVisuals = GetComponent<EnemyVisuals>();
        InitialiseStats();
    }

    private void InitialiseStats()
    {
        if (_enemyData == null) return;

        _currentHP = _enemyData.BaseHP;
        _currentArmour = _enemyData.BaseArmour;
        _currentMagicResist = _enemyData.BaseMagicResist;
    }

    public void TakeDamage(int damage)
    {
        if (damage >= _currentHP)
        {
            _currentHP = 0;
            _enemyVisuals.UpdateHP();
            return;
        }

        _currentHP -= damage;
        _enemyVisuals.UpdateHP();
    }
}
