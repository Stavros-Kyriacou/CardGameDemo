public class DamageCalculator
{
    public int CalculateDamage(DamageInstance instance, Enemy enemy)
    {
        int finalDamage = instance.Amount;

        switch (instance.DamageType)
        {
            case DamageType.Physical:
                finalDamage -= enemy.EnemyStats.CurrentArmour;
                break;
            case DamageType.Magic:
                finalDamage -= enemy.EnemyStats.CurrentMagicResist;
                break;
            default:
                finalDamage = 0;
                break;
        }
        return finalDamage;
    }
}
