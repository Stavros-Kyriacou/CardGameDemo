using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
   [SerializeField] private int _baseHP;
   [SerializeField] private int _baseArmour;
   [SerializeField] private int _baseMagicResist;
   public int BaseHP => _baseHP;
   public int BaseArmour => _baseArmour;
   public int BaseMagicResist => _baseMagicResist;
}
